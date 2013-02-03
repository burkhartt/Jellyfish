using System;

namespace Domain.Models.Goals {
    public class GroupMember {
        public Guid GroupId { get; set; }
        public Guid AccountId { get; set; }
    }
}