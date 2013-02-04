using System;

namespace Events {
    public class GoalAddedToGoalEvent : DomainEvent {
        public Guid GoalId { get; set; }
        public Guid ParentGoalId { get; set; }
    }
}