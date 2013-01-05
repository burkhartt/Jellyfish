using System;
using System.Web.Mvc;
using Web.Events;
using Web.Events.Entity;
using Web.Models;
using Web.Repositories;

namespace Web.Controllers {
    public class CrudController<T> : Controller where T : IEntity, new() {
        private readonly IEventBus<T> eventBus;
        private readonly IRepository<T> repository;

        public CrudController(IEventBus<T> eventBus, IRepository<T> repository) {
            this.eventBus = eventBus;
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
            eventBus.Send(new EntityCreatedEvent<T>(model));
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
            eventBus.Send(new EntityUpdatedEvent<T>(model));
            return RedirectToAction("Index", "Home", new { SuccessMessage = typeof(T).Name + " updated" });
        }

        public ActionResult Delete(Guid id) {
            eventBus.Send(new EntityDeletedEvent<T>(id));
            return RedirectToAction("Index", "Home", new { SuccessMessage = typeof(T).Name + " updated" });
        }
    }
}