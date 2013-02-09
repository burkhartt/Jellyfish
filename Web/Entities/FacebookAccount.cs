namespace Entities {
    public class FacebookAccount : Account {
        public FacebookAccount(int id) : base() {
            this.FacebookId = id;
        }

        public FacebookAccount() : base() { }

        public int FacebookId { get; set; }
        public override bool AccountConfirmed { get { return true; } }
        public override string Picture { get; set; }
    }
}