using System;
using System.Globalization;
using System.Web.Mvc;
using Autofac.Integration.Mvc;

namespace Web.ModelBinders {
    [ModelBinderType(typeof(decimal))]
    public class DecimalModelBinder : DefaultModelBinder {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            var modelState = new ModelState { Value = valueResult };
            object actualValue = null;
            try {
                var valToCheck = valueResult.AttemptedValue;
                if (valToCheck != string.Empty) {
                    actualValue = Convert.ToDecimal(valToCheck.Replace("$", string.Empty), CultureInfo.InvariantCulture);
                }
            } catch (FormatException e) {
                modelState.Errors.Add(e);
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
            return actualValue;
        }
    }
}