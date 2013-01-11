using System;
using Domain.Models;
using Events.Events;

namespace Events.Entities {
    public class EntityDeletedEvent<T> : IEvent where T : IEntity {
        public EntityDeletedEvent(Guid id) {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}