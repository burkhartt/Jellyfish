using System.Web.Mvc;

namespace Web.Attributes {
    public class TextAreaAttribute : BaseMetadataAttribute {
        public new void Process(ModelMetadata modelMetadata) {
            modelMetadata.TemplateHint = "Textarea";
        }
    }
}