using System;

namespace Events {
    public class TaskCreatedEvent : DomainEvent {
        public string Title { get; set; }
        public Guid GoalId { get; set; }
    }
}