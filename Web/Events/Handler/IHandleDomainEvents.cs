using Events.Events;

namespace Events.Handler {
    public interface IHandleDomainEvents<in T> where T : DomainEvent {
        void Handle(T @event);
    }
}