using System;
using System.Web.Mvc;
using Autofac;

namespace Budget.Attributes {
    public class PasswordAttribute : Attribute, IMetadataAttribute {
        public void Process(ModelMetadata modelMetadata) {
            modelMetadata.TemplateHint = "Password";
        }

        public IComponentContext Container { get; set; }
    }
}