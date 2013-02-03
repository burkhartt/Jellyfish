using System;

namespace Domain.Models.Goals {
    public class Bucket : IEntity {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public string Title { get; set; }
    }
}