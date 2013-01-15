using Autofac;
using Events.Bus;

namespace Events {
    public class EventModule : Module {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(EventBus)).As(typeof(IEventBus)).SingleInstance();            
        }
    }
}