using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Events.Events;
using Events.Handler;

namespace Events.Bus {
    public class EventBus : IEventBus {
        private readonly IComponentContext componentContext;

        public EventBus(IComponentContext componentContext) {
            this.componentContext = componentContext;
        }

        public void Send(DomainEvent @event) {
            foreach (var registration in componentContext.ComponentRegistry.Registrations) {
                foreach (var service in registration.Services.OfType<TypedService>()) {
                    var type = service.ServiceType;
                    if (!type.IsInterface || !type.IsGenericType || !type.IsConstructedGenericType ||
                        type.GetGenericTypeDefinition() != typeof (IHandleDomainEvents<>)) {
                        continue;
                    }

                    var method = type.GetMethod("Handle");

                    if (Enumerable.Any<ParameterInfo>(method.GetParameters(), x => x.ParameterType == @event.GetType())) {
                        var handler = componentContext.ResolveComponent(registration, new List<Parameter>());
                        method.Invoke(handler, new[] {@event});
                    }
                }
            }
        }
    }
}