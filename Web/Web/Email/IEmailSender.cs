using Web.FriendInviter;

namespace Web.Email {
    public interface IEmailSender {
        void SendInvitation(FriendInvitation friendInvitation);
    }

    public class EmailSender : IEmailSender {
        public void SendInvitation(FriendInvitation friendInvitation) {
            
        }
    }
}