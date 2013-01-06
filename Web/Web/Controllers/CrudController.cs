using System;
using System.Web.Mvc;
using Web.Events;
using Web.Events.Entity;
using Web.Models;
using Web.Repositories;

namespace Web.Controllers {
    public class CrudController<T> : Controller where T : IEntity, new() {
        private readonly IEventBus eventBus;
        private readonly IRepository<T> repository;

        public CrudController(IEventBus eventBus, IRepository<T> repository) {
            this.eventBus = eventBus;
            this.repository = repository;
        }

        public ViewResult Index(Guid id) {
            return View(repository.FindById(id));
        }

        public ViewResult Listing() {
            return View(repository.All());
        }

        public ViewResult Create() {
            var model = new T { Id = Guid.NewGuid() };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(T model) {
            if (!ModelState.IsValid) {
                return View(model);
            }
            eventBus.Send(new EntityCreatedEvent<T>(model));
            return RedirectToAction("Listing", new { SuccessMessage = typeof(T).Name + " created" });
        }

        public ViewResult Update(Guid id) {            
            return View(repository.FindById(id));
        }

        [HttpPost]
        public ActionResult Update(T model) {
            if (!ModelState.IsValid) {
                return View(model);
            }
            eventBus.Send(new EntityUpdatedEvent<T>(model));
            return RedirectToAction("Listing", new { SuccessMessage = typeof(T).Name + " updated" });
        }

        public ActionResult Delete(Guid id) {
            eventBus.Send(new EntityDeletedEvent<T>(id));
            return RedirectToAction("Listing", new { SuccessMessage = typeof(T).Name + " updated" });
        }
    }
}