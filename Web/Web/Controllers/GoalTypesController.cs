using System.Web.Mvc;
using Domain.Repositories;
using Web.Filters;

namespace Web.Controllers {
    [Authorized]
    public class GoalTypesController : Controller {
        private readonly IGoalTypeRepository goalTypeRepository;

        public GoalTypesController(IGoalTypeRepository goalTypeRepository) {
            this.goalTypeRepository = goalTypeRepository;
        }

        public JsonResult GetAllTypes() {
            return Json(goalTypeRepository.GetAll(), JsonRequestBehavior.AllowGet);
        }
    }    
}