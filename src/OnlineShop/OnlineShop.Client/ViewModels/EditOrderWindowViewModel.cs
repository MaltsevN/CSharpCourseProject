using DomainModel;
using OnlineShop.Client.Common;
using OnlineShop.Client.Models;
using OnlineShop.Client.Services;
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
            OrderItems = new ObservableCollection<CheckableItem<OrderItem>>();
            collectionView = CollectionViewSource.GetDefaultView(OrderItems);
            collectionView.Filter = SearchFilter;
        }

        private bool SearchFilter(object obj)
        {
            CheckableItem<OrderItem> orderItem = obj as CheckableItem<OrderItem>;
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

        public Order Order { get; set; }

        public ObservableCollection<CheckableItem<OrderItem>> OrderItems { get; set; }

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
            foreach (Product product in productService.GetProducts())
            {
                OrderItem orderItem = Order.Items.Find(item => item.ProductId == product.Id);
                if (orderItem != null)
                {
                    OrderItems.Add(new CheckableItem<OrderItem>(orderItem)
                    {
                        IsChecked = true
                    });
                }
                else
                {
                    orderItem = new OrderItem() { ProductId = product.Id, Product = product, Quantity = 1 };
                    OrderItems.Add(new CheckableItem<OrderItem>(orderItem)
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
            return Order != null && Order.Status == Status.NotDecorated;
        }

        private void SaveOrderCommandExecute(object obj)
        {
            var existingOrderItems = Order.Items;
            var newOrderItems = OrderItems.Where(checkableItem => checkableItem.IsChecked).Select(checkableItem => checkableItem.Item);

            var addedOrderItems = newOrderItems.Where(orderItem => !existingOrderItems.Any(existingOrderItem => existingOrderItem.Id == orderItem.Id)).ToList();
            var deletedOrderItems = existingOrderItems.Where(existingOrderItem => !newOrderItems.Any(orderItem => orderItem.Id == existingOrderItem.Id)).ToList();
            var updatedOrderItems = existingOrderItems.Where(existingOrderItem => !deletedOrderItems.Any(deletedOrderItem => deletedOrderItem.Id == existingOrderItem.Id)).ToList();

            foreach (OrderItem orderItem in addedOrderItems)
            {
                Product prod = orderItem.Product;
                orderItem.Product = null;
                orderItem.OrderId = Order.Id;
                var createdOrderItem = orderItemService.Create(orderItem);
                createdOrderItem.Order = Order;
                createdOrderItem.Product = prod;
                Order.Items.Add(createdOrderItem);

            }

            foreach (OrderItem orderItem in deletedOrderItems)
            {
                orderItemService.Delete(orderItem.Id);
                Order.Items.Remove(orderItem);
            }

            foreach (OrderItem orderItem in updatedOrderItems)
            {
                Product prod = orderItem.Product;
                orderItem.Product = null;
                orderItem.Order = null;
                orderItemService.Update(orderItem);
                var tempOrderItem = Order.Items.Find(o => o.Id == orderItem.Id);
                Order.Items.Remove(tempOrderItem);
                orderItem.Product = prod;
                orderItem.Order = Order;
                Order.Items.Add(orderItem);
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
