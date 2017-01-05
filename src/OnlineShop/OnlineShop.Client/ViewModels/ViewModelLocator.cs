using Autofac;
using OnlineShop.Client.Common;
using OnlineShop.Client.Services;
using OnlineShop.DTO;
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

            builder.RegisterType<OrderService>().As<IOrderService>();
            builder.RegisterType<ProductService>().As<IProductService>();
            builder.RegisterType<OrderItemService>().As<IOrderItemService>();
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>().SingleInstance();
            builder.RegisterType<UserService>().As<IUserService>();

            builder.RegisterType<MessegeManager>().As<IMessegeManager>();

            builder.RegisterType<OrdersWindowViewModel>().AsSelf();
            builder.RegisterType<EditOrderWindowViewModel>().AsSelf();
            builder.RegisterType<AuthenticationWindowViewModel>().AsSelf();
            builder.RegisterType<AdminOrdersWindowViewModel>().AsSelf();

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

        public EditOrderWindowViewModel EditOrderWindowViewModel
        {
            get
            {
                return container.Resolve<EditOrderWindowViewModel>();
            }
        }

        public AuthenticationWindowViewModel AuthenticationWindowViewModel
        {
            get
            {
                return container.Resolve<AuthenticationWindowViewModel>();
            }
        }
    }
}
