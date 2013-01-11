using System;
using System.Collections.Generic;

namespace Domain.Models.Accounts {
    public class Account : IAccount, IEntity {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual int FacebookId { get; set; }
        public virtual string Password { get; set; }
        public virtual string ConfirmPassword { get; set; }
        public virtual bool AccountConfirmed { get; set; }
        public virtual IEnumerable<Guid> Friends { get; set; }
        public string FullName {
            get {
                return !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName)
                           ? FirstName + " " + LastName
                           : EmailAddress;
            }
        }

        public virtual Guid Id { get; set; }
        public bool IsAFacebookAccount { get { return FacebookId > 0; } }
        public virtual string Picture { get; set; }
    }
}