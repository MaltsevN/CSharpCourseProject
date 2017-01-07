using OnlineShop.Client.Common;
using OnlineShop.Client.Exceptions;
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
    class OrdersWindowViewModel : BaseViewModel
    {
        private IOrderService orderService;
        private IMessegeManager messageService;
        private ICollectionView collectionView;

        private OrderDto clonedOrder;


        public ObservableCollection<OrderDto> Orders { get; private set; }

        public UserDto User { get; set; }

        private string newOrderName = string.Empty;
        public string NewOrderName
        {
            get
            {
                return newOrderName;
            }
            set
            {
                newOrderName = value;
                OnPropertyChanged(nameof(NewOrderName));
            }
        }

        private OrderDto selectedOrder;
        public OrderDto SelectedOrder
        {
            get
            {
                return selectedOrder;
            }
            set
            {
                selectedOrder = value;
                OnPropertyChanged(nameof(SelectedOrder));
            }
        }

        private string searchString = string.Empty;
        public string SearchString
        {
            get
            {
                return searchString;
            }
            set
            {
                searchString = value;
                OnPropertyChanged(nameof(SearchString));
                collectionView.Refresh();
            }
        }

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

        public OrdersWindowViewModel(IOrderService orderService, IMessegeManager messageService)
        {
            this.orderService = orderService;
            this.messageService = messageService;
            Orders = new ObservableCollection<OrderDto>();
            collectionView = CollectionViewSource.GetDefaultView(Orders);
            collectionView.Filter = SearchFilter;
            Messenger.Default.Register<WindowMessege, bool?>(this, WindowMessege.ClosingEditOrderWindow, ClosingEditOrderWindow);
        }

        private void ClosingEditOrderWindow(bool? dialogResult)
        {
            if(dialogResult == true)
            {
                OrderDto order = Orders.First(o => o.Id == clonedOrder.Id);
                order.OrderItems = clonedOrder.OrderItems;
                SelectedOrder = order;
                CollectionViewSource.GetDefaultView(Orders).Refresh();
            }
        }

        private bool SearchFilter(object obj)
        {
            OrderDto order = obj as OrderDto;
            if (order == null)
                return false;

            if (string.IsNullOrEmpty(SearchString))
                return true;

            return order.Name.ToLower().Contains(SearchString.ToLower());
        }

        #region AddNewOrderCommand
        private RelayCommand<object, object> addNewOrderCommand;

        public ICommand AddNewOrderCommand
        {
            get
            {
                if (addNewOrderCommand == null)
                    addNewOrderCommand = new RelayCommand<object, object>(AddNewOrderCommandExecute, AddNewOrderCommandCanExecute);

                return addNewOrderCommand;
            }
        }

        private bool AddNewOrderCommandCanExecute(object obj)
        {
            return !string.IsNullOrEmpty(NewOrderName);
        }

        private async void AddNewOrderCommandExecute(object obj)
        {
            IsBusy = true;
            Logger.For(this).Info("Adding new order");
            OrderDto newOrder = new OrderDto()
            {
                Name = NewOrderName,
                Status = StatusDto.NotDecorated,
                User = new UserChildDto()
                {
                    Id = User.Id,
                    Login = User.Login,
                    Name = User.Name,
                    Rank = User.Rank
                },
                PlacingDate = DateTime.UtcNow
            };

            try
            {
                OrderDto order = await orderService.CreateAsync(newOrder);
                User.Orders.Add(order);
                Orders.Add(order);
                clonedOrder = ObjectCopier.Clone<OrderDto>(order);
                IsBusy = false;
                NewOrderName = string.Empty;
                Messenger.Default.Send<WindowMessege, OrderDto>(WindowMessege.OpenEditOrderWindow, clonedOrder);
                Logger.For(this).Info("New order is added");
            }
            catch (NoInternetConnectionException ex)
            {
                Logger.For(this).Error(ex.Message, ex);
                messageService.ShowMessage(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            catch (HttpRequestException ex)
            {
                Logger.For(this).Error(ex.Message, ex);
                messageService.ShowMessage(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            IsBusy = false;

        }

        #endregion

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
            Logger.For(this).Info("OrdersWindow is loading");
            IsBusy = true;
            foreach (var order in User.Orders)
            {
                Orders.Add(order);
            }
            IsBusy = false;
        }
        #endregion

        #region DeleteOrderCommand
        private RelayCommand<object, object> deleteOrderCommand;

        public ICommand DeleteOrderCommand
        {
            get
            {
                if (deleteOrderCommand == null)
                    deleteOrderCommand = new RelayCommand<object, object>(DeleteOrderCommandExecute, DeleteOrderCommandCanExecute);
                return deleteOrderCommand;
            }
        }

        private async void DeleteOrderCommandExecute(object obj)
        {
            var result = messageService.ShowMessage("Are you sure you want to delete this order?", "Delete", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question);
            if (result == System.Windows.MessageBoxResult.Yes)
            {
                Logger.For(this).Info("Removing order");
                IsBusy = true;
                try
                {
                    await orderService.DeleteAsync(SelectedOrder.Id);
                    User.Orders.Remove(SelectedOrder);
                    Orders.Remove(SelectedOrder);
                    Logger.For(this).Info("Order is removed");
                }
                catch (NoInternetConnectionException ex)
                {
                    Logger.For(this).Error(ex.Message, ex);
                    messageService.ShowMessage(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                }
                catch (HttpRequestException ex)
                {
                    Logger.For(this).Error(ex.Message, ex);
                    messageService.ShowMessage(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                }
                
                IsBusy = false;
            }
        }

        private bool DeleteOrderCommandCanExecute(object obj)
        {
            return SelectedOrder != null && SelectedOrder.Status == StatusDto.NotDecorated;
        }
        #endregion

        #region ConfirmOrderCommand
        private RelayCommand<object, object> confirmOrderCommand;
        public ICommand ConfirmOrderCommand
        {
            get
            {
                if (confirmOrderCommand == null)
                    confirmOrderCommand = new RelayCommand<object, object>(ConfirmOrderCommandExecute, ConfirmOrderCommandCanExecute);
                return confirmOrderCommand;
            }
        }

        private async void ConfirmOrderCommandExecute(object obj)
        {
            IsBusy = true;
            Logger.For(this).Info("Confirming order");
            SelectedOrder.Status = StatusDto.Processing;
            try
            {
                await orderService.UpdateAsync(SelectedOrder);
            }
            catch (NoInternetConnectionException ex)
            {
                Logger.For(this).Error(ex.Message, ex);
                messageService.ShowMessage(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                SelectedOrder.Status = StatusDto.NotDecorated;
            }
            catch (HttpRequestException ex)
            {
                Logger.For(this).Error(ex.Message, ex);
                messageService.ShowMessage(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                SelectedOrder.Status = StatusDto.NotDecorated;
            }
            
            CollectionViewSource.GetDefaultView(Orders).Refresh();
            OnPropertyChanged(nameof(SelectedOrder));
            IsBusy = false;
        }

        private bool ConfirmOrderCommandCanExecute(object obj)
        {
            return SelectedOrder != null && SelectedOrder.Status == StatusDto.NotDecorated && SelectedOrder.OrderItems.Count > 0;
        }
        #endregion

        #region CancelOrderCommand
        private RelayCommand<object, object> cancelOrderCommand;
        public ICommand CancelOrderCommand
        {
            get
            {
                if (cancelOrderCommand == null)
                    cancelOrderCommand = new RelayCommand<object, object>(CancelOrderCommandExecute, CancelOrderCommandCanExecute);
                return cancelOrderCommand;
            }
        }

        private async void CancelOrderCommandExecute(object obj)
        {
            Logger.For(this).Info("Canceling order");
            IsBusy = true;
            SelectedOrder.Status = StatusDto.NotDecorated;
            try
            {
                await orderService.UpdateAsync(SelectedOrder);
            }
            catch (NoInternetConnectionException ex)
            {
                Logger.For(this).Error(ex.Message, ex);
                messageService.ShowMessage(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                SelectedOrder.Status = StatusDto.Processing;
            }
            catch (HttpRequestException ex)
            {
                Logger.For(this).Error(ex.Message, ex);
                messageService.ShowMessage(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                SelectedOrder.Status = StatusDto.Processing;
            }
            
            CollectionViewSource.GetDefaultView(Orders).Refresh();
            OnPropertyChanged(nameof(SelectedOrder));
            IsBusy = false;
        }

        private bool CancelOrderCommandCanExecute(object obj)
        {
            return SelectedOrder != null && SelectedOrder.Status == StatusDto.Processing;
        }
        #endregion

        #region EditOrderCommand
        private RelayCommand<object, object> editOrderCommand;

        public ICommand EditOrderCommand
        {
            get
            {
                if (editOrderCommand == null)
                    editOrderCommand = new RelayCommand<object, object>(EditOrderCommandExecute, EditOrderCommandCanExecute);

                return editOrderCommand;
            }
        }

        private bool EditOrderCommandCanExecute(object obj)
        {
            return SelectedOrder != null;
        }

        private void EditOrderCommandExecute(object obj)
        {
            Logger.For(this).Info("Open EditOrderWindow");
            clonedOrder = ObjectCopier.Clone<OrderDto>(SelectedOrder);
            Messenger.Default.Send<WindowMessege, OrderDto>(WindowMessege.OpenEditOrderWindow, clonedOrder);
        }

        #endregion
    }
}
