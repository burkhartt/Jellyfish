using CmsDomain;
using CmsDomain.Repositories;

namespace Cms.Controllers {
    public class PagesController : CrudController<Page> {
        public PagesController(IRepository<Page> repository) : base(repository) {}
    }
}