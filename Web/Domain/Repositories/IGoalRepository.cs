using System;
using System.Collections.Generic;
using Database;
using Domain.Models.Goals;

namespace Domain.Repositories {
    public interface IGoalRepository {
        IEnumerable<Goal> AllByAccountId(Guid id);
    }

    public class GoalRepository : Repository<Goal>, IGoalRepository {
        private readonly IDatabase database;

        public GoalRepository(IDatabase database) : base(database) {
            this.database = database;
        }

        public IEnumerable<Goal> AllByAccountId(Guid id) {
            return database.GetTheDatabase().Goals.FindAllByAccountId(id).ToList<Goal>();
        }
    }
}