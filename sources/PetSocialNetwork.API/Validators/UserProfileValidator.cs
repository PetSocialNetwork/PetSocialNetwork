using FluentValidation;
using PetSocialNetwork.Domain.Membership;

namespace PetSocialNetwork.API.Validators
{
    public class UserProfileValidator : AbstractValidator<UserProfile>
    {
        public UserProfileValidator()
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty();
            RuleFor(x => x.LastName).NotNull().NotEmpty();
            RuleFor(x => x.Animal).NotNull().NotEmpty();
            RuleFor(x => x.Profession).NotNull().NotEmpty();
            RuleFor(x => x.Gender).IsInEnum();
            RuleFor(x => x.PetGender).IsInEnum();
        }
    }
}
