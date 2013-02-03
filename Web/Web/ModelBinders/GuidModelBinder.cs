using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Autofac.Integration.Mvc;

namespace Web.ModelBinders {
    [ModelBinderType(typeof (Guid))]
    public class GuidModelBinder : DefaultModelBinder {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueResult == null) {
                return Guid.Empty;
            }
            var rawValue = valueResult.RawValue;

            var vals = rawValue as IEnumerable<string>;
            if (vals != null) {
                return new Guid(vals.First());
            }

            return new Guid(rawValue.ToString());
        }
    }
}