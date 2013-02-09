using System;
using Entities;

namespace Events {
    public class GoalDomainEvent : DomainEvent {
        public Guid AccountId { get; set; }
        public virtual string GetMessage(Account account) {
            return string.Empty;
        }
    }
}