using System;
using System.Web.Mvc;
using Autofac;

namespace Budget.Attributes {
    public class HiddenAttribute : Attribute, IMetadataAttribute {
        public void Process(ModelMetadata modelMetadata) {
            modelMetadata.DisplayName = " ";
            modelMetadata.TemplateHint = "Hidden";
        }

        public IComponentContext Container { get; set; }
    }
}