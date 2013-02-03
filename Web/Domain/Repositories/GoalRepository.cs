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

        public IEnumerable<Goal> AllOrphansByAccountId(Guid id) {
            IEnumerable<Goal> goals = database.GetTheDatabase().Goals.FindAllByAccountId(id).ToList<Goal>();
            return goals.Where(x => x.BucketId == Guid.Empty);
        }
    }
}