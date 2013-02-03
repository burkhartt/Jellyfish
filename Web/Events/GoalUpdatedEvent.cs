using System;

namespace Events {
    public class GoalUpdatedEvent : DomainEvent {
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }
    }
}