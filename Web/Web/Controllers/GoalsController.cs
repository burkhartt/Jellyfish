using Domain.Models.Goals;
using Domain.Repositories;
using Events.Bus;
using Web.Filters;

namespace Web.Controllers {
    [Authorized]
    public class GoalsController : CrudController<Goal> {
        public GoalsController(IEventBus eventBus, IRepository<Goal> repository) : base(eventBus, repository) {}        
    }
}