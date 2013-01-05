using Web.Events;
using Web.Models;
using Web.Repositories;

namespace Web.Controllers {
    public class GoalsController : CrudController<Goal> {
        public GoalsController(IEventBus eventBus, IRepository<Goal> repository) : base(eventBus, repository) {}        
    }
}