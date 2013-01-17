using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Events;

namespace Domain {
    public abstract class AggregateRoot {
        private readonly Queue<DomainEvent> uncommittedEvents = new Queue<DomainEvent>();
        public Guid Id { get; protected set; }
        public char LastEventSequence { get; protected set; }

        public ReadOnlyCollection<DomainEvent> UncommittedEvents {
            get { return new ReadOnlyCollection<DomainEvent>(uncommittedEvents.ToList()); }
        }

        protected void Apply(DomainEvent domainEvent) {
            domainEvent.Sequence = ++LastEventSequence;
            ApplyEventToDomainEntityState(domainEvent);

            domainEvent.AggregateRootId = Id;
            domainEvent.EventDate = DateTime.Now;
            uncommittedEvents.Enqueue(domainEvent);
        }

        private void ApplyEventToDomainEntityState(DomainEvent domainEvent) {
            var domainEventType = domainEvent.GetType();
            var domainEventTypeName = domainEventType.Name;
            var aggregateRootType = GetType();

            var eventHandlerMethodName = GetEventHandlerMethodName(domainEventTypeName);
            var methodInfo = aggregateRootType.GetMethod(eventHandlerMethodName,
                                                         BindingFlags.Instance | BindingFlags.Public |
                                                         BindingFlags.NonPublic, null, new[] {domainEventType}, null);

            if (methodInfo != null && EventHandlerMethodInfoHasCorrectParameter(methodInfo, domainEventType)) {
                methodInfo.Invoke(this, new[] {domainEvent});
            }
        }

        private static string GetEventHandlerMethodName(string domainEventTypeName) {
            var eventIndex = domainEventTypeName.LastIndexOf("Event");
            return "On" + domainEventTypeName.Remove(eventIndex, 5);
        }

        private static bool EventHandlerMethodInfoHasCorrectParameter(MethodInfo eventHandlerMethodInfo,
                                                                      Type domainEventType) {
            var parameters = eventHandlerMethodInfo.GetParameters();
            return parameters.Length == 1 && parameters[0].ParameterType == domainEventType;
        }
    }
}