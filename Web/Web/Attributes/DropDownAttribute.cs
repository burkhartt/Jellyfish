using System.Collections.Generic;
using System.Web.Mvc;

namespace Web.Attributes {
    public class DropDownAttribute : BaseMetadataAttribute {
        public DropDownAttribute() {
            SelectListItems = new List<SelectListItem>();
        }

        protected virtual List<SelectListItem> SelectListItems { get; private set; }

        public override void Process(ModelMetadata modelMetadata) {
            modelMetadata.TemplateHint = "Dropdown";

            List<SelectListItem> selectListItems = SelectListItems;
            selectListItems.Insert(0, new SelectListItem {Text = "-Please Select-", Value = ""});

            modelMetadata.AdditionalValues.Add("Items", selectListItems);
        }
    }
}