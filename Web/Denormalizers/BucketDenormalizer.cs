using Database;
using Events;
using Events.Handler;

namespace Denormalizers {
    public class BucketDenormalizer : IHandleDomainEvents<BucketCreatedEvent> {
        private readonly IDatabase database;

        public BucketDenormalizer(IDatabase database) {
            this.database = database;
        }

        public void Handle(BucketCreatedEvent @event) {
            database.GetTheDatabase().Buckets.Insert(Id: @event.Id, AccountId: @event.AccountId, Title: @event.Title);
        }
    }
}