using System;
using Web.Models;

namespace Web.Repositories {
    public interface IAccountRepository : IRepository<Account> {
        void AddInvitedAccount(Account account);
        void AddFriend(Guid id, Guid friendId);
    }
}