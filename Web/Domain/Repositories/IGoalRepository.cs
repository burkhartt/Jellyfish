using System;
using System.Collections.Generic;
using Domain.Models.Goals;

namespace Domain.Repositories {
    public interface IGoalRepository {
        IEnumerable<Goal> AllByGroupId(Guid groupId, Guid parentGoalId);
        IEnumerable<Goal> AllByGroupId(Guid groupId);
        Goal GetById(Guid id);
    }
}