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

        public IEnumerable<Goal> GetByAccountId(Guid bucketId, Guid accountId) {
            IEnumerable<Goal> goals = database.GetTheDatabase().Goals.FindAllByAccountId(accountId).ToList<Goal>();
            return goals.Where(x => x.BucketId == bucketId);
        }
    }
}