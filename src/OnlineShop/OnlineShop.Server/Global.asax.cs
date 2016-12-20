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
using OnlineShop.DAL.EntityFramework;

namespace OnlineShop.Server
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<OrderContract>();
            builder.RegisterType<ProductContract>();
            builder.RegisterType<UserContract>();
            
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            AutofacHostFactory.Container = builder.Build();

            RouteTable.Routes.Add(new ServiceRoute("Order", new AutofacServiceHostFactory(), typeof(OrderContract)));
            RouteTable.Routes.Add(new ServiceRoute("Product", new AutofacServiceHostFactory(), typeof(ProductContract)));
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