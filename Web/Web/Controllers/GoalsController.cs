using Web.Events;
using Web.Filters;
using Web.Models;
using Web.Repositories;

namespace Web.Controllers {
    [Authorized]
    public class GoalsController : CrudController<Goal> {
        public GoalsController(IEventBus eventBus, IRepository<Goal> repository) : base(eventBus, repository) {}        
    }
}