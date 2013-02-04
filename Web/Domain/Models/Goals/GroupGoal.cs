using System;

namespace Domain.Models.Goals {
    public class GroupGoal {
        public Guid GroupId { get; set; }
        public Guid GoalId { get; set; }
    }
}