using System.Web.Mvc;
using Web.Authentication;

namespace Web.Controllers {
    public class BaseController : Controller {
        protected new virtual Principal User {
            get { return HttpContext.User as Principal; }
        }
    }
}