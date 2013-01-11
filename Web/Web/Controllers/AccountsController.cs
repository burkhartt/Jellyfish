using Domain.Models.Accounts;
using Domain.Repositories;
using Events.Bus;
using Web.Models;

namespace Web.Controllers {
    public class AccountsController : CrudController<Account> {
        public AccountsController(IEventBus eventBus, IRepository<Account> repository) : base(eventBus, repository) {}
    }
}