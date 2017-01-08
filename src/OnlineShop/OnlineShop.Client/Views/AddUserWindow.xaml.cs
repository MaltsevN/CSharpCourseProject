using OnlineShop.Client.Common;
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
    /// Interaction logic for AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        public AddUserWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<WindowMessege, bool?>(this, WindowMessege.CloseAddUserWindow, CloseAddUserWindow);
        }

        private void CloseAddUserWindow(bool? dialogResult)
        {
            this.DialogResult = dialogResult;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Messenger.Default.Unregister<WindowMessege>(this, WindowMessege.CloseAddUserWindow);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Messenger.Default.Send<WindowMessege, bool?>(WindowMessege.AddUserWindowClosed, this.DialogResult);
        }
    }
}
