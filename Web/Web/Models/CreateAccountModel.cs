using Web.Attributes;

namespace Web.Models
{
    public class CreateAccountModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        [Password]
        public string Password { get; set; }
        [Password]
        public string ConfirmPassword { get; set; }
    }
}