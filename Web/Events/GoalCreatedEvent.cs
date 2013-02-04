using System;

namespace Events {
    public class GoalCreatedEvent : DomainEvent {
        public string Title { get; set; }
        public Guid AccountId { get; set; }
        public Guid ParentGoalId { get; set; }
    }
}