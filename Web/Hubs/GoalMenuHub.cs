using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Domain.Models.Goals;
using Domain.Repositories;
using Microsoft.AspNet.SignalR;
using ServiceStack.Text;

namespace Hubs {
    public class GoalMenuHub : Hub {
        public IEnumerable<Goal> ShowGoals() {
            var goalRepository = DependencyResolver.Current.GetService<IGoalRepository>();
            return goalRepository.AllByGroupId(new Guid("3cdbbaec-9aa0-4ebf-aec1-6ad6ddcb4ae0"), Guid.Empty);
        }
    }
}