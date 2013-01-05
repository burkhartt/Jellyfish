using Web.Controllers;
using Web.Models;

namespace Web.Events.Entity {
    public class EntityCreatedEvent<T> : IEvent where T : IEntity {
        public EntityCreatedEvent(T entity) {
            Entity = entity;
        }

        public T Entity { get; private set; }
    }
}