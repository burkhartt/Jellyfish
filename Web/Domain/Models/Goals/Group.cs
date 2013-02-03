using System;

namespace Domain.Models.Goals {
    public class Group : IEntity {
        public Guid Id { get; set; }
        public string Title { get; set; }
    }
}