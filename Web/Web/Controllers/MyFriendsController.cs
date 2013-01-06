using System.Web.Mvc;
using Web.Filters;
using Web.Repositories;

namespace Web.Controllers {
    [Authorized]
    public class MyFriendsController : BaseController {
        private readonly IFriendRepository friendRepository;

        public MyFriendsController(IFriendRepository friendRepository) {
            this.friendRepository = friendRepository;
        }

        public ActionResult Listing() {
            var friends = friendRepository.GetFriends(User.Account.Id);            
            return View(friends);
        }
    }
}