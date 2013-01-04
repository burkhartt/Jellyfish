using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Autofac;

namespace Budget.Attributes {
    public class DropDownAttribute : Attribute, IMetadataAttribute {
        protected virtual List<SelectListItem> SelectListItems { get; private set; }

        public DropDownAttribute() {
            SelectListItems = new List<SelectListItem>();
        }

        public void Process(ModelMetadata modelMetadata) {            
            modelMetadata.TemplateHint = "Dropdown";

            var selectListItems = SelectListItems;
            selectListItems.Insert(0, new SelectListItem { Text = "-Please Select-", Value = "" });

            modelMetadata.AdditionalValues.Add("Items", selectListItems);
        }

        public IComponentContext Container { get; set; }
    }
}