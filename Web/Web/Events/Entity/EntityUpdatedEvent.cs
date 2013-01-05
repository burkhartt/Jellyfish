using Web.Models;

namespace Web.Events.Entity {
    public class EntityUpdatedEvent<T> : IEvent where T : IEntity {
        public EntityUpdatedEvent(T entity) {
            Entity = entity;
        }

        public T Entity { get; private set; }
    }
}