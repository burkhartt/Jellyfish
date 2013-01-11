using System;
using Domain.Models;
using Web.Attributes;

namespace Web.Models {
    public abstract class AccountModel {
        private Guid id;

        protected AccountModel(IAccount account) {
            id = account.Id;
            FirstName = account.FirstName;
            LastName = account.LastName;
            EmailAddress = account.EmailAddress;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        [Password]
        public string Password { get; set; }

        [Password]
        public string ConfirmPassword { get; set; }

        [Hidden]
        public virtual Guid Id {
            get { return id; }
            set { id = value; }
        }
    }
}