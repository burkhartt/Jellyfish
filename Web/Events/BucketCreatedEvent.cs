using System;

namespace Events {
    public class BucketCreatedEvent : DomainEvent {
        public string Title { get; set; }
        public Guid AccountId { get; set; }
        public Guid ParentBucketId { get; set; }
    }
}