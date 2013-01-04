using System.Web.Mvc;
using Web.Attributes;
using Web.Models;

namespace Web.Controllers {
    public class GoalsController : CrudController<Goal> {
        
    }

    public class CrudController<T> : Controller where T : class, new() {
        public ViewResult Create() {
            return View(new T());
        }

        [HttpPost]
        public ActionResult Create(T model) {
            if (!ModelState.IsValid) {
                return View(model);
            }

            return RedirectToAction("Index", "Home", new {SuccessMessage = model.GetType().Name + " created"});
        }
    }
}