namespace Entities {
    public sealed class FacebookAccount : Account {
        public FacebookAccount(int id) : base() {
            FacebookId = id;
        }

        public FacebookAccount() : base() { }

        public override bool AccountConfirmed { get { return true; } }
        public override string Picture { get; set; }
    }
}