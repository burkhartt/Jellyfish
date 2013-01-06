using Web.Attributes;

namespace Web.Models {
    public class LoginModel {
        public string EmailAddress { get; set; }
        [Password]
        public string Password { get; set; }
    }
}