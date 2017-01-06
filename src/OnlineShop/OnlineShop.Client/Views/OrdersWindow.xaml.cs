using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OnlineShop.Client.Common;
using OnlineShop.Client.ViewModels;
using OnlineShop.DTO;

namespace OnlineShop.Client.Views
{
    /// <summary>
    /// Interaction logic for OrdersWindow.xaml
    /// </summary>
    public partial class OrdersWindow : Window
    {
        public OrdersWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<WindowMessege, OrderDto>(this, WindowMessege.OpenEditOrderWindow, OpenEtitOrderWindow);
        }

        private void OpenEtitOrderWindow(OrderDto order)
        {
            EditOrderWindow window = new EditOrderWindow();
            window.Owner = this;
            var viewModel = (EditOrderWindowViewModel)window.DataContext;
            viewModel.Order = order;
            window.ShowDialog();
        }
    }
}
