using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OnlineShop.Client.Common
{
    class MessegeManager : IMessegeManager
    {
        public MessageBoxResult ShowMessage(string message, string header, MessageBoxButton button, MessageBoxImage icon)
        {
            return MessageBox.Show(message, header, button, icon);
        }
    }
}
