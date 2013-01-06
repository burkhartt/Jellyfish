using System;
using Web.Attributes;

namespace Web.Models {
    public class Goal : IEntity {
        public string Title { get; set; }
        [TextArea]
        public string Description { get; set; }
        [Hidden]
        public Guid Id { get; set; }
    }

    public interface IEntity {
        Guid Id { get; set; }
    }
}