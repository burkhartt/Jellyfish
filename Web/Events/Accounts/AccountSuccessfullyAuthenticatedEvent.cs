using System;
using Events.Events;

namespace Events.Accounts {
    public class AccountSuccessfullyAuthenticatedEvent : DomainEvent
    {
        public Guid Id { get; set; }
    }
}