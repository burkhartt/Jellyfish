using System;
using Commands;
using Domain;
using Domain.Models.Accounts;

namespace CommandHandlers {
    public class CreateAccountCommandHandler : ICommandHandler<CreateAccountCommand> {
        private readonly IDomainRepository domainRepository;

        public CreateAccountCommandHandler(IDomainRepository domainRepository) {
            this.domainRepository = domainRepository;
        }

        public void Handle(CreateAccountCommand command) {
            var account = new Account(Guid.NewGuid());
            account.SetName(command.FirstName, command.LastName);
            domainRepository.Save(account);
        }
    }
}