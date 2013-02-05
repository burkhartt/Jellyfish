using System.Collections.Generic;
using Domain.Models.Goals;

namespace Domain.Repositories {
    public class GoalTypeRepository : IGoalTypeRepository {
        public IEnumerable<string> GetAll() {
            return new[] {
                "Basic",
                "Quantitative"
            };
        }
    }
}