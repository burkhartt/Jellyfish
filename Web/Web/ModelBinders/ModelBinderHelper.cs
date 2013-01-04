using System;
using System.Web.Mvc;

namespace Budget.ModelBinders {
    public static class ModelBinderHelper {
        public static string GetValue(ModelBindingContext bindingContext, string key) {
            if (String.IsNullOrEmpty(key)) return null;
            //Try it with the prefix...
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + "." + key);
            //Didn't work? Try without the prefix if needed...
            if (valueResult == null && bindingContext.FallbackToEmptyPrefix) {
                valueResult = bindingContext.ValueProvider.GetValue(key);
            }
            if (valueResult == null) {
                return null;
            }
            return valueResult.AttemptedValue;
        }
    }
}