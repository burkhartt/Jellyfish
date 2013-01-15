using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Cms.Controllers;
using Denormalizers;
using Events;

namespace Cms {
    public class MvcApplication : HttpApplication {
        protected IContainer Container { get; set; }

        protected void Application_Start() {
            RegisterIoC();
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
        }

        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new {controller = "Home", action = "Index", id = UrlParameter.Optional} // Parameter defaults
                );
        }

        private void RegisterIoC() {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new DenormalizerModule());
            builder.RegisterModule(new EventModule());

            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();
            builder.RegisterType<ExtensibleActionInvoker>().As<IActionInvoker>();
            builder.RegisterControllers(Assembly.GetExecutingAssembly()).InjectActionInvoker();
            builder.RegisterFilterProvider();

            builder.RegisterType(typeof (CommandBus)).As<ICommandBus>();
            builder.RegisterType(typeof (DomainRepository)).As<IDomainRepository>().SingleInstance();
            
            

            builder.RegisterAssemblyTypes(typeof(CreateAccountCommandHandler).Assembly)
                   .Where(t => t.Name.EndsWith("CommandHandler"))
                   .AsImplementedInterfaces();

            Container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
        }
    }
}