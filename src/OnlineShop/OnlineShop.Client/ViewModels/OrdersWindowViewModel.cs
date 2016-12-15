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
        private IMessageService messageService;
        private ICollectionView collectionView;

        private string searchString = string.Empty;

        public ObservableCollection<Order> Orders { get; private set; }

        public User User { get; set; }

        public string NewOrderName { get; set; }

        public Order SelectedOrder { get; set; }

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

        public OrdersWindowViewModel(IOrderService orderService, IMessageService messageService)
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
        private RelayCommand addNewOrderCommand;

        public ICommand AddNewOrderCommand
        {
            get
            {
                if (addNewOrderCommand == null)
                    addNewOrderCommand = new RelayCommand(AddNewOrderCommandExecute, AddNewOrderCommandCanExecute);

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
                User = User,
                PlacingDate = DateTime.UtcNow
            };

            orderService.Create(newOrder);
            UpdateData();
            NewOrderName = string.Empty;
        }

        #endregion

        #region WindowLoadedCommand
        private RelayCommand windowLoadedCommand;

        public ICommand WindowLoadedCommand
        {
            get
            {
                if (windowLoadedCommand == null)
                    windowLoadedCommand = new RelayCommand(WindowLoadedCommandExecute);
                return windowLoadedCommand;
            }
        }

        private void WindowLoadedCommandExecute(object obj)
        {
            UpdateData();
        }
        #endregion

        #region DeleteOrderCommand
        private RelayCommand deleteOrderCommand;

        public ICommand DeleteOrderCommand
        {
            get
            {
                if (deleteOrderCommand == null)
                    deleteOrderCommand = new RelayCommand(DeleteOrderCommandExecute, DeleteOrderCommandCanExecute);
                return deleteOrderCommand;
            }
        }

        private void DeleteOrderCommandExecute(object obj)
        {
            var result = messageService.ShowMessage("Are you sure you want to delete this order?", "Delete", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question);
            if (result == System.Windows.MessageBoxResult.Yes)
            {
                orderService.Delete(SelectedOrder.Id);
                UpdateData();
            } 
        }

        private bool DeleteOrderCommandCanExecute(object obj)
        {
            return SelectedOrder != null && SelectedOrder.Status == Status.NotDecorated;
        }
        #endregion

        private void UpdateData()
        {
            Orders.Clear();

            var orders = orderService.GetOrders().Where(order => order.User == User);
            foreach (var order in orders)
            {
                Orders.Add(order);
            }
        }
    }
}
