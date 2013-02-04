using System;
using System.Collections.Generic;
using Database;
using Domain.Models.Goals;

namespace Domain.Repositories {
    public class TaskRepository : Repository<Task>, ITaskRepository {
        private readonly IDatabase database;

        public TaskRepository(IDatabase database) : base(database) {
            this.database = database;
        }

        public IEnumerable<Task> AllByGoalId(Guid goalId) {
            return database.GetTheDatabase().Tasks.FindAllByGoalId(goalId).ToList<Task>();
        }
    }
}