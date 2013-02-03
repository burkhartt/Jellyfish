using System;

namespace Events {
    public class GoalAddedToBucketEvent : DomainEvent {
        public Guid GoalId { get; set; }
        public Guid BucketId { get; set; }
    }
}