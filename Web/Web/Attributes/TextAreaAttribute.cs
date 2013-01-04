using System;
using System.Web.Mvc;
using Autofac;

namespace Budget.Attributes {
    public class TextAreaAttribute : Attribute, IMetadataAttribute {
        public void Process(ModelMetadata modelMetadata) {
            modelMetadata.TemplateHint = "Textarea";
        }

        public IComponentContext Container { get; set; }
    }
}