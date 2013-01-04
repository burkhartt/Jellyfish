using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Autofac;
using Web.Attributes;

namespace Web {
    public class CustomModelMetadataProvider : DataAnnotationsModelMetadataProvider {
        private readonly IComponentContext componentContext;

        public CustomModelMetadataProvider(IComponentContext componentContext) {
            this.componentContext = componentContext;
        }

        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType,
                                                        Func<object> modelAccessor, Type modelType, string propertyName) {
            var modelMetadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);

            var r = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);
            if (!string.IsNullOrEmpty(modelMetadata.PropertyName)) {
                modelMetadata.DisplayName = r.Replace(modelMetadata.PropertyName, " ");
            }
            
            attributes.OfType<IMetadataAttribute>().ToList().ForEach(x => {
                x.Container = componentContext;
                x.Process(modelMetadata);
            });

            return modelMetadata;
        }
    }
}