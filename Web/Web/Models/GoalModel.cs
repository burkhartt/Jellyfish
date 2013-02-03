using System;
using Domain.Models.Goals;
using Web.Attributes;

namespace Web.Models {
    public class GoalModel {
        public GoalModel(Goal goal) {
            Title = goal.Title;
            Id = goal.Id;
        }

        public string Title { get; set; }

        [TextArea]
        public string Description { get; set; }

        [Hidden]
        public Guid Id { get; set; }
    }
}