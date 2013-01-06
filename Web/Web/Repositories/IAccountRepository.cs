using System;
using Web.Models;

namespace Web.Repositories {
    public interface IAccountRepository : IRepository<Account> {
        void AddInvitedAccount(Account account);
        Account GetByEmailAddressAndPassword(string emailAddress, string password);
    }
}