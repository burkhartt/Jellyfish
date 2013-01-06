using System;
using System.Web.Mvc;
using Web.Events;
using Web.Repositories;

namespace Web.Controllers {
    public class VerifyAccountController : BaseController {
        private readonly IEventBus eventBus;

        public VerifyAccountController(IEventBus eventBus) {
            this.eventBus = eventBus;
        }

        public ActionResult Index(Guid id, string emailAddress) {
            eventBus.Send(new VerifyAccountEvent { Id = id, EmailAddress = emailAddress});
            return RedirectToAction("Verified", "MyAccount");
        }
    }
}