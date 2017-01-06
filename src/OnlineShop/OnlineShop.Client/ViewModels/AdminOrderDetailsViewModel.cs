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
    class AdminOrderDetailsViewModel : BaseViewModel
    {
        private readonly IOrderItemService orderItemService;
        private readonly IProductService productService;
        private readonly ICollectionView collectionView;

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

        public AdminOrderDetailsViewModel(IOrderItemService orderItemService, IProductService productService)
        {
            this.orderItemService = orderItemService;
            this.productService = productService;
            OrderItems = new ObservableCollection<CheckableItem<OrderItemDto>>();
            collectionView = CollectionViewSource.GetDefaultView(OrderItems);
            collectionView.Filter = SearchFilter;
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
            }
            IsBusy = false;
        }
        #endregion

        #region CloseCommand
        private RelayCommand<object, object> closeCommand;

        public ICommand CloseCommand
        {
            get
            {
                if (closeCommand == null)
                    closeCommand = new RelayCommand<object, object>(CancelCommandExecute);
                return closeCommand;
            }
        }

        private void CancelCommandExecute(object obj)
        {
            Messenger.Default.Send<WindowMessege, bool?>(WindowMessege.CloseOrderDetailsWindow, false);
        }
        #endregion

    }
}
