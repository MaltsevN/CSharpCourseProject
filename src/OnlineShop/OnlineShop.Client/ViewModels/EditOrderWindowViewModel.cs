using DomainModel;
using OnlineShop.Client.Common;
using OnlineShop.Client.Models;
using OnlineShop.Client.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OnlineShop.Client.ViewModels
{
    class EditOrderWindowViewModel : BaseViewModel
    {
        private readonly IOrderItemService orderItemService;
        private readonly IProductService productService;

        public EditOrderWindowViewModel(IOrderItemService orderItemService, IProductService productService)
        {
            this.orderItemService = orderItemService;
            this.productService = productService;
            OrderItems = new ObservableCollection<CheckableItem<OrderItem>>();
        }

        private string searchString;
        public string SearchString
        {
            get { return searchString; }
            set
            {
                searchString = value;
                OnPropertyChanged(nameof(SearchString));
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
                OrderItem orderItem = Order.Items.Find(item => item.Product.Equals(product));
                if (orderItem != null)
                {
                    OrderItems.Add(new CheckableItem<OrderItem>(orderItem)
                    {
                        IsChecked = true
                    });
                }
                else
                {
                    orderItem = new OrderItem() { Product = product, Quantity = 1 };
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
                    saveOrderCommand = new RelayCommand<object, object>(SaveOrderCommandExecute);
                return saveOrderCommand;
            }
        }

        private void SaveOrderCommandExecute(object obj)
        {
            
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

        }
        #endregion

    }
}
