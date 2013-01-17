using System;

namespace Domain.Models.Accounts {
    public class FacebookAccount : Account {
        private readonly IAccount account;

        public FacebookAccount(Guid id) : base(id) {}
        public override bool AccountConfirmed { get { return true; } }
        public override Guid Id { get { return account.Id; } }
        public override string Picture { get; set; }
    }
}