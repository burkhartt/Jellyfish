using System;
using System.Collections.Generic;
using Attributes;
using Events;

namespace Domain.Models.Accounts {
    public class Account : IAccount {
        private readonly Guid id;

        public Account(Guid id) {
            this.id = id;
        }

        public Account() {
            id = Guid.NewGuid();
        }

        [NotEditable]
        public virtual int FacebookId { get; set; }

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
    }
}