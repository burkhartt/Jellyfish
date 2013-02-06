using System;

namespace Events {
    [Serializable]
    public class DomainEvent {
        public DomainEvent() {
            this.EventDate = DateTime.Now;
        }

        public int Sequence { get; set; }
        public Guid Id { get; set; }
        public DateTime EventDate { get; set; }
    }
}