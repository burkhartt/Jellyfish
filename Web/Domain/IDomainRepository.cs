namespace Domain {
    public interface IDomainRepository {
        void Save(AggregateRoot aggregateRoot);
    }
}