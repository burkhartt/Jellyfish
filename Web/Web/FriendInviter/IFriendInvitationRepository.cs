using Web.Database;

namespace Web.FriendInviter {
    public interface IFriendInvitationRepository {
        void Add(FriendInvitation friendInvitation);
    }

    public class FriendInvitationRepository : IFriendInvitationRepository {
        private readonly IDatabase database;

        public FriendInvitationRepository(IDatabase database) {
            this.database = database;
        }

        public void Add(FriendInvitation friendInvitation) {
            database.GetTheDatabase().FriendInvitations.Insert(friendInvitation);
        }
    }
}