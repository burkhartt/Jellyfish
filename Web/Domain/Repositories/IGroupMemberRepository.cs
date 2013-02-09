using System;
using System.Collections.Generic;
using Domain.Models.Goals;

namespace Domain.Repositories {
    public interface IGroupMemberRepository {
        IEnumerable<GroupMember> GetAllByGroupId(Guid id);
    }
}