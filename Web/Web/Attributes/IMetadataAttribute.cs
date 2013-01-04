using System.Web.Mvc;
using Autofac;

namespace Web.Attributes {
    public interface IMetadataAttribute {
        IComponentContext Container { get; set; }
        void Process(ModelMetadata modelMetadata);
    }
}