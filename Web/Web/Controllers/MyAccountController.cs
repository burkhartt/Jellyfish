using System.Web.Mvc;
using Web.Filters;
using Web.Models;
using Web.Repositories;

namespace Web.Controllers {
    [Authorized]
    public class MyAccountController : Controller {
        private readonly IAccountRepository accountRepository;

        public MyAccountController(IAccountRepository accountRepository) {
            this.accountRepository = accountRepository;
        }

        public ActionResult Verified() {
            return View();
        }        
    }
}