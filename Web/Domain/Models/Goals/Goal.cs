using System;
using System.Collections.Generic;

namespace Domain.Models.Goals {
    public class Goal : IEntity {        
        public string Title { get; set; }
        public Guid Id { get; set; }
        public Guid BucketId { get; set; }
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }
        public string Type { get; set; }
        public IEnumerable<GoalLog> Logs { get; set; }
    }
}