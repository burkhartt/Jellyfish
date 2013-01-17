using System.Web.Mvc;
using CommandBus;
using Commands;

namespace Cms.Controllers {
    public class HomeController : Controller {
        private readonly ICommandBus commandBus;

        public HomeController(ICommandBus commandBus) {
            this.commandBus = commandBus;
        }

        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string nothing) {
            commandBus.Send(new CreateAccountCommand {FirstName = "Tim", LastName = "Burkhart"});

            return View();
        }
    }
}