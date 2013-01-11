using System;
using Autofac;

namespace Denormalizers {
    public class DenormalizerModule : Module {
        protected override void Load(ContainerBuilder builder) {
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                   .Where(t => t.Name.EndsWith("Denormalizer"))
                   .AsImplementedInterfaces();
        }
    }
}