using Commands;

namespace CommandBus {
    public interface ICommandBus {
        void Send<T>(T command) where T : ICommand;
    }
}