using System;
using System.Linq;
using System.Web.Mvc;
using Domain.Repositories;
using Web.Extensions;

namespace Web.Filters {
    public class GoalViewBagFilter : IActionFilter {
        private Guid id;
        public IGoalRepository GoalRepository { get; set; }
        public IGroupGoalRepository GroupGoalRepository { get; set; }

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

            var goals = GoalRepository.AllByGroupId(id);
            if (!goals.Any()) {
                var groupGoal = GroupGoalRepository.GetByGoalId(id);
                goals = GoalRepository.AllByGroupId(groupGoal.GroupId);
            }

            filterContext.HttpContext.Items["Goals"] = goals;            
        }
    }
}