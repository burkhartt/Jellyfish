using Web.Events;
using Web.Models;
using Web.Repositories;

namespace Web.Denormalizers {
    public class GoalDenormalizer : GenericDenormalizer<Goal> {
        public GoalDenormalizer(IRepository<Goal> repository) : base(repository) {}
    }

    public class AccountDenormalizer : GenericDenormalizer<Account> {
        public AccountDenormalizer(IRepository<Account> repository) : base(repository) {}
    }
}