using System;
using System.Collections.Generic;
using Web.Attributes;

namespace Web.Models {
    public class Account : IEntity {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }

        [Password]
        public string Password { get; set; }

        [Password]
        public string ConfirmPassword { get; set; }

        public bool AccountConfirmed { get; set; }

        public IEnumerable<Guid> Friends { get; set; }

        public string FullName {
            get {
                return !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName)
                           ? FirstName + " " + LastName
                           : EmailAddress;
            }
        }

        [Hidden]
        public Guid Id { get; set; }
    }
}