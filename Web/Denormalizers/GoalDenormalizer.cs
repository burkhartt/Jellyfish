using Domain.Models.Goals;
using Domain.Repositories;

namespace Denormalizers {
    public class GoalDenormalizer : GenericDenormalizer<Goal> {
        public GoalDenormalizer(IRepository<Goal> repository) : base(repository) {}
    }
}