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
using System.Data;

namespace OnlineShop.Client.Views
{
    /// <summary>
    /// Interaction logic for AdminOrderDetailsWindow.xaml
    /// </summary>
    public partial class AdminOrderDetailsWindow : Window
    {
        public AdminOrderDetailsWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<WindowMessege, bool?>(this, WindowMessege.CloseOrderDetailsWindow, CloseOrderDetailsWindow);
        }

        private void CloseOrderDetailsWindow(bool? dialogResult)
        {
            this.DialogResult = dialogResult;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Messenger.Default.Unregister<WindowMessege>(this, WindowMessege.CloseOrderDetailsWindow);
        }
        
    }
}
