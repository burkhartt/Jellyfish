using Autofac;
using CommandHandlers;
using Commands;

namespace CommandBus {
    public class CommandBus : ICommandBus {
        private readonly IComponentContext componentContext;

        public CommandBus(IComponentContext componentContext) {
            this.componentContext = componentContext;
        }

        public void Send<T>(T command) where T : ICommand {
            var commandHandler = (ICommandHandler<T>) componentContext.Resolve(typeof (ICommandHandler<T>));
            commandHandler.Handle(command);
        }
    }
}