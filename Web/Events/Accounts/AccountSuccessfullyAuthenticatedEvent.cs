using System;
using Events.Events;

namespace Events.Accounts {
    public class AccountSuccessfullyAuthenticatedEvent : IEvent {
        public Guid Id { get; set; }
    }
}