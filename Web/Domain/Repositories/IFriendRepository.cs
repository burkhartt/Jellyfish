using System;
using System.Collections.Generic;
using Entities;

namespace Domain.Repositories {
    public interface IFriendRepository {
        void AddFriend(Guid accountId, Guid friendId);
        IEnumerable<Account> GetFriends(Guid accountId);
    }
}