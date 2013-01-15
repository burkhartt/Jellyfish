using Domain.Models;
using Events.Events;

namespace Events.Entities {
    public class EntityUpdatedEvent<T> : DomainEvent where T : IEntity
    {
        public EntityUpdatedEvent(T entity) {
            Entity = entity;
        }

        public T Entity { get; private set; }
    }
}