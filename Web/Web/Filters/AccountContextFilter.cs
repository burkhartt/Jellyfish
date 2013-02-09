using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Domain.Repositories;
using Entities;

namespace Web.Filters {
    public class AccountContextFilter : IActionFilter {
        private Guid id;
        public IGoalRepository GoalRepository { get; set; }
        public IGroupGoalRepository GroupGoalRepository { get; set; }
        public IAccountRepository AccountRepository { get; set; }
        public IGroupRepository GroupRepository { get; set; }
        public IGroupMemberRepository GroupMemberRepository { get; set; }

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
                group = GroupRepository.GetById(groupGoal.GroupId);
            }

            if (group == null) return;

            var groupMembers = GroupMemberRepository.GetAllByGroupId(group.Id);
            var accounts = groupMembers.Select(groupMember => AccountRepository.FindById(groupMember.AccountId)).ToList();

            filterContext.HttpContext.Items["GroupMembers"] = accounts;
        }
    }
}