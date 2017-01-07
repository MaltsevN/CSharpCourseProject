using OnlineShop.Client.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace OnlineShop.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Logger.InitLogger();
            base.OnStartup(e);
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Logger.For(this).Fatal(e.Exception.Message, e.Exception);
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Logger.For(this).Info("Application Exit");
        }
    }
}
