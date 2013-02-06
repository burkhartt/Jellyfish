using System;

namespace Events {
    public class GoalTypeUpdatedEvent : DomainEvent {
        public string Type { get; set; }
        public Guid AccountId { get; set; }
    }
}