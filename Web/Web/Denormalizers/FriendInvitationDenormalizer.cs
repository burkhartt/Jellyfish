using Web.Email;
using Web.Events;

namespace Web.Denormalizers {
    public class FriendInvitationDenormalizer : IHandleEvents<InviteFriendEvent> {
        private readonly IEmailSender emailSender;

        public FriendInvitationDenormalizer(IEmailSender emailSender) {
            this.emailSender = emailSender;
        }

        public void Handle(InviteFriendEvent @event) {
            emailSender.SendInvitation(@event.FriendId, @event.EmailAddress);
        }
    }
}