using Autofac;

namespace Hubs {
    public class HubsModule : Module {
        protected override void Load(ContainerBuilder builder) {
            builder.RegisterAssemblyTypes(typeof (GoalHistoryHub).Assembly).AsImplementedInterfaces();
        }
    }
}