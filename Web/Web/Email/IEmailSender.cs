using System;

namespace Web.Email {
    public interface IEmailSender {
        void SendInvitation(Guid accountId, string emailAddress);
    }

    public class EmailSender : IEmailSender {
        public void SendInvitation(Guid accountId, string emailAddress) {}
    }
}