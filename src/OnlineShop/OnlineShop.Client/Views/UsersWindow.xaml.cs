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
    /// Interaction logic for UsersWindow.xaml
    /// </summary>
    public partial class UsersWindow : Window
    {
        public UsersWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<WindowMessege, UserDto>(this, WindowMessege.OpenAddUserWindow, OpenAddUserWindow);
        }

        private void OpenAddUserWindow(UserDto newUser)
        {
            AddUserWindow window = new AddUserWindow();
            window.Owner = this;
            AddUserWindowViewModel viewModel = (AddUserWindowViewModel)window.DataContext;
            viewModel.User = newUser;
            window.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Messenger.Default.Unregister<WindowMessege>(this, WindowMessege.OpenAddUserWindow);
        }
    }
}
