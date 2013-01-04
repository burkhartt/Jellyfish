using System;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Autofac;
using Budget.Attributes;

namespace Web.Attributes {
    public abstract class BaseMetadataAttribute : Attribute, IMetadataAttribute {
        public virtual void Process(ModelMetadata modelMetadata) { }

        public IComponentContext Container { get; set; }
    }
}