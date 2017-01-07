using OnlineShop.Client.Common;
using OnlineShop.Client.ViewModels;
using OnlineShop.DTO;
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

namespace OnlineShop.Client.Views
{
    /// <summary>
    /// Interaction logic for AuthenticationWindow.xaml
    /// </summary>
    public partial class AuthenticationWindow : Window
    {
        public AuthenticationWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<WindowMessege, object>(this, WindowMessege.CloseAuthenticationWindow, CloseAuthenticationWindow);
            Messenger.Default.Register<WindowMessege, UserDto>(this, WindowMessege.OpenOrdersWindow, OpenOrdersWindow);
            Messenger.Default.Register<WindowMessege, object>(this, WindowMessege.OpenAdminOrderWindow, OpenAdminOrderWindow);
        }

        private void OpenOrdersWindow(UserDto user)
        {
            OrdersWindow window = new OrdersWindow();
            var viewModel = (OrdersWindowViewModel)window.DataContext;
            viewModel.User = user;
            window.Show();
        }

        private void OpenAdminOrderWindow(object obj)
        {
            AdminOrdersWindow window = new AdminOrdersWindow();
            window.Show();
        }

        private void CloseAuthenticationWindow(object obj)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Messenger.Default.Unregister<WindowMessege>(this, WindowMessege.CloseAuthenticationWindow);
            Messenger.Default.Unregister<WindowMessege>(this, WindowMessege.OpenOrdersWindow);
            Messenger.Default.Unregister<WindowMessege>(this, WindowMessege.OpenAdminOrderWindow);
        }
    }
}
