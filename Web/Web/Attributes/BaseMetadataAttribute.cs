using System;
using System.Web.Mvc;
using Autofac;

namespace Web.Attributes {
    public abstract class BaseMetadataAttribute : Attribute, IMetadataAttribute {
        public virtual void Process(ModelMetadata modelMetadata) {}

        public IComponentContext Container { get; set; }
    }
}