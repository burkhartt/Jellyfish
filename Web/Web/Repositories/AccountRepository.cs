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

        public void AddFriend(Guid id, Guid friendId) {
            var account = FindById(id);
            var listOfFriends = new List<Guid>();

            if (account.Friends != null) {
                listOfFriends = account.Friends.ToList();
            }
            
            listOfFriends.Add(friendId);
            account.Friends = listOfFriends.ToArray();
            database.GetTheDatabase().Account.Update(account);
        }
    }
}