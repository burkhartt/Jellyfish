﻿using System;
using System.Collections.Generic;
using System.Linq;
using Web.Database;
using Web.Models;

namespace Web.Repositories {
    public interface IFriendRepository {
        void AddFriend(Guid accountId, Guid friendId);
        IEnumerable<Account> GetFriends(Guid accountId);
    }

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