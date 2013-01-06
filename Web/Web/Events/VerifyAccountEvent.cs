using System;

namespace Web.Events {
    public class VerifyAccountEvent : IEvent {
        public Guid Id { get; set; }
        public string EmailAddress { get; set; }
    }
}