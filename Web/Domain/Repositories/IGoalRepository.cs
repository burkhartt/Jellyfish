using System;
using System.Collections.Generic;
using Domain.Models.Goals;

namespace Domain.Repositories {
    public interface IGoalRepository {
        IEnumerable<Goal> GetByAccountId(Guid bucketId, Guid accountId);
    }
}