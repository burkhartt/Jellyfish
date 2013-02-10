using System;
using System.Web.Mvc;
using Domain.Repositories;
using Entities;
using Events;
using Events.Bus;
using Web.Filters;

namespace Web.Controllers {
    [Authorized]
    public class TasksController : Controller {
        private readonly IEventBus eventBus;
        private readonly ITaskRepository taskRepository;
        private readonly IAccount account;

        public TasksController(IEventBus eventBus, ITaskRepository taskRepository, IAccount account) {
            this.eventBus = eventBus;
            this.taskRepository = taskRepository;
            this.account = account;
        }

        public JsonResult Get(Guid goalId) {
            return Json(taskRepository.AllByGoalId(goalId), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Create(string task, Guid goalId) {
            var taskId = Guid.NewGuid();
            eventBus.Send(new TaskCreatedEvent { Id = taskId, GoalId = goalId, AccountId = account.Id});
            eventBus.Send(new TaskTitleUpdatedEvent { Id = taskId, GoalId = goalId, Title = task, AccountId = account.Id});
            return Json(taskId);
        }

        [HttpPost]
        public void StatusChanged(Guid taskId, bool isComplete) {
            var task = taskRepository.GetById(taskId);
            eventBus.Send(new TaskStatusUpdatedEvent { Id = taskId, IsComplete = isComplete, AccountId = account.Id, GoalId = task.GoalId });
        }

        [HttpPost]
        public void UpdateTitle(Guid id, string content) {
            var task = taskRepository.GetById(id);
            eventBus.Send(new TaskTitleUpdatedEvent { Id = id, Title = content, AccountId = account.Id, GoalId = task.GoalId});
        }
    }    
}