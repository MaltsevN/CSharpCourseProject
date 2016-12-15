using System.Windows;

namespace OnlineShop.Client.Common
{
    internal interface IMessageService
    {
        MessageBoxResult ShowMessage(string message, string header, MessageBoxButton button, MessageBoxImage icon);
    }
}