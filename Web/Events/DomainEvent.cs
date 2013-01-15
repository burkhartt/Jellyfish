using System;

namespace Events {
    [Serializable]
    public class DomainEvent {
        public int Sequence { get; set; }
        public Guid AggregateRootId { get; set; }
        public DateTime EventDate { get; set; }
    }
}