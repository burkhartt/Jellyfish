using System;
using Events.Events;

namespace Events.Accounts {
    public class VerifyAccountEvent : DomainEvent
    {
        public Guid Id { get; set; }
        public string EmailAddress { get; set; }
    }
}