using System.Collections.Generic;
using Domain.Models.Goals;

namespace Domain.Repositories {
    public interface IGoalTypeRepository {
        IEnumerable<string> GetAll();
    }
}