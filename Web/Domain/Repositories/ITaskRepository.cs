using System;
using System.Collections.Generic;
using Domain.Models.Goals;

namespace Domain.Repositories {
    public interface ITaskRepository {
        IEnumerable<Task> AllByGoalId(Guid goalId);
    }    
}