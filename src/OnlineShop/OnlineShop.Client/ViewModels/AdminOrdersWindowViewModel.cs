using OnlineShop.Client.Common;
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
    class AdminOrdersWindowViewModel : BaseViewModel
    {
        private IOrderService orderService;
        private IMessegeManager messageService;
        private ICollectionView collectionView;
        
        public ObservableCollection<OrderDto> OrdersObsCol { get; private set; }
        
        private OrderDto clonedOrder;

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

        public ObservableCollection<StatusDto> StatusesObsCol { get; set; }
                 
        private StatusDto selectedStatus = StatusDto.Processing;
        public StatusDto SelectedStatus
        {
            get
            {
                return selectedStatus;
            }
            set
            {
                selectedStatus = value;
                OnPropertyChanged(nameof(selectedStatus));
                collectionView.Refresh();
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

        public AdminOrdersWindowViewModel(IOrderService orderService, IMessegeManager messageService)
        {
            this.orderService = orderService;
            this.messageService = messageService;
            OrdersObsCol = new ObservableCollection<OrderDto>();
            StatusesObsCol = new ObservableCollection<StatusDto>();
            collectionView = CollectionViewSource.GetDefaultView(OrdersObsCol);
            collectionView.Filter = Filter;
        }

        private bool Filter(object obj)
        {
            OrderDto order = obj as OrderDto;
            if (order == null)
                return false;

            if (string.IsNullOrEmpty(SearchString))
                return order.Status == SelectedStatus;

            return order.Name.ToLower().Contains(SearchString.ToLower()) && order.Status == SelectedStatus;
        }

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
            foreach (var item in Enum.GetValues(typeof(StatusDto)).Cast<StatusDto>())
            {
                StatusesObsCol.Add(item);
            }
            CollectionViewSource.GetDefaultView(StatusesObsCol).Refresh();
            foreach (var order in await orderService.GetOrdersAsync())
            {
                OrdersObsCol.Add(order);
            }
            IsBusy = false;
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
            SelectedOrder.Status = StatusDto.Confirmed;
            await orderService.UpdateAsync(SelectedOrder);
            CollectionViewSource.GetDefaultView(OrdersObsCol).Refresh();
            OnPropertyChanged(nameof(SelectedOrder));
            IsBusy = false;
        }

        private bool ConfirmOrderCommandCanExecute(object obj)
        {
            return SelectedOrder != null && SelectedOrder.Status == StatusDto.Processing;
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
            IsBusy = true;
            SelectedOrder.Status = StatusDto.Cancelled;
            await orderService.UpdateAsync(SelectedOrder);
            CollectionViewSource.GetDefaultView(OrdersObsCol).Refresh();
            OnPropertyChanged(nameof(SelectedOrder));
            IsBusy = false;
        }

        private bool CancelOrderCommandCanExecute(object obj)
        {
            return SelectedOrder != null && SelectedOrder.Status == StatusDto.Processing;
        }
        #endregion

        #region DetailsCommand
        private RelayCommand<object, object> detailsCommand;

        public ICommand AdminOrderDetailsCommand
        {
            get
            {
                if (detailsCommand == null)
                    detailsCommand = new RelayCommand<object, object>(DetailsCommandExecute, DetailsCommandCanExecute);

                return detailsCommand;
            }
        }

        private bool DetailsCommandCanExecute(object obj)
        {
            return SelectedOrder != null;
        }

        private void DetailsCommandExecute(object obj)
        {
            Messenger.Default.Send<WindowMessege, OrderDto>(WindowMessege.OpenOrderDetailsWindow, SelectedOrder);
        }

        #endregion
        
    }
}

