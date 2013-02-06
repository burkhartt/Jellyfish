using System;

namespace Events {
    public class GoalDeadlineUpdatedEvent : DomainEvent {
        public DateTime? Deadline { get; set; }
        public Guid AccountId { get; set; }
    }
}