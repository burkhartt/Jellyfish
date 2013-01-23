using System;
using System.ComponentModel;

namespace Web.Models {
    public class GoalForm {
        public GoalForm() {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        [DisplayName("What are you going to do?")]
        public string Title { get; set; }
        [DisplayName("Tell me more about it...")]
        public string Description { get; set; }        
    }
}