using System.Web.Mvc;
using Autofac;

namespace Budget.Attributes {
    public interface IMetadataAttribute {
        void Process(ModelMetadata modelMetadata);
        IComponentContext Container { get; set; }
    }
}