using System.Web.Mvc;
using Authentication;
using Autofac;
using Domain.Repositories;
using Entities;
using Events.Bus;
using Web.Controllers;
using Web.FacebookAuthentication;
using Web.Filters;
using Web.Layouts;
using Web.Repositories;

namespace Web {
    public class WebModule : Module {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).SingleInstance();
            builder.RegisterType(typeof(FriendRepository)).As(typeof(IFriendRepository));
            builder.RegisterType(typeof(FacebookStateRepository)).As(typeof(IFacebookDataRepository));
            builder.RegisterType(typeof(Authenticator)).As<IAuthenticator>();
            builder.RegisterType(typeof(AccountRepository)).Named<IAccountRepository>("BaseAccountRepository");
            builder.Register(c => new FacebookAccountRepository(c.ResolveNamed<IAccountRepository>("BaseAccountRepository"), c.Resolve<IFacebookDataRepository>(), c.Resolve<IEventBus>())).As(typeof(IAccountRepository));
            builder.RegisterType(typeof(AccountSessionRepository)).As(typeof(IAccountSessionRepository));

            builder.Register<IAccount>(c =>
            {
                var currentId = c.Resolve<IAccountSessionRepository>().GetCurrentId();
                return c.Resolve<IAccountRepository>().FindById(currentId);
            }).As<IAccount>();

            builder.RegisterType<GlobalMessageFilter>().As<IActionFilter>();
            builder.RegisterType<GoalContextFilter>().PropertiesAutowired();
            builder.RegisterType<AccountContextFilter>().PropertiesAutowired();
            builder.RegisterType<CurrentGroupContextFilter>().PropertiesAutowired();

            builder.RegisterAssemblyTypes(ThisAssembly).Where(t => t is LayoutAttribute).PropertiesAutowired();
        }
    }
}