using System;

namespace Web.Attributes {
    public class DoNotDisplayAttribute : BaseMetadataAttribute {
        public override void Process(System.Web.Mvc.ModelMetadata modelMetadata) {
            modelMetadata.ShowForDisplay = false;
            modelMetadata.ShowForEdit = false;
        }
    }
}