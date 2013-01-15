using Events.Events;

namespace Events.Bus {
    public interface IEventBus {
        void Send(DomainEvent @event);
    }
}