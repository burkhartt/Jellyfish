using System.Web.Mvc;
using Domain.Repositories;
using Web.Filters;

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