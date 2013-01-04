using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Autofac;
using Budget.Attributes;

namespace Web.Attributes {
    public class CheckboxListAttribute : BaseMetadataAttribute {
        protected virtual List<SelectListItem> SelectListItems { get; private set; }

        public CheckboxListAttribute() {
            SelectListItems = new List<SelectListItem>();
        }

        public override void Process(ModelMetadata modelMetadata) {
            modelMetadata.TemplateHint = "CheckboxList";
            modelMetadata.AdditionalValues.Add("Items", SelectListItems);            
            base.Process(modelMetadata);
        }
    }    
}