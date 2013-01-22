using System;

namespace Domain.Models.Accounts {
    public class FacebookAccount : Account {
        private readonly Guid id;

        public FacebookAccount(Guid id) : base(id) {
            this.id = id;
        }

        public override bool AccountConfirmed { get { return true; } }
        public override Guid Id { get { return id; } }
        public override string Picture { get; set; }
    }
}