using System;
using System.Collections.Generic;
using System.Linq;
using Web.Database;
using Web.Models;

namespace Web.Repositories {
    public class AccountRepository : Repository<Account>, IAccountRepository {
        private readonly IDatabase database;

        public AccountRepository(IDatabase database) : base(database) {
            this.database = database;
        }

        public void AddInvitedAccount(Account account) {
            database.GetTheDatabase().Account.Insert(account);
        }

        public Account GetByEmailAddressAndPassword(string emailAddress, string password) {
            return database.GetTheDatabase().Account.FindByEmailAddressAndPassword(EmailAddress: emailAddress, Password: password);
        }
    }
}