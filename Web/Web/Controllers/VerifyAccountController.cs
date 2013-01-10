using System;
using System.Web.Mvc;
using Web.Events;

namespace Web.Controllers {
    public class VerifyAccountController : Controller {
        private readonly IEventBus eventBus;

        public VerifyAccountController(IEventBus eventBus) {
            this.eventBus = eventBus;
        }

        public ActionResult Index(Guid id, string emailAddress) {
            eventBus.Send(new VerifyAccountEvent {Id = id, EmailAddress = emailAddress});
            return RedirectToAction("Verified", "MyAccount");
        }
    }
}