using System;
using System.Web.Mvc;
using Domain.Models;
using Domain.Repositories;
using Events;
using Events.Bus;
using Web.Filters;

namespace Web.Controllers {
    [Authorized]
    public class GroupsController : Controller {
        private readonly IAccount account;
        private readonly IGroupRepository groupRepository;
        private readonly IEventBus eventBus;

        public GroupsController(IAccount account, IGroupRepository groupRepository, IEventBus eventBus) {
            this.account = account;
            this.groupRepository = groupRepository;
            this.eventBus = eventBus;
        }

        public ViewResult Index(Guid id) {
            return View(groupRepository.GetById(id));
        }

        public JsonResult Get() {
            return Json(groupRepository.GetByAccountId(account.Id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Create(string title) {
            var groupId = Guid.NewGuid();
            eventBus.Send(new GroupCreatedEvent { Id = groupId, Title = title });
            eventBus.Send(new AccountAddedToGroupEvent { GroupId = groupId, AccountId = account.Id });
            return Json(groupId);
        }        
    }    
}