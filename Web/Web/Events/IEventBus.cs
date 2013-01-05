using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;

namespace Web.Events {
    public interface IEventBus {
        void Send(IEvent @event);
    }

    public class EventBus : IEventBus {
        private readonly IComponentContext componentContext;

        public EventBus(IComponentContext componentContext) {
            this.componentContext = componentContext;
        }

        public void Send(IEvent @event) {
            foreach (var registration in componentContext.ComponentRegistry.Registrations) {
                foreach (var service in registration.Services.OfType<TypedService>()) {
                    var type = service.ServiceType;
                    if (!type.IsInterface || !type.IsGenericType || !type.IsConstructedGenericType ||
                        type.GetGenericTypeDefinition() != typeof (IHandleEvents<>)) {
                        continue;
                    }

                    var method = type.GetMethod("Handle");

                    if (method.GetParameters().Any(x => x.ParameterType == @event.GetType())) {
                        var handler = componentContext.ResolveComponent(registration, new List<Parameter>());
                        method.Invoke(handler, new[] {@event});
                    }
                }
            }
        }
    }
}