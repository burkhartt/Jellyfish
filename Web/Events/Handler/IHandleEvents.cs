using Events.Events;

namespace Events.Handler {
    public interface IHandleEvents<in T> where T : IEvent {
        void Handle(T @event);
    }
}