using FluentValidation;
using Web.Models;

namespace Web.Validation {
    public class GoalFormValidator : AbstractValidator<GoalForm> {
        public GoalFormValidator() {
            RuleFor(x => x.Title).NotEmpty();
        }
    }
}