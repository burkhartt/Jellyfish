using Web.Controllers;
using Web.Models;

namespace Web.Events.Entity {
    public class EntityUpdatedEvent<T> : IEvent where T : IEntity {
        public EntityUpdatedEvent(IEntity model) {}
    }
}