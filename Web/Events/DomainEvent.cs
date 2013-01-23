using System;

namespace Events {
    [Serializable]
    public class DomainEvent {
        public int Sequence { get; set; }
        public Guid Id { get; set; }
        public DateTime EventDate { get; set; }
    }
}