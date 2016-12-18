using Autofac;
using DomainModel;
using OnlineShop.Client.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Client.ViewModels
{
    class ViewModelLocator
    {
        static Autofac.IContainer container;

        static ViewModelLocator()
        {
            ConfigureContainer();
        }

        static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Design.DesignOrderService>().As<IOrderService>();
            builder.RegisterType<OrdersWindowViewModel>().AsSelf();

            //Desing Data

            Design.DesignUserService userService = new Design.DesignUserService();
            User user = userService.GetUser(0);
            builder.RegisterType<OrdersWindowViewModel>().WithProperty("User", user);
            //

            container = builder.Build();

        }

        protected static bool IsInDesignMode
        {
            get
            {
                return DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject());
            }
        }

        public OrdersWindowViewModel OrdersWindowViewModel
        {
            get
            {
                return container.Resolve<OrdersWindowViewModel>();
            }
        }
    }
}
