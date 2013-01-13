using System;

namespace GoalEngine {
    public class ParsedGoal {
        public decimal? Quantity { get; internal set; }

        public GoalDirection Direction { get; set; }

        public DateTime? Deadline { get; set; }
    }
}