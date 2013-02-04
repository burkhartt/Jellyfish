using System;
using System.Web.Mvc;
using Domain.Models;
using Domain.Repositories;
using Events;
using Events.Bus;
using Web.Filters;
using Web.Models;

namespace Web.Controllers {
    [Authorized]
    public class TasksController : Controller {
        private readonly IEventBus eventBus;
        private readonly IGoalRepository goalRepository;
        private readonly IAccount account;

        public TasksController(IEventBus eventBus, IGoalRepository goalRepository, IAccount account) {
            this.eventBus = eventBus;
            this.goalRepository = goalRepository;
            this.account = account;
        }

        public JsonResult Get(Guid groupId, Guid bucketId) {
            return Json(goalRepository.AllByGroupId(groupId, bucketId), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Create(string goal, Guid parentGoalId) {
            var goalId = Guid.NewGuid();
            eventBus.Send(new GoalCreatedEvent {Id = goalId, Title = goal, AccountId = account.Id, ParentGoalId = parentGoalId});
            return Json(goalId);
        }

        [HttpPost]
        public JsonResult Update(GoalForm goalForm) {
            if (!ModelState.IsValid) {
                return Json(false);
            }

            eventBus.Send(new GoalUpdatedEvent {Id = goalForm.Id, Description = goalForm.Description, Deadline = goalForm.Deadline });

            return Json(true);
        }
    }
}