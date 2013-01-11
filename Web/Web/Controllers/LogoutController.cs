using System.Web.Mvc;
using System.Web.Security;
using Domain.Repositories;
using Web.Repositories;

namespace Web.Controllers {
    public class LogoutController : Controller {
        private readonly IAccountSessionRepository accountSessionRepository;

        public LogoutController(IAccountSessionRepository accountSessionRepository) {
            this.accountSessionRepository = accountSessionRepository;
        }

        public ActionResult Index() {
            accountSessionRepository.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}