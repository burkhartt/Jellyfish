using Web.Attributes;

namespace Web.Models {
    public class Goal {
        public string Title { get; set; }
        [TextArea]
        public string Description { get; set; }
    }
}