using System.Web.Mvc;
using Budget.Attributes;

namespace Web.Attributes {
    public class TextAreaAttribute : BaseMetadataAttribute {
        public new void Process(ModelMetadata modelMetadata) {
            modelMetadata.TemplateHint = "Textarea";
            base.Process(modelMetadata);
        }
    }
}