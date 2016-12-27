using OnlineShop.Client.Common;
using OnlineShop.Client.Models;
using OnlineShop.Client.Services;
using OnlineShop.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace OnlineShop.Client.ViewModels
{
    class EditOrderWindowViewModel : BaseViewModel
    {
        private readonly IOrderItemService orderItemService;
        private readonly IProductService productService;
        private ICollectionView collectionView;

        public EditOrderWindowViewModel(IOrderItemService orderItemService, IProductService productService)
        {
            this.orderItemService = orderItemService;
            this.productService = productService;
            OrderItems = new ObservableCollection<CheckableItem<OrderItemDto>>();
            collectionView = CollectionViewSource.GetDefaultView(OrderItems);
            collectionView.Filter = SearchFilter;
        }

        private bool SearchFilter(object obj)
        {
            CheckableItem<OrderItemDto> orderItem = obj as CheckableItem<OrderItemDto>;
            if (orderItem == null)
                return false;

            if (string.IsNullOrEmpty(SearchString))
                return true;

            return orderItem.Item.Product.Name.ToLower().Contains(SearchString.ToLower());
        }

        private string searchString;
        public string SearchString
        {
            get { return searchString; }
            set
            {
                searchString = value;
                OnPropertyChanged(nameof(SearchString));
                collectionView.Refresh();
            }
        }

        public OrderDto Order { get; set; }

        public ObservableCollection<CheckableItem<OrderItemDto>> OrderItems { get; set; }

        #region WindowLoadedCommand
        private RelayCommand<object, object> windowLoadedCommand;

        public ICommand WindowLoadedCommand
        {
            get
            {
                if (windowLoadedCommand == null)
                    windowLoadedCommand = new RelayCommand<object, object>(WindowLoadedCommandExecute);
                return windowLoadedCommand;
            }
        }

        private void WindowLoadedCommandExecute(object obj)
        {
            foreach (ProductDto product in productService.GetProducts())
            {
                OrderItemDto orderItem = Order.OrderItems.Find(item => item.Product.Id == product.Id);
                if (orderItem != null)
                {
                    OrderItems.Add(new CheckableItem<OrderItemDto>(orderItem)
                    {
                        IsChecked = true
                    });
                }
                else
                {
                    orderItem = new OrderItemDto() { Product = product, Quantity = 1 };
                    OrderItems.Add(new CheckableItem<OrderItemDto>(orderItem)
                    {
                        IsChecked = false
                    });
                }
            }
        }
        #endregion

        #region SaveOrderCommand
        private RelayCommand<object, object> saveOrderCommand;

        public ICommand SaveOrderCommand
        {
            get
            {
                if (saveOrderCommand == null)
                    saveOrderCommand = new RelayCommand<object, object>(SaveOrderCommandExecute, SaveOrderCommandCanExecute);
                return saveOrderCommand;
            }
        }

        private bool SaveOrderCommandCanExecute(object obj)
        {
            return Order != null && Order.Status == StatusDto.NotDecorated;
        }

        private void SaveOrderCommandExecute(object obj)
        {
            var existingOrderItems = Order.OrderItems;
            var newOrderItems = OrderItems.Where(checkableItem => checkableItem.IsChecked).Select(checkableItem => checkableItem.Item);

            var addedOrderItems = newOrderItems.Where(orderItem => !existingOrderItems.Any(existingOrderItem => existingOrderItem.Id == orderItem.Id)).ToList();
            var deletedOrderItems = existingOrderItems.Where(existingOrderItem => !newOrderItems.Any(orderItem => orderItem.Id == existingOrderItem.Id)).ToList();
            var updatedOrderItems = existingOrderItems.Where(existingOrderItem => !deletedOrderItems.Any(deletedOrderItem => deletedOrderItem.Id == existingOrderItem.Id)).ToList();

            foreach (OrderItemDto orderItem in addedOrderItems)
            {
                orderItem.OrderId = Order.Id;
                var createdOrderItem = orderItemService.Create(orderItem);
                Order.OrderItems.Add(createdOrderItem);
            }

            foreach (OrderItemDto orderItem in deletedOrderItems)
            {
                orderItemService.Delete(orderItem.Id);
                Order.OrderItems.Remove(orderItem);
            }

            foreach (OrderItemDto orderItem in updatedOrderItems)
            {
                orderItemService.Update(orderItem);
                var tempOrderItem = Order.OrderItems.Find(o => o.Id == orderItem.Id);
                Order.OrderItems.Remove(tempOrderItem);
                Order.OrderItems.Add(orderItem);
            }

            Messenger.Default.Send<WindowMessege, bool?>(WindowMessege.CloseEditOrderWindow, true);
        }
        #endregion

        #region CancelCommand
        private RelayCommand<object, object> cancelCommand;

        public ICommand CancelCommand
        {
            get
            {
                if (cancelCommand == null)
                    cancelCommand = new RelayCommand<object, object>(CancelCommandExecute);
                return cancelCommand;
            }
        }

        private void CancelCommandExecute(object obj)
        {
            Messenger.Default.Send<WindowMessege, bool?>(WindowMessege.CloseEditOrderWindow, false);
        }
        #endregion

    }
}
