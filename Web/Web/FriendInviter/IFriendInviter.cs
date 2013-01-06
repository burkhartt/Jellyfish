using System;
using System.Security.Principal;
using Web.Database;
using Web.Email;

namespace Web.FriendInviter {
    public interface IFriendInviter {
        void Invite(IIdentity identity, string emailAddress);
    }

    public class FriendInviter : IFriendInviter {
        private readonly IFriendInvitationRepository friendInvitationRepository;
        private readonly IEmailSender emailSender;

        public FriendInviter(IFriendInvitationRepository friendInvitationRepository, IEmailSender emailSender) {
            this.friendInvitationRepository = friendInvitationRepository;
            this.emailSender = emailSender;
        }

        public void Invite(IIdentity identity, string emailAddress) {
            var invitationToken = Guid.NewGuid();
            var friendInvitation = new FriendInvitation {
                Id = invitationToken,
                AccountId = identity.Name,
                EmailAddress = emailAddress
            };

            friendInvitationRepository.Add(friendInvitation);
            emailSender.SendInvitation(friendInvitation);
        }
    }
}