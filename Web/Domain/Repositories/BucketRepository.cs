using System;
using System.Collections.Generic;
using Database;
using Domain.Models.Goals;

namespace Domain.Repositories {
    public class BucketRepository : Repository<Bucket>, IBucketRepository {
        private readonly IDatabase database;

        public BucketRepository(IDatabase database) : base(database) {
            this.database = database;
        }

        public IEnumerable<Bucket> AllByAccountId(Guid id) {
            return database.GetTheDatabase().Buckets.FindAllByAccountId(id).ToList<Bucket>();
        }
    }
}