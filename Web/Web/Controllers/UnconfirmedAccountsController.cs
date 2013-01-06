using System.Web.Mvc;
using Web.Repositories;

namespace Web.Controllers {
    public class UnconfirmedAccountsController : Controller {
        private readonly IAccountRepository accountRepository;

        public UnconfirmedAccountsController(IAccountRepository accountRepository) {
            this.accountRepository = accountRepository;
        }

        public ActionResult Index() {
            return View(accountRepository.GetAllUnconfirmedAccounts());
        }
    }
}