using System;
using System.Collections.Generic;
using Domain.Models.Goals;

namespace Domain.Repositories {
    public interface IGroupRepository {
        IEnumerable<Group> GetByAccountId(Guid id);
        Group GetById(Guid id);
    }    
}