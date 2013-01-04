using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Autofac;

namespace Budget.Attributes {
    public class CheckboxListAttribute : Attribute, IMetadataAttribute {
        protected virtual List<SelectListItem> SelectListItems { get; private set; }

        public CheckboxListAttribute() {
            SelectListItems = new List<SelectListItem>();
        }

        public void Process(ModelMetadata modelMetadata) {
            modelMetadata.TemplateHint = "CheckboxList";
            modelMetadata.AdditionalValues.Add("Items", SelectListItems);            
        }

        public IComponentContext Container { get; set; }
    }
}