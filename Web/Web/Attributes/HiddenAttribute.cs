using System.Web.Mvc;

namespace Web.Attributes {
    public class HiddenAttribute : BaseMetadataAttribute {
        public override void Process(ModelMetadata modelMetadata) {
            modelMetadata.DisplayName = " ";
            modelMetadata.TemplateHint = "Hidden";
        }
    }
}