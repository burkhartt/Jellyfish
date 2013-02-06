using System;

namespace Events {
    public class GoalTargetNumberUpdatedDeltaEvent : DomainEvent {
        public decimal Delta { get; set; }
        public Guid AccountId { get; set; }
    }
}