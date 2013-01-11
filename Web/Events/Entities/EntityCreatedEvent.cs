using Domain.Models;
using Events.Events;

namespace Events.Entities {
    public class EntityCreatedEvent<T> : IEvent where T : IEntity {
        public EntityCreatedEvent(T entity) {
            Entity = entity;
        }

        public T Entity { get; private set; }
    }
}