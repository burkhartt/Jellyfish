using System;

namespace Web.Events {
    public class AccountSuccessfullyAuthenticatedEvent : IEvent {
        public Guid Id { get; set; }
    }
}