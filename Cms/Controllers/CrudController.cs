using System;
using System.Linq;
using System.Web.Mvc;
using CmsDomain;
using CmsDomain.Repositories;

namespace Cms.Controllers {
    public class CrudController<T> : Controller where T : CmsEntity, new() {
        private readonly IRepository<T> repository;

        public CrudController(IRepository<T> repository) {
            this.repository = repository;
        }

        public ActionResult Index() {
            return View(repository.All().ToList());
        }

        public ActionResult Create() {
            return View(new T {Id = Guid.NewGuid()});
        }

        [HttpPost]
        public ActionResult Create(T model) {
            repository.Create(model);
            return RedirectToAction("Index");
        }

        public ActionResult Update(Guid id) {
            return View(repository.FindById(id));
        }

        [HttpPost]
        public ActionResult Update(T model) {
            repository.Update(model);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(Guid id) {
            repository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}