using System;
using System.Linq;
using System.Web.Mvc;
using Domain.Repositories;

namespace Web.Filters {
    public class CurrentGroupContextFilter : IActionFilter {
        private Guid id;
        public IGoalRepository GoalRepository { get; set; }
        public IGroupGoalRepository GroupGoalRepository { get; set; }
        public IGroupRepository GroupRepository { get; set; }

        public void OnActionExecuting(ActionExecutingContext filterContext) {
            if (!filterContext.ActionParameters.ContainsKey("id")) {
                id = Guid.Empty;
                return;
            }
            id = (Guid) filterContext.ActionParameters["id"];
        }

        public void OnActionExecuted(ActionExecutedContext filterContext) {
            if (id == Guid.Empty) {
                return;
            }

            var group = GroupRepository.GetById(id);
            if (group == null) {
                var groupGoal = GroupGoalRepository.GetByGoalId(id);
                if (groupGoal == null) return;
                group = GroupRepository.GetById(groupGoal.GroupId);
            }            

            filterContext.HttpContext.Items["CurrentGroup"] = group;
        }
    }
}