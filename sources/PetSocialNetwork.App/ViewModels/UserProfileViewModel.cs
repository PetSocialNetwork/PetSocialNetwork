using PetSocialNetwork.App.ViewModels;

namespace PetSocialNetwork.App.ViewModel
{
    public class UserProfileViewModel
    {
        //public Guid Id { get; set; }
        public string? FirstName {  get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public Gender Gender { get; set; }
        public string? Profession { get; set; }
        public string? Animal { get; set; }
        public Gender PetGender { get; set; }
    }
}
//public record UserProfileResponse
//    (Guid Id, string FirstName, string LastName, string userName, Gender Gender,
//    string? Profession, string? Animal, Gender PetGender);

