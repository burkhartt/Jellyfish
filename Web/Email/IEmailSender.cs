using System;

namespace Email {
    public interface IEmailSender {
        void SendInvitation(Guid accountId, string emailAddress);
    }
}