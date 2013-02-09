using System;
using System.Collections.Generic;
using Domain.Models;

namespace Domain.Repositories {
    internal interface IGoalLogRepository {
        IEnumerable<string> GetAllById(Guid id);
    }
}