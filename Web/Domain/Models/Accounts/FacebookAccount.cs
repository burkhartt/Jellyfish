using System;

namespace Domain.Models.Accounts {
    public class FacebookAccount : Account {
        private readonly IAccount account;
        public FacebookAccount() {}

        public FacebookAccount(IAccount account) {
            this.account = account;
        }

        public FacebookAccount(int facebookId) {
            FacebookId = facebookId;
        }

        public override bool AccountConfirmed { get { return true; } }
        public override Guid Id { get { return account.Id; } }
        public override string Picture { get; set; }
    }
}