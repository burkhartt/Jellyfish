using System;

namespace Events {
    [Serializable]
    public class AccountNameSetEvent : DomainEvent {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}