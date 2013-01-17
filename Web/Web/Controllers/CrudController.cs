using System;
using System.Web.Mvc;
using Domain.Models;
using Domain.Repositories;
using Events.Bus;

namespace Web.Controllers {
    public class CrudController<T> : Controller where T : IEntity, new() {
        private readonly IEventBus eventBus;
        private readonly IRepository<T> repository;

        public CrudController(IEventBus eventBus, IRepository<T> repository) {
            this.eventBus = eventBus;
            this.repository = repository;
        }

        public virtual ViewResult Index(Guid id) {
            return View(repository.FindById(id));
        }

        public virtual ViewResult Listing() {
            return View(repository.All());
        }

        public virtual ViewResult Create() {
            var model = new T {Id = Guid.NewGuid()};
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult Create(T model) {
            if (!ModelState.IsValid) {
                return View(model);
            }
            
            return RedirectToAction("Listing", new {SuccessMessage = typeof (T).Name + " created"});
        }

        public virtual ViewResult Update(Guid id) {
            return View(repository.FindById(id));
        }

        [HttpPost]
        public virtual ActionResult Update(T model) {
            if (!ModelState.IsValid) {
                return View(model);
            }
            
            return RedirectToAction("Listing", new {SuccessMessage = typeof (T).Name + " updated"});
        }

        public virtual ActionResult Delete(Guid id) {
            
            return RedirectToAction("Listing", new {SuccessMessage = typeof (T).Name + " updated"});
        }
    }
}