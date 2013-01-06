using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Autofac;
using Autofac.Integration.Mvc;
using Budget;
using FluentValidation;
using FluentValidation.Mvc;
using Web.Authentication;
using Web.Controllers;
using Web.Database;
using Web.Events;
using Web.Events.Entity;
using Web.Filters;
using Web.Models;
using Web.Repositories;
using Web.Validation;

namespace Web {
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication {
        protected IContainer Container { get; set; }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new {controller = "Home", action = "Index", id = UrlParameter.Optional} // Parameter defaults
                );
        }

        protected void Application_Start() {
            RegisterIoC();
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e) {
            // Extract the forms authentication cookie
            var cookieName = FormsAuthentication.FormsCookieName;
            var authCookie = Context.Request.Cookies[cookieName];

            if (null == authCookie) {
                // There is no authentication cookie.
                return;
            }

            FormsAuthenticationTicket authTicket = null;
            try {
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            }
            catch (Exception ex) {
                // Log exception details (omitted for simplicity)
                return;
            }

            if (null == authTicket) {
                // Cookie failed to decrypt.
                return;
            }

            // When the ticket was created, the UserData property was assigned a
            // pipe delimited string of role names.
            var roles = authTicket.UserData.Split(new[] {'|'});

            // Create an Identity object
            var id = new FormsIdentity(authTicket);

            // This principal will flow throughout the request.
            var principal = new GenericPrincipal(id, roles);
            // Attach the new principal object to the current HttpContext object
            Context.User = principal;
        }

        private void RegisterIoC() {
            var builder = new ContainerBuilder();

            builder.Register(c => new CustomModelMetadataProvider(c.Resolve<IComponentContext>()))
                   .As<DataAnnotationsModelMetadataProvider>()
                   .InstancePerLifetimeScope();

            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();
            builder.RegisterType<ExtensibleActionInvoker>().As<IActionInvoker>();
            builder.RegisterControllers(Assembly.GetExecutingAssembly()).InjectActionInvoker();
            builder.RegisterFilterProvider();

            builder.RegisterGeneric(typeof (Repository<>)).As(typeof (IRepository<>)).SingleInstance();
            builder.RegisterType(typeof (EventBus)).As(typeof (IEventBus)).SingleInstance();
            builder.RegisterType(typeof (Database.Database)).As(typeof (IDatabase)).SingleInstance();

            builder.RegisterType(typeof(Authenticator)).As<IAuthenticator>();
            

            builder.RegisterType<SuccessMessageFilter>().As<IActionFilter>();

            builder.RegisterType<ServiceLocatorValidatorFactory>().As<IValidatorFactory>();
            var findValidatorsInAssembly = AssemblyScanner.FindValidatorsInAssembly(Assembly.GetExecutingAssembly());
            foreach (var item in findValidatorsInAssembly)
                builder.RegisterType(item.ValidatorType).PropertiesAutowired();

            Container = builder.Build();

            var fluentValidationModelValidatorProvider =
                new FluentValidationModelValidatorProvider(new ServiceLocatorValidatorFactory(Container)) {
                    AddImplicitRequiredValidator = false
                };
            ModelValidatorProviders.Providers.Add(fluentValidationModelValidatorProvider);

            ModelMetadataProviders.Current = Container.Resolve<DataAnnotationsModelMetadataProvider>();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
        }
    }

    public static class ActionFilterInjection {
        /// <summary>
        /// Registers the <see cref="AutofacFilterAttributeFilterProvider"/>.
        /// </summary>
        /// <param name="builder">The container builder.</param>
        public static void RegisterFilterProvider(this ContainerBuilder builder) {
            if (builder == null) throw new ArgumentNullException("builder");

            foreach (var provider in FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().ToArray())
                FilterProviders.Providers.Remove(provider);

            builder.RegisterType<AutofacFilterAttributeFilterProvider>()
                   .As<IFilterProvider>()
                   .SingleInstance();
        }
    }

    /// <summary>
    /// Defines a filter provider for filter attributes that performs property injection.
    /// </summary>
    public class AutofacFilterAttributeFilterProvider : FilterAttributeFilterProvider {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutofacFilterAttributeFilterProvider"/> class.
        /// </summary>
        /// <remarks>
        /// The <c>false</c> constructor parameter passed to base here ensures that attribute instances are not cached.
        /// </remarks>
        public AutofacFilterAttributeFilterProvider()
            : base(false) {}

        /// <summary>
        /// Aggregates the filters from all of the filter providers into one collection.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="actionDescriptor">The action descriptor.</param>
        /// <returns>
        /// The collection filters from all of the filter providers with properties injected.
        /// </returns>
        public override IEnumerable<Filter> GetFilters(ControllerContext controllerContext,
                                                       ActionDescriptor actionDescriptor) {
            var filters = base.GetFilters(controllerContext, actionDescriptor).ToArray();
            var lifetimeScope = AutofacDependencyResolver.Current.RequestLifetimeScope;

            if (lifetimeScope != null)
                foreach (var filter in filters)
                    lifetimeScope.InjectProperties(filter.Instance);

            return filters;
        }
    }
}