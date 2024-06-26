using FluentValidation;
using PetSocialNetwork.API.DTOs;

namespace PetSocialNetwork.API.Validators
{
    public class UserProfileValidator : AbstractValidator<UserProfileDTO>
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
