using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;
using Web.Controllers;
using Web.Events.Entity;
using Web.Models;

namespace Web.Events {
    public interface IEventBus<T> {
        void Send(IEvent @event);
    }

    public class EventBus<T> : IEventBus<T> {
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

    public abstract class GenericDenormalizer<T> : IHandleEvents<EntityCreatedEvent<T>>,
                                                   IHandleEvents<EntityUpdatedEvent<T>>,
                                                   IHandleEvents<EntityDeletedEvent<T>> where T : IEntity {
        public virtual void Handle(EntityCreatedEvent<T> @event) {
            var a = 3;
        }

        public virtual void Handle(EntityDeletedEvent<T> @event) {}
        public virtual void Handle(EntityUpdatedEvent<T> @event) {}
    }

    public class GoalDenormalizer : GenericDenormalizer<Goal> {        
    }
}