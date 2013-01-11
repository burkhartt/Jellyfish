using System;

namespace Domain.Models {
    public class Friend {
        public Guid AccountId { get; set; }
        public Guid FriendId { get; set; }
    }
}