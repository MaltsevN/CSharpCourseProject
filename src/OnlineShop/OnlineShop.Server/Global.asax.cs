using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using OnlineShop.ServiceContracts;
using Autofac;
using Autofac.Integration.Wcf;
using DAL;
using DomainModel;

namespace OnlineShop.Server
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<OrderContract>();
            builder.RegisterType<UserContract>();

            builder.RegisterType<OrderRepository>().As<IRepository<Order>>();
            builder.RegisterType<UserRepository>().As<IRepository<User>>();

            AutofacHostFactory.Container = builder.Build();

            RouteTable.Routes.Add(new ServiceRoute("Order", new AutofacServiceHostFactory(), typeof(OrderContract)));
            RouteTable.Routes.Add(new ServiceRoute("User", new AutofacServiceHostFactory(), typeof(UserContract)));
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}