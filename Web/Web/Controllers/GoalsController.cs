using System.Web.Mvc;
using Domain.Models.Goals;
using Domain.Repositories;
using Events;
using Events.Bus;
using Web.Filters;
using Web.Models;

namespace Web.Controllers {
    [Authorized]
    public class GoalsController : Controller {
        private readonly IEventBus eventBus;
        private readonly IRepository<Goal> goalRepository;

        public GoalsController(IEventBus eventBus, IRepository<Goal> goalRepository) {
            this.eventBus = eventBus;
            this.goalRepository = goalRepository;
        }

        public ActionResult Index() {
            return View("Listing", goalRepository.All());
        }

        public ActionResult Create() {
            return View(new GoalForm());
        }

        [HttpPost]
        public JsonResult Create(GoalForm goalForm) {
            if (!ModelState.IsValid) {
                return Json(false);
            }

            eventBus.Send(new GoalCreatedEvent {Id = goalForm.Id, Title = goalForm.Title});

            return Json(true);
        }

        [HttpPost]
        public JsonResult Update(GoalForm goalForm) {
            if (!ModelState.IsValid) {
                return Json(false);
            }

            eventBus.Send(new GoalUpdatedEvent {Id = goalForm.Id, Description = goalForm.Description});

            return Json(true);
        }
    }
}