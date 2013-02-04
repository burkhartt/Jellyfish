using System;
using System.Collections.Generic;
using Domain.Models.Goals;

namespace Domain.Repositories {
    public interface IGoalRepository {
        IEnumerable<Goal> AllByGroupId(Guid groupId, Guid parentGoalId);
        Goal GetById(Guid id);
    }
}