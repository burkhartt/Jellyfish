namespace Web.Models {
    public class BaseMessage {
        public string Message { get; set; }
    }

    public class SuccessMessage : BaseMessage {}

    public class ErrorMessage : BaseMessage {}
}