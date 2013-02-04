using System;
using System.Collections.Generic;
using System.Linq;
using Database;
using Domain.Models.Goals;

namespace Domain.Repositories {
    public class GroupRepository : Repository<Group>, IGroupRepository {
        private readonly IDatabase database; 

        public GroupRepository(IDatabase database) : base(database) {
            this.database = database;
        }

        public IEnumerable<Group> GetByAccountId(Guid id) {
            IEnumerable<GroupMember> groupsAccountTiedTo = database.GetTheDatabase().GroupMembers.FindAllByAccountId(id).ToList<GroupMember>();
            var list = groupsAccountTiedTo.Select(@group => @group.GroupId).ToList();
            return database.GetTheDatabase().Groups.FindAllById(list).ToList<Group>();
        }

        public Group GetById(Guid id) {
            return (Group)database.GetTheDatabase().Groups.FindById(id);
        }
    }
}