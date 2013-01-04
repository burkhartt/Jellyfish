using System.Web.Mvc;

namespace Web.Attributes {
    public class PasswordAttribute : BaseMetadataAttribute {
        public override void Process(ModelMetadata modelMetadata) {
            modelMetadata.TemplateHint = "Password";
        }
    }
}