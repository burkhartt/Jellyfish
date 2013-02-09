using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Autofac;

namespace Web.Layouts {
    public abstract class LayoutAttribute : Attribute {
        public abstract void LoadLayout(IComponentContext componentContext, IDictionary<string, object> actionParameters);
    }
}