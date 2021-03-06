﻿using System;
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
    /// Interaction logic for EditOrderWindow.xaml
    /// </summary>
    public partial class EditOrderWindow : Window
    {
        public EditOrderWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<WindowMessege, bool?>(this, WindowMessege.CloseEditOrderWindow, CloseEditOrderWindow);
        }

        private void CloseEditOrderWindow(bool? dialogResult)
        {
            this.DialogResult = dialogResult;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Messenger.Default.Unregister<WindowMessege>(this, WindowMessege.CloseEditOrderWindow);
            Messenger.Default.Send<WindowMessege, bool?>(WindowMessege.ClosingEditOrderWindow, this.DialogResult);
        }

        private bool isManualEditCommit;
        private void orderItemList_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (!isManualEditCommit)
            {
                isManualEditCommit = true;
                DataGrid grid = (DataGrid)sender;
                grid.CommitEdit(DataGridEditingUnit.Row, true);
                CollectionViewSource.GetDefaultView(orderItemList.ItemsSource).Refresh();
                isManualEditCommit = false;
            }
        }
    }
}
