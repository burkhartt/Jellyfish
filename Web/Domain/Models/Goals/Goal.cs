using System;

namespace Domain.Models.Goals {
    public class Goal : IEntity {        
        public string Title { get; set; }
        public Guid Id { get; set; }
        public Guid BucketId { get; set; }
    }
}