namespace PetSocialNetwork.Domain.Membership;

public class User
{
    private Guid _id;
    private long _telegramId;

    protected User() { }
    public User(Guid id, long telegramId)
    {
        _id = id;
        _telegramId = telegramId;
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
}