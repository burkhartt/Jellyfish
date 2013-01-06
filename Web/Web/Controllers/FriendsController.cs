using System;
using System.Web.Mvc;
using Web.Events;
using Web.Models;

namespace Web.Controllers {
    public class FriendsController : Controller {
        private readonly IEventBus eventBus;

        public FriendsController(IEventBus eventBus) {
            this.eventBus = eventBus;
        }

        public ActionResult Invite() {
            return View(new InviteFriendForm());
        }

        [HttpPost]
        public ActionResult Invite(InviteFriendForm inviteFriendForm) {
            if (!ModelState.IsValid) {
                return View(inviteFriendForm);
            }

            eventBus.Send(new InviteFriendEvent { Id = Guid.Parse(User.Identity.Name), FriendId = Guid.NewGuid(), EmailAddress = inviteFriendForm.EmailAddress });

            return RedirectToAction("Invite", new {SuccessMessage = "Friend invited"});
        }
    }
}