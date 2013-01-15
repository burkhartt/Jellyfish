using Email;
using Events.Friends;
using Events.Handler;

namespace Denormalizers {
    public class FriendInvitationDenormalizer : IHandleDomainEvents<InviteFriendEvent> {
        private readonly IEmailSender emailSender;

        public FriendInvitationDenormalizer(IEmailSender emailSender) {
            this.emailSender = emailSender;
        }

        public void Handle(InviteFriendEvent @event) {
            emailSender.SendInvitation(@event.FriendId, @event.EmailAddress);
        }
    }
}