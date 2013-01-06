using System.Collections.Generic;
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

        public IEnumerable<Account> GetAllUnconfirmedAccounts() {
            return database.GetTheDatabase().Account.FindAllByAccountConfirmed(AccountConfirmed: false).ToList<Account>();
        }
    }
}