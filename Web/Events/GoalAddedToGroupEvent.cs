using System;

namespace Events {
    public class GoalAddedToGroupEvent : DomainEvent {
        public Guid GoalId { get; set; }
        public Guid GroupId { get; set; }
    }
}