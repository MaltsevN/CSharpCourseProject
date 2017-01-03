using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OnlineShop.Client.Controls
{
    /// <summary>
    /// Interaction logic for BusyLoader.xaml
    /// </summary>
    public partial class BusyLoader : UserControl
    {
        public BusyLoader()
        {
            InitializeComponent();
            busyIndicator.Visibility = IsLoading ? Visibility.Visible : Visibility.Collapsed;
        }
        
        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }

        public static readonly DependencyProperty IsLoadingProperty =
            DependencyProperty.Register("IsLoading", typeof(bool), typeof(BusyLoader), new PropertyMetadata(false, IsLoadingChangedCallback));

        private static void IsLoadingChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            BusyLoader loader = (BusyLoader)sender;
            bool isLoading = (bool)e.NewValue;
            loader.busyIndicator.Visibility = isLoading ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
