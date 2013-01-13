using System;
using Should;

namespace GoalEngine.Tests {
    public static class ParsedGoalExtensions {
        public static void ShouldEqual(this ParsedGoal goal, decimal? quantity, GoalDirection direction, DateTime? deadline) {
            goal.Quantity.ShouldEqual(quantity);
            goal.Direction.ShouldEqual(direction);
            goal.Deadline.ShouldEqual(deadline);
        }
    }
}