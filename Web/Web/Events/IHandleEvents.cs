namespace Web.Events {
    public interface IHandleEvents<in T> where T : IEvent {
        void Handle(T @event);
    }
}