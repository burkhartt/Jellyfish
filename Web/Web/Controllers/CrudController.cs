using System;
using System.Web.Mvc;
using Web.Repositories;

namespace Web.Controllers {
    public class CrudController<T> : Controller where T : class, new() {
        private readonly IRepository<T> repository;

        public CrudController(IRepository<T> repository) {
            this.repository = repository;
        }

        public ViewResult Create() {
            return View(new T());
        }

        [HttpPost]
        public ActionResult Create(T model) {
            if (!ModelState.IsValid) {
                return View(model);
            }

            return RedirectToAction("Index", "Home", new { SuccessMessage = typeof(T).Name + " created" });
        }

        public ViewResult Update(Guid id) {
            return View(repository.FindById(id));
        }

        [HttpPost]
        public ActionResult Update(T model) {
            if (!ModelState.IsValid) {
                return View(model);
            }
            repository.Update(model);
            return RedirectToAction("Index", "Home", new { SuccessMessage = typeof(T).Name + " updated" });
        }

        public ActionResult Delete(Guid id) {
            repository.Delete(id);
            return RedirectToAction("Index", "Home", new { SuccessMessage = typeof(T).Name + " updated" });
        }
    }
}