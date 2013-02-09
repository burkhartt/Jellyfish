using System;
using System.Collections.Generic;
using System.Linq;
using Database;
using Domain.Models;
using Entities;

namespace Domain.Repositories {
    public class FriendRepository : IFriendRepository {
        private readonly IDatabase database;
        private readonly IAccountRepository accountRepository;

        public FriendRepository(IDatabase database, IAccountRepository accountRepository) {
            this.database = database;
            this.accountRepository = accountRepository;
        }

        public void AddFriend(Guid accountId, Guid friendId) {            
            database.GetTheDatabase().Friends.Insert(AccountId: accountId, FriendId: friendId);
            database.GetTheDatabase().Friends.Insert(AccountId: friendId, FriendId: accountId);
        }

        public IEnumerable<Account> GetFriends(Guid accountId) {
            List<Friend> friends = database.GetTheDatabase().Friends.FindAllByAccountId(accountId).ToList<Friend>();
            return friends.Select(x => accountRepository.FindById(x.FriendId));
        }
    }
}