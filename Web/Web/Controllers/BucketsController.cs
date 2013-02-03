using System;
using System.Web.Mvc;
using Domain.Models;
using Domain.Repositories;
using Events;
using Events.Bus;
using Web.Filters;

namespace Web.Controllers {
    [Authorized]
    public class BucketsController : Controller {
        private readonly IAccount account;
        private readonly IBucketRepository bucketRepository;
        private readonly IEventBus eventBus;

        public BucketsController(IAccount account, IBucketRepository bucketRepository, IEventBus eventBus) {
            this.account = account;
            this.bucketRepository = bucketRepository;
            this.eventBus = eventBus;
        }

        public ViewResult Index(Guid id) {            
            return View(bucketRepository.GetById(id));
        }

        public JsonResult Get(Guid parentId) {
            return Json(bucketRepository.AllByAccountId(parentId, account.Id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Create(string title, Guid parentBucketId) {
            var bucketId = Guid.NewGuid();
            eventBus.Send(new BucketCreatedEvent {Id = bucketId, Title = title, AccountId = account.Id, ParentBucketId = parentBucketId});
            return Json(bucketId);
        }

        public JsonResult AddGoal(Guid goalId, Guid bucketId) {
            eventBus.Send(new GoalAddedToBucketEvent { GoalId = goalId, BucketId = bucketId });
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }    
}