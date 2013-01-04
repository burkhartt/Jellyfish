using System;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Autofac;

namespace Budget.Attributes {
    public abstract class BaseMetadataAttribute : Attribute, IMetadataAttribute {
        public virtual void Process(ModelMetadata modelMetadata) {
            var r = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);
            modelMetadata.DisplayName = r.Replace(modelMetadata.PropertyName, " ");
        }

        public IComponentContext Container { get; set; }
    }
}