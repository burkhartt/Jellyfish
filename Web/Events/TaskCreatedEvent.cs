using System;

namespace Events {
    public class TaskCreatedEvent : DomainEvent {
        public Guid GoalId { get; set; }
    }
}