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
using DomainModel;
using OnlineShop.Client.ViewModels;

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
            Messenger.Default.Register<WindowMessege, Order>(this, WindowMessege.OpenEtitOrderWindow, OpenEtitOrderWindow);
        }

        private void OpenEtitOrderWindow(Order order)
        {
            EditOrderWindow window = new EditOrderWindow();
            var viewModel = (EditOrderWindowViewModel)window.DataContext;
            viewModel.Order = order;
            window.ShowDialog();
        }
    }
}
