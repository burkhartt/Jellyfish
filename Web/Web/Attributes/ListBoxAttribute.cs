using System.Collections.Generic;
using System.Web.Mvc;

namespace Web.Attributes {
    public class ListBoxAttribute : BaseMetadataAttribute {
        protected virtual List<SelectListItem> SelectListItems { get; private set; }

        public ListBoxAttribute() {
            SelectListItems = new List<SelectListItem>();
        }

        public override void Process(ModelMetadata modelMetadata) {
            modelMetadata.TemplateHint = "ListBox";

            var selectListItems = SelectListItems;
            selectListItems.Insert(0, new SelectListItem { Text = "-Please Select-", Value = "" });

            modelMetadata.AdditionalValues.Add("Items", selectListItems);
        }
    }
}