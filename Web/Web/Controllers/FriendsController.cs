using System.Web.Mvc;
using Web.FriendInviter;
using Web.Models;

namespace Web.Controllers {
    public class FriendsController : Controller {
        private readonly IFriendInviter friendInviter;

        public FriendsController(IFriendInviter friendInviter) {
            this.friendInviter = friendInviter;
        }

        public ActionResult Invite() {
            return View(new InviteFriendForm());
        }

        [HttpPost]
        public ActionResult Invite(InviteFriendForm inviteFriendForm) {
            if (!ModelState.IsValid) {
                return View(inviteFriendForm);
            }

            friendInviter.Invite(User.Identity, inviteFriendForm.EmailAddress);

            return RedirectToAction("Invite", new {SuccessMessage = "Friend invited"});
        }
    }
}