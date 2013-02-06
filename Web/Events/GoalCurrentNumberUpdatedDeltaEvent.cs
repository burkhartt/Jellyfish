using System;

namespace Events {
    public class GoalCurrentNumberUpdatedDeltaEvent : DomainEvent {
        public decimal Delta { get; set; }
        public Guid AccountId { get; set; }
    }
}