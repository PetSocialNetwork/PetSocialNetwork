namespace PetSocialNetwork.Domain.Membership;

public class User
{
    private Guid _id;

    private long _telegramId;
    private bool _hasFullProfile{ get; set; }
    public UserProfile UserProfile { get; set; }

    protected User() { }

    public User(Guid id, long telegramId, bool hasFullProfile)
    {
        _id = id;
        _telegramId = telegramId;
        _hasFullProfile = hasFullProfile;
    }

    public Guid Id
    {
        get
        {
            return _id;
        }
        init
        {
            _id = value;
        }
    }

    public long TelegramId
    {
        get
        {
            return _telegramId;
        }
        set
        {
            _telegramId = value;
        }
    }

    public bool HasFullProfile
    {
        get
        {
            return _hasFullProfile;
        }
        set
        {
            _hasFullProfile = value;
        }
    }

    public void AddUserProfile(string firstName, string lastName, string userName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new ArgumentException($"\"{nameof(firstName)}\" не может быть пустым или содержать только пробел.", nameof(firstName));
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new ArgumentException($"\"{nameof(lastName)}\" не может быть пустым или содержать только пробел.", nameof(lastName));
        }

        if (string.IsNullOrWhiteSpace(userName))
        {
            throw new ArgumentException($"\"{nameof(userName)}\" не может быть пустым или содержать только пробел.", nameof(userName));
        }

        UserProfile = new UserProfile(Guid.NewGuid(), firstName, lastName, userName);
    }
}