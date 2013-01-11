using System;
using System.Collections.Generic;
using Domain.Models.Accounts;

namespace Domain.Repositories {
    public interface IFriendRepository {
        void AddFriend(Guid accountId, Guid friendId);
        IEnumerable<Account> GetFriends(Guid accountId);
    }
}