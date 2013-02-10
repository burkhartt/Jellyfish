using System;
using System.Collections.Generic;
using Attributes;

namespace Entities {
    public class Account : IAccount {
        public Account(Guid id) {
            Id = id;
        }

        public Account() {
            Id = Guid.NewGuid();
        }

        [NotEditable]
        public virtual long FacebookId { get; set; }

        public virtual string Password { get; set; }
        public virtual string ConfirmPassword { get; set; }

        [NotEditable]
        public virtual bool AccountConfirmed { get; set; }

        [NotEditable]
        public virtual IEnumerable<Guid> Friends { get; set; }

        [NotEditable]
        public string FullName {
            get {
                return !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName)
                           ? FirstName + " " + LastName
                           : EmailAddress;
            }
        }

        [NotEditable]
        public bool IsAFacebookAccount {
            get { return FacebookId > 0; }
        }

        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string EmailAddress { get; set; }

        [NotEditable]
        public virtual Guid Id { get; set; }

        [NotEditable]
        public virtual string Picture { get; set; }

        public DateTime? FacebookFriendListImported { get; set; }

        public virtual bool IsNull() {
            return false;
        }
    }
}