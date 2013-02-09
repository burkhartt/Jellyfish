using System;
using Database;
using Domain.Models.Goals;

namespace Domain.Repositories {
    public class GroupGoalRepository : IGroupGoalRepository {
        private readonly IDatabase database;

        public GroupGoalRepository(IDatabase database) {
            this.database = database;
        }

        public GroupGoal GetByGoalId(Guid goalId) {
            return (GroupGoal)database.GetTheDatabase().GroupGoals.FindByGoalId(goalId);
        }
    }
}