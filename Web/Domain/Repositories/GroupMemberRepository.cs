using System;
using System.Collections.Generic;
using Database;
using Domain.Models.Goals;

namespace Domain.Repositories {
    public class GroupMemberRepository : IGroupMemberRepository {
        private readonly IDatabase database;

        public GroupMemberRepository(IDatabase database) {
            this.database = database;
        }

        public IEnumerable<GroupMember> GetAllByGroupId(Guid id) {
            return database.GetTheDatabase().GroupMembers.FindAllByGroupId(id).ToList<GroupMember>();
        }
    }
}