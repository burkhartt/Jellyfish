using Web.Attributes;
using Web.Events;
using Web.Models;
using Web.Repositories;

namespace Web.Controllers {
    public class GoalsController : CrudController<Goal> {
        public GoalsController(IEventBus<Goal> eventBus, IRepository<Goal> repository) : base(eventBus, repository) {}
    }
}