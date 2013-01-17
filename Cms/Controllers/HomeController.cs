using System.Web.Mvc;

namespace Cms.Controllers {
    public class HomeController : Controller {      
        public ActionResult Index() {
            return View();
        }        
    }
}