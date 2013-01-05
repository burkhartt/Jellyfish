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
            //var implementorMethods =
            //    componentContext.ComponentRegistry.Registrations.Where(x => x.Services.OfType<TypedService>().Any(y => {
            //        var type = y.ServiceType;
            //        if (!type.IsInterface || !type.IsGenericType || !type.IsConstructedGenericType || type.GetGenericTypeDefinition() != typeof (IEventHandler<>)) {
            //            return false;
            //        }

            //        return type.GetGenericArguments().Any(z => z == @event.GetType());
            //    })).ToList();

            //foreach (var blah in implementorMethods) {
            //    dynamic handler = componentContext.Resolve(blah.Services.First().GetType());                
            //    handler.Handle(@event);
            //}

            foreach (var registration in componentContext.ComponentRegistry.Registrations) {
                foreach (var service in registration.Services.OfType<TypedService>()) {
                    var type = service.ServiceType;
                    if (!type.IsInterface || !type.IsGenericType || !type.IsConstructedGenericType ||
                        type.GetGenericTypeDefinition() != typeof (IEventHandler<>)) {
                        continue;
                    }

                    var method = type.GetMethod("Handle");
                    var handler = componentContext.ResolveComponent(registration, new List<Parameter>());
                    method.Invoke(handler, new[] {@event});
                }
            }
        }
    }

    public class Junk : IEventHandler<EntityCreatedEvent<Goal>> {
        public void Handle(EntityCreatedEvent<Goal> @event) {
            var a = 3;
        }
    }

    public class MoreJunk : IEventHandler<EntityCreatedEvent<Goal>>
    {
        public void Handle(EntityCreatedEvent<Goal> @event)
        {
            var a = 3;
        }
    }
}