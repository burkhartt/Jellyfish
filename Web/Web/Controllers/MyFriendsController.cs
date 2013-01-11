using System.Web.Mvc;
using Domain;
using Domain.Models;
using Domain.Repositories;
using Web.Filters;
using Web.Models;
using Web.Repositories;

namespace Web.Controllers {
    [Authorized]
    public class MyFriendsController : Controller {
        private readonly IAccount account;
        private readonly IFriendRepository friendRepository;

        public MyFriendsController(IAccount account, IFriendRepository friendRepository) {
            this.account = account;
            this.friendRepository = friendRepository;
        }

        public ActionResult Listing() {
            var friends = friendRepository.GetFriends(account.Id);
            return View(friends);
        }
    }    
}