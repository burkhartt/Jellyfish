using Web.Attributes;
using Web.Models;
using Web.Repositories;

namespace Web.Controllers {
    public class GoalsController : CrudController<Goal> {
        public GoalsController(IRepository<Goal> repository) : base(repository) {}
    }
}