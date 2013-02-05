using System;
using System.Web.Mvc;
using Domain.Models;
using Domain.Repositories;
using Events;
using Events.Bus;
using Web.Filters;

namespace Web.Controllers {
    [Authorized]
    public class GoalsController : Controller {
        private readonly IAccount account;
        private readonly IEventBus eventBus;
        private readonly IGoalRepository goalRepository;

        public GoalsController(IAccount account, IGoalRepository goalRepository, IEventBus eventBus) {
            this.account = account;
            this.goalRepository = goalRepository;
            this.eventBus = eventBus;
        }

        public ViewResult Index(Guid id) {
            return View(goalRepository.GetById(id));
        }

        public JsonResult Get(Guid groupId, Guid parentGoalId) {
            return Json(goalRepository.AllByGroupId(groupId, parentGoalId), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Create(string title, Guid groupId, Guid parentGoalId) {
            var goalId = Guid.NewGuid();
            eventBus.Send(new GoalCreatedEvent {
                Id = goalId,
                Title = title,
                AccountId = account.Id,
                ParentGoalId = parentGoalId
            });
            eventBus.Send(new GoalAddedToGroupEvent {GoalId = goalId, GroupId = groupId});
            return Json(goalId);
        }

        public JsonResult AddGoal(Guid goalId, Guid parentGoalId) {
            eventBus.Send(new GoalAddedToGoalEvent {GoalId = goalId, ParentGoalId = parentGoalId});
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void UpdateDescription(Guid id, string content) {
            eventBus.Send(new GoalDescriptionUpdatedEvent {Id = id, Description = content});
        }

        [HttpPost]
        public void UpdateDeadline(Guid id, DateTime? datetime) {
            eventBus.Send(new GoalDeadlineUpdatedEvent { Id = id, Deadline = datetime });
        }
    }
}