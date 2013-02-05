using System;
using System.Collections.Generic;
using System.Linq;
using Database;
using Domain.Models.Goals;

namespace Domain.Repositories {
    public class GoalRepository : Repository<Goal>, IGoalRepository {
        private readonly IDatabase database;

        public GoalRepository(IDatabase database) : base(database) {
            this.database = database;
        }

        public IEnumerable<Goal> AllByGroupId(Guid groupId, Guid parentGoalId) {
            IEnumerable<GroupGoal> groupGoals = database.GetTheDatabase().GroupGoals.FindAllByGroupId(groupId).ToList<GroupGoal>();
            var goalIds = groupGoals.Select(@group => @group.GoalId).ToList();
            IEnumerable<Goal> goals = database.GetTheDatabase().Goals.FindAllById(goalIds).ToList<Goal>();
            return goals.Where(x => x.BucketId == parentGoalId);
        }

        public Goal GetById(Guid id) {
            return (Goal)database.GetTheDatabase().Goals.FindById(id);
        }
    }
}