using System;
using System.Collections.Generic;
using Events;
using Events.Handler;

namespace Denormalizers {
    public class AccountReportDenormalizer : IHandleDomainEvents<AccountCreatedEvent>,
    IHandleDomainEvents<AccountNameSetEvent> {
        private readonly Dictionary<Guid, string> dictionary;

        public AccountReportDenormalizer() {
            dictionary = new Dictionary<Guid, string>();
        }

        public void Handle(AccountCreatedEvent @event) {
            dictionary[@event.Id] = "";
        }

        public void Handle(AccountNameSetEvent @event) {
            dictionary[@event.Id] = @event.FirstName + " " + @event.LastName;
        }
    }
}