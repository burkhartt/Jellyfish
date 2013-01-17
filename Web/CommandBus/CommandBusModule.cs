using Autofac;
using CommandHandlers;

namespace CommandBus {
    public class CommandBusModule : Module {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(CreateAccountCommandHandler).Assembly)
                   .Where(t => t.Name.EndsWith("CommandHandler"))
                   .AsImplementedInterfaces();

            builder.RegisterType(typeof(CommandBus)).As<ICommandBus>();
        }
    }
}