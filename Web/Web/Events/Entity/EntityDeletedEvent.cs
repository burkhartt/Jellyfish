﻿using System;
using Web.Models;

namespace Web.Events.Entity {
    public class EntityDeletedEvent<T> : IEvent where T : IEntity {
        public EntityDeletedEvent(Guid id) {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}