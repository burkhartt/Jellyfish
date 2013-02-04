using System;

namespace Domain.Models.Goals {
    public class Task : IEntity {
        public Guid Id { get; set; }
        public Guid GoalId { get; set; }
        public string Title { get; set; }
        public bool IsComplete { get; set; }
    }
}