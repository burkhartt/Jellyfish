using System;

namespace Events {
    public class GoalTargetNumberUpdatedEvent : DomainEvent {
        public decimal Number { get; set; }
        public Guid AccountId { get; set; }
    }
}