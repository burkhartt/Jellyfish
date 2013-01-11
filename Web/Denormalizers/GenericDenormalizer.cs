using Domain.Models;
using Domain.Repositories;
using Events.Entities;
using Events.Handler;

namespace Denormalizers {
    public abstract class GenericDenormalizer<T> : IHandleEvents<EntityCreatedEvent<T>>,
                                                   IHandleEvents<EntityUpdatedEvent<T>>,
                                                   IHandleEvents<EntityDeletedEvent<T>> where T : IEntity {
        private readonly IRepository<T> repository;

        protected GenericDenormalizer(IRepository<T> repository) {
            this.repository = repository;
        }

        public virtual void Handle(EntityCreatedEvent<T> @event) {
            repository.Create(@event.Entity);
        }

        public virtual void Handle(EntityDeletedEvent<T> @event) {
            repository.Delete(@event.Id);
        }

        public virtual void Handle(EntityUpdatedEvent<T> @event) {
            repository.Update(@event.Entity);
        }
    }
}