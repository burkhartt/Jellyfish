﻿using System;
using System.Web.Mvc;
using Domain.Models;
using Domain.Repositories;
using Events;
using Events.Bus;
using Web.Filters;
using Web.Models;

namespace Web.Controllers {
    [Authorized]
    public class GoalsController : Controller {
        private readonly IEventBus eventBus;
        private readonly IGoalRepository goalRepository;
        private readonly IAccount account;

        public GoalsController(IEventBus eventBus, IGoalRepository goalRepository, IAccount account) {
            this.eventBus = eventBus;
            this.goalRepository = goalRepository;
            this.account = account;
        }

        public JsonResult Get(Guid bucketId) {
            return Json(goalRepository.GetByAccountId(bucketId, account.Id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Create(string goal, Guid bucketId) {
            var goalId = Guid.NewGuid();
            eventBus.Send(new GoalCreatedEvent {Id = goalId, Title = goal, AccountId = account.Id, BucketId = bucketId});
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