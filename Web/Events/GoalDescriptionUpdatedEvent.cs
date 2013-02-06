using System;

namespace Events {
    public class GoalDescriptionUpdatedEvent : DomainEvent {
        public string Description { get; set; }
        public Guid AccountId { get; set; }
    }
}