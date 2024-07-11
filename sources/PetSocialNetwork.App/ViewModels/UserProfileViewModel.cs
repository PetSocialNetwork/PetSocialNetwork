namespace PetSocialNetwork.App.ViewModel
{
    public class UserProfileViewModel
    {
        public string? FirstName;
        public string? LastName;
        public string? UserName;
        public Gender Gender;
        public string? Profession;
        public string? Animal;
        public Gender PetGender;
    }

    public enum Gender
    {
        Male,
        Female
    }
}
