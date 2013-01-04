using System.Web.Mvc;
using Budget.Attributes;

namespace Web.Attributes {
    public class PasswordAttribute : BaseMetadataAttribute {
        public override void Process(ModelMetadata modelMetadata) {
            modelMetadata.TemplateHint = "Password";
        }
    }
}