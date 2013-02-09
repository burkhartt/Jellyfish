using System;
using System.Web.Mvc;
using Domain.Models;
using Domain.Models.Goals;
using Domain.Repositories;
using Entities;
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
            eventBus.Send(new GoalAddedToGroupEvent { Id = goalId, GroupId = groupId, AccountId = account.Id});
            return Json(goalId);
        }

        public JsonResult AddGoal(Guid goalId, Guid parentGoalId) {
            eventBus.Send(new GoalAddedToGoalEvent {GoalId = goalId, ParentGoalId = parentGoalId, AccountId = account.Id});
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void UpdateDescription(Guid id, string content) {
            eventBus.Send(new GoalDescriptionUpdatedEvent {Id = id, Description = content, AccountId = account.Id});
        }

        [HttpPost]
        public void UpdateDeadline(Guid id, DateTime? datetime) {
            eventBus.Send(new GoalDeadlineUpdatedEvent { Id = id, Deadline = datetime, AccountId = account.Id });
        }

        [HttpPost]
        public void UpdateType(Guid id, string type) {
            eventBus.Send(new GoalTypeUpdatedEvent { Id = id, Type = type, AccountId = account.Id });
        }

        [HttpPost]
        public void UpdateCurrentNumber(Guid id, decimal number) {
            var goal = (QuantitativeGoal)goalRepository.GetById(id);
            var difference = number - goal.CurrentNumber;
            eventBus.Send(new GoalCurrentNumberUpdatedEvent { Id = id, Number = number, AccountId = account.Id });
            eventBus.Send(new GoalCurrentNumberUpdatedDeltaEvent { Id = id, Delta = difference, AccountId = account.Id });
        }

        [HttpPost]
        public void UpdateTargetNumber(Guid id, decimal number) {
            var goal = (QuantitativeGoal)goalRepository.GetById(id);
            var difference = number - goal.TargetNumber;
            eventBus.Send(new GoalTargetNumberUpdatedEvent { Id = id, Number = number, AccountId = account.Id});
            eventBus.Send(new GoalTargetNumberUpdatedDeltaEvent { Id = id, Delta = difference, AccountId = account.Id });
        }
    }    
}