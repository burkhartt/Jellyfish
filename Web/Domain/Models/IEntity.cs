using System;

namespace Domain.Models {
    public interface IEntity {
        Guid Id { get; set; }
    }
}