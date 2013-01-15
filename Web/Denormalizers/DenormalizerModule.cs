using System;
using Autofac;

namespace Denormalizers {
    public class DenormalizerModule : Module {
        protected override void Load(ContainerBuilder builder) {
            builder.RegisterAssemblyTypes(typeof(DenormalizerModule).Assembly)
                   .Where(t => t.Name.EndsWith("Denormalizer"))
                   .AsImplementedInterfaces();
        }
    }
}