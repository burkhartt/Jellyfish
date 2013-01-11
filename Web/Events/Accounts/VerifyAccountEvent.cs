using System;
using Events.Events;

namespace Events.Accounts {
    public class VerifyAccountEvent : IEvent {
        public Guid Id { get; set; }
        public string EmailAddress { get; set; }
    }
}