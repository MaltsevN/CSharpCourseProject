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
    /// Interaction logic for AdminOrdersWindow.xaml
    /// </summary>
    public partial class AdminOrdersWindow : Window
    {
        public AdminOrdersWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<WindowMessege, OrderDto>(this, WindowMessege.OpenOrderDetailsWindow, OpenOrderDetailsWindow);
        }

        private void OpenOrderDetailsWindow(OrderDto order)
        {
            AdminOrderDetailsWindow window = new AdminOrderDetailsWindow();
            window.Owner = this;
            var viewModel = (AdminOrderDetailsViewModel)window.DataContext;
            viewModel.Order = order;
            window.ShowDialog();
        }
    }
}
