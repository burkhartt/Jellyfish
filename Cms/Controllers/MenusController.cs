using CmsDomain;
using CmsDomain.Repositories;

namespace Cms.Controllers {
    public class MenusController : CrudController<Menu> {
        public MenusController(IRepository<Menu> repository) : base(repository) {}
    }
}