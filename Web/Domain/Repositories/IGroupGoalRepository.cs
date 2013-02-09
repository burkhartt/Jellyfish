using System;
using Domain.Models.Goals;

namespace Domain.Repositories {
    public interface IGroupGoalRepository {
        GroupGoal GetByGoalId(Guid goalId);
    }
}