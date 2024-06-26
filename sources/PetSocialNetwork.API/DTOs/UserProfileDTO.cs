using PetSocialNetwork.Domain.Membership;

namespace PetSocialNetwork.API.DTOs
{
    public class UserProfileDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Gender Gender { get; set; }
        public string? Profession { get; set; }
        public string? Animal { get; set; }
        public Gender PetGender { get; set; }
    }
}
