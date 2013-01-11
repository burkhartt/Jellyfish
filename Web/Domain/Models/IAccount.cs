using System;

namespace Domain.Models {
    public interface IAccount {
        Guid Id { get; }
        string FirstName { get; }
        string LastName { get; }
        string Picture { get; }
        string EmailAddress { get; }
    }
}