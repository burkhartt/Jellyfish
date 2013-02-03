using System;
using System.Collections.Generic;
using System.Linq;
using Database;
using Domain.Models.Goals;

namespace Domain.Repositories {
    public class BucketRepository : Repository<Bucket>, IBucketRepository {
        private readonly IDatabase database;

        public BucketRepository(IDatabase database) : base(database) {
            this.database = database;
        }

        public IEnumerable<Bucket> AllByAccountId(Guid parentId, Guid accountId) {
            IEnumerable<Bucket> buckets = database.GetTheDatabase().Buckets.FindAllByAccountId(accountId).ToList<Bucket>();
            return buckets.Where(x => x.ParentId == parentId);
        }

        public Bucket GetById(Guid id) {
            return (Bucket)database.GetTheDatabase().Buckets.FindById(id);
        }
    }
}