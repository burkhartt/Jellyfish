using System.Linq;
using System.Web.Mvc;
using Autofac;
using Web.Layouts;

namespace Web.Filters {
    public class GroupMenuFilter : LayoutComponentActionFilter {      
        public override void OnActionExecuted(ActionExecutedContext filterContext) {
            var attributes = filterContext.ActionDescriptor.GetCustomAttributes(true).Where(x => x is LayoutAttribute);
            
            foreach (LayoutAttribute attribute in attributes) {
                attribute.LoadLayout(ComponentContext, ActionParameters);
            }
        }
    }
}