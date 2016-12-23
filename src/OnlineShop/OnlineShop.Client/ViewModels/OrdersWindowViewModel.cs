using DomainModel;
using OnlineShop.Client.Common;
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
    class OrdersWindowViewModel : BaseViewModel
    {
        private IOrderService orderService;
        private IMessegeManager messageService;
        private ICollectionView collectionView;


        public ObservableCollection<Order> Orders { get; private set; }

        public User User { get; set; }

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

        private Order selectedOrder;
        public Order SelectedOrder
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

        public OrdersWindowViewModel(IOrderService orderService, IMessegeManager messageService)
        {
            this.orderService = orderService;
            this.messageService = messageService;
            Orders = new ObservableCollection<Order>();
            collectionView = CollectionViewSource.GetDefaultView(Orders);
            collectionView.Filter = SearchFilter;
        }

        private bool SearchFilter(object obj)
        {
            Order order = obj as Order;
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

        private void AddNewOrderCommandExecute(object obj)
        {
            Order newOrder = new Order()
            {
                Name = NewOrderName,
                Status = Status.NotDecorated,
                UserId = User.Id,
                PlacingDate = DateTime.UtcNow
            };


            Order order = orderService.Create(newOrder);
            Messenger.Default.Send<WindowMessege, Order>(WindowMessege.OpenEtitOrderWindow, newOrder);
            User.Orders.Add(order);
            Orders.Add(order);
            NewOrderName = string.Empty;
            CollectionViewSource.GetDefaultView(Orders).Refresh();
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
            foreach (var order in User.Orders)
            {
                Orders.Add(order);
            }
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

        private void DeleteOrderCommandExecute(object obj)
        {
            var result = messageService.ShowMessage("Are you sure you want to delete this order?", "Delete", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question);
            if (result == System.Windows.MessageBoxResult.Yes)
            {
                orderService.Delete(SelectedOrder.Id);
                User.Orders.Remove(SelectedOrder);
                Orders.Remove(SelectedOrder);
            }
        }

        private bool DeleteOrderCommandCanExecute(object obj)
        {
            return SelectedOrder != null && SelectedOrder.Status == Status.NotDecorated;
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

        private void ConfirmOrderCommandExecute(object obj)
        {
            SelectedOrder.Status = Status.Processing;
            orderService.Update(SelectedOrder);
            CollectionViewSource.GetDefaultView(Orders).Refresh();
            OnPropertyChanged(nameof(SelectedOrder));
        }

        private bool ConfirmOrderCommandCanExecute(object obj)
        {
            return SelectedOrder != null && SelectedOrder.Status == Status.NotDecorated;
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

        private void CancelOrderCommandExecute(object obj)
        {
            SelectedOrder.Status = Status.NotDecorated;
            orderService.Update(SelectedOrder);
            CollectionViewSource.GetDefaultView(Orders).Refresh();
            OnPropertyChanged(nameof(SelectedOrder));
        }

        private bool CancelOrderCommandCanExecute(object obj)
        {
            return SelectedOrder != null && SelectedOrder.Status == Status.Processing;
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
            Messenger.Default.Send<WindowMessege, Order>(WindowMessege.OpenEtitOrderWindow, SelectedOrder);
            CollectionViewSource.GetDefaultView(Orders).Refresh();
        }

        #endregion
    }
}
