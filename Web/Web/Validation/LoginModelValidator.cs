using FluentValidation;
using Web.Models;

namespace Web.Validation {
    public class LoginModelValidator : AbstractValidator<LoginModel> {
        public LoginModelValidator() {
            RuleFor(x => x.EmailAddress).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}