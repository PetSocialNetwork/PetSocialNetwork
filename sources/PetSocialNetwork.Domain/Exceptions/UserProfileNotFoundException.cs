namespace PetSocialNetwork.Domain.Exceptions
{
    public class UserProfileNotFoundException : DomainException
    {
        public UserProfileNotFoundException()
        {
        }

        public UserProfileNotFoundException(string message) : base(message)
        {
        }
    }
}
