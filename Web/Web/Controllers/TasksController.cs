using System;
using System.Web.Mvc;
using Domain.Repositories;
using Events;
using Events.Bus;
using Web.Filters;

namespace Web.Controllers {
    [Authorized]
    public class TasksController : Controller {
        private readonly IEventBus eventBus;
        private readonly ITaskRepository taskRepository;

        public TasksController(IEventBus eventBus, ITaskRepository taskRepository) {
            this.eventBus = eventBus;
            this.taskRepository = taskRepository;
        }

        public JsonResult Get(Guid goalId) {
            return Json(taskRepository.AllByGoalId(goalId), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Create(string task, Guid goalId) {
            var taskId = Guid.NewGuid();
            eventBus.Send(new TaskCreatedEvent { Id = taskId, Title = task, GoalId = goalId });
            return Json(taskId);
        }

        [HttpPost]
        public void StatusChanged(Guid taskId, bool isComplete) {
            eventBus.Send(new TaskStatusUpdatedEvent { Id = taskId, IsComplete = isComplete});
        }
    }    
}