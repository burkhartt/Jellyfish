using System;
using System.Collections.Generic;
using System.Linq;
using Entities;

namespace Domain.Repositories {
    public class CachedFriendRepository : IFriendRepository {
        private readonly IFriendRepository friendRepository;
        private Dictionary<Guid, IList<Account>> cachedFriends; 

        public CachedFriendRepository(IFriendRepository friendRepository) {
            this.friendRepository = friendRepository;
            cachedFriends = new Dictionary<Guid, IList<Account>>();
        }

        public void AddFriend(Guid accountId, Guid friendId) {
            friendRepository.AddFriend(accountId, friendId);
        }

        public IEnumerable<Account> GetFriends(Guid accountId) {
            if (!cachedFriends.ContainsKey(accountId)) {
                var friends = friendRepository.GetFriends(accountId).ToList();
                cachedFriends[accountId] = friends;
            }            

            return cachedFriends[accountId];
        }
    }
}