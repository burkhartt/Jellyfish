using System.Collections.Generic;
using System.Web.Mvc;

namespace Web.Attributes {
    public class CheckboxListAttribute : BaseMetadataAttribute {
        public CheckboxListAttribute() {
            SelectListItems = new List<SelectListItem>();
        }

        protected virtual List<SelectListItem> SelectListItems { get; private set; }

        public override void Process(ModelMetadata modelMetadata) {
            modelMetadata.TemplateHint = "CheckboxList";
            modelMetadata.AdditionalValues.Add("Items", SelectListItems);
            base.Process(modelMetadata);
        }
    }
}