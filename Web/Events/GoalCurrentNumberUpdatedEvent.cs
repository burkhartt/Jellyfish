using System;

namespace Events {
    public class GoalCurrentNumberUpdatedEvent : DomainEvent {
        public decimal Number { get; set; }
        public Guid AccountId { get; set; }
    }
}