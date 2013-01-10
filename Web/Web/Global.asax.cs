using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using Autofac;
using Autofac.Integration.Mvc;
using FluentValidation;
using FluentValidation.Mvc;
using Web.Authentication;
using Web.Database;
using Web.Email;
using Web.Events;
using Web.FacebookAuthentication;
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

        protected void Application_AcquireRequestState(object sender, EventArgs e){
            var account = DependencyResolver.Current.GetService<IAccount>();
            var accountSessionRepository = DependencyResolver.Current.GetService<IAccountSessionRepository>();
            Context.Items["Account"] = new AccountView { Data = account, IsLoggedIn = accountSessionRepository.GetCurrentId() != Guid.Empty };
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
            builder.RegisterType(typeof (FriendRepository)).As(typeof (IFriendRepository));
            builder.RegisterType(typeof(FacebookStateRepository)).As(typeof(IFacebookDataRepository));
            builder.RegisterType(typeof(Authenticator)).As<IAuthenticator>();
            builder.RegisterType(typeof(EmailSender)).As(typeof(IEmailSender));
            builder.RegisterType(typeof(AccountRepository)).Named<IAccountRepository>("BaseAccountRepository");
            builder.Register(c => new FacebookAccountRepository(c.ResolveNamed<IAccountRepository>("BaseAccountRepository"), c.Resolve<IFacebookDataRepository>())).As(typeof(IAccountRepository));
            builder.RegisterType(typeof (AccountSessionRepository)).As(typeof (IAccountSessionRepository));

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(t => t.Name.EndsWith("Denormalizer")).AsImplementedInterfaces();
            
            builder.Register(c => c.Resolve<IAccountRepository>().FindById(c.Resolve<IAccountSessionRepository>().GetCurrentId())).As<IAccount>().InstancePerLifetimeScope();

            builder.RegisterType<GlobalMessageFilter>().As<IActionFilter>();

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

    public static class CustomContext {
        public static AccountView Account(this HttpContextBase contextBase) {
            return (AccountView)contextBase.Items["Account"];
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