using System;
using System.ComponentModel;
using Attributes;

namespace Domain.Models.Goals {
    public class Goal : IEntity {        
        public string Title { get; set; }        
        public string Description { get; set; }
        [DisplayName("When will you have it accomplished?")]
        public DateTime? Deadline { get; set; }
        [NotEditable]
        public Guid Id { get; set; }
    }
}