using FluentValidation;
using PetSocialNetwork.API.Contracts;

namespace PetSocialNetwork.API.Validators
{
    public class UserProfileValidator : AbstractValidator<UserProfileRequest>
    {
        public UserProfileValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Не указано имя!");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Не указана фамилия!");
            RuleFor(x => x.Animal).NotEmpty().WithMessage("Не указано животное!");
            RuleFor(x => x.Profession).NotEmpty().WithMessage("Не указана профессия!");
            RuleFor(x => x.Gender).IsInEnum().WithMessage("Не указан пол!");
            RuleFor(x => x.PetGender).IsInEnum().WithMessage("Не указан пол животного!");
        }
    }
}
