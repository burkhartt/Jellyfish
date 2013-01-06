using Web.Events;
using Web.Filters;
using Web.Models;
using Web.Repositories;

namespace Web.Controllers {
    [Authorized]
    public class AccountsController : CrudController<Account> {
        public AccountsController(IEventBus eventBus, IRepository<Account> repository) : base(eventBus, repository) {}
    }
}