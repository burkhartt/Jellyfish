using System;
using System.Collections.Generic;
using Web.Attributes;

namespace Web.Models {
    public interface IAccount {
        Guid Id { get; }
        string FirstName { get; }
        string LastName { get; }
        string Picture { get; }
    }

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

    public class Account : IAccount, IEntity {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        
        [DoNotDisplay]
        public int FacebookId { get; set; }

        [Password]
        public string Password { get; set; }

        [Password]
        public string ConfirmPassword { get; set; }

        [DoNotDisplay]
        public virtual bool AccountConfirmed { get; set; }

        [DoNotDisplay]
        public IEnumerable<Guid> Friends { get; set; }

        [DoNotDisplay]
        public string FullName {
            get {
                return !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName)
                           ? FirstName + " " + LastName
                           : EmailAddress;
            }
        }

        [Hidden]
        public virtual Guid Id { get; set; }

        [DoNotDisplay]
        public bool IsAFacebookAccount { get { return FacebookId > 0; } }

        [DoNotDisplay]
        public virtual string Picture { get; set; }
    }
}