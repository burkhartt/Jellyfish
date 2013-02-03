using System;
using System.Collections.Generic;
using Domain.Models.Goals;

namespace Domain.Repositories {
    public interface IBucketRepository {
        IEnumerable<Bucket> AllByAccountId(Guid id);
    }
}