using System.Collections.Generic;
using Events.Bus;

namespace Domain {
    public class DomainRepository : IDomainRepository {
        private readonly List<AggregateRoot> aggregateRoots;
        private readonly IEventBus eventBus;

        public DomainRepository(IEventBus eventBus) {
            this.eventBus = eventBus;
            aggregateRoots = new List<AggregateRoot>();
        }

        public void Save(AggregateRoot aggregateRoot) {
            aggregateRoots.Add(aggregateRoot);
            
            foreach (var uncommittedEvent in aggregateRoot.UncommittedEvents) {
                eventBus.Send(uncommittedEvent);
            }
        }
    }
}