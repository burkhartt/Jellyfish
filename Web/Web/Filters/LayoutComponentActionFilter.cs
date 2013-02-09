using System.Collections.Generic;
using System.Web.Mvc;
using Autofac;

namespace Web.Filters {
    public abstract class LayoutComponentActionFilter : IActionFilter {
        public IComponentContext ComponentContext { get; set; }
        protected IDictionary<string, object> ActionParameters;

        public virtual void OnActionExecuting(ActionExecutingContext filterContext) {
            ActionParameters = filterContext.ActionParameters;
        }

        public abstract void OnActionExecuted(ActionExecutedContext filterContext);
    }
}