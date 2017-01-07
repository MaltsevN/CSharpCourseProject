using OnlineShop.Client.Common;
using OnlineShop.Client.Exceptions;
using OnlineShop.Client.Models;
using OnlineShop.Client.Services;
using OnlineShop.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
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
        private readonly IMessegeManager messageService;
        private ICollectionView collectionView;

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        public EditOrderWindowViewModel(IOrderItemService orderItemService, IProductService productService, IMessegeManager messageService)
        {
            this.orderItemService = orderItemService;
            this.productService = productService;
            this.messageService = messageService;
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

        private async void WindowLoadedCommandExecute(object obj)
        {
            IsBusy = true;
            try
            {
                foreach (ProductDto product in await productService.GetProductsAsync())
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
            catch (NoInternetConnectionException ex)
            {
                messageService.ShowMessage(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                Messenger.Default.Send<WindowMessege, bool?>(WindowMessege.CloseEditOrderWindow, false);
            }
            catch (HttpRequestException ex)
            {
                messageService.ShowMessage(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                Messenger.Default.Send<WindowMessege, bool?>(WindowMessege.CloseEditOrderWindow, false);
            }
            
            IsBusy = false;
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

        private async void SaveOrderCommandExecute(object obj)
        {
            IsBusy = true;
            var existingOrderItems = Order.OrderItems;
            var newOrderItems = OrderItems.Where(checkableItem => checkableItem.IsChecked).Select(checkableItem => checkableItem.Item);

            var addedOrderItems = newOrderItems.Where(orderItem => !existingOrderItems.Any(existingOrderItem => existingOrderItem.Id == orderItem.Id)).ToList();
            var deletedOrderItems = existingOrderItems.Where(existingOrderItem => !newOrderItems.Any(orderItem => orderItem.Id == existingOrderItem.Id)).ToList();
            var updatedOrderItems = existingOrderItems.Where(existingOrderItem => !deletedOrderItems.Any(deletedOrderItem => deletedOrderItem.Id == existingOrderItem.Id)).ToList();

            try
            {
                foreach (OrderItemDto orderItem in addedOrderItems)
                {
                    orderItem.OrderId = Order.Id;
                    var createdOrderItem = await orderItemService.CreateAsync(orderItem);
                    Order.OrderItems.Add(createdOrderItem);
                }

                foreach (OrderItemDto orderItem in deletedOrderItems)
                {
                    await orderItemService.DeleteAsync(orderItem.Id);
                    Order.OrderItems.Remove(orderItem);
                }

                foreach (OrderItemDto orderItem in updatedOrderItems)
                {
                    await orderItemService.UpdateAsync(orderItem);
                    var tempOrderItem = Order.OrderItems.Find(o => o.Id == orderItem.Id);
                    Order.OrderItems.Remove(tempOrderItem);
                    Order.OrderItems.Add(orderItem);
                }

                IsBusy = false;
                Messenger.Default.Send<WindowMessege, bool?>(WindowMessege.CloseEditOrderWindow, true);
            }
            catch (NoInternetConnectionException ex)
            {
                messageService.ShowMessage(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            catch (HttpRequestException ex)
            {
                messageService.ShowMessage(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            IsBusy = false;

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
