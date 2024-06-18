namespace PetSocialNetwork.Models
{
    public class Account
    {
        private Guid _id;
        private string _telegramId;
        private string _hashedPassword;

        protected Account() { }
        public Account(Guid id, string telegramId, string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(hashedPassword))
            {
                throw new ArgumentNullException(nameof(hashedPassword));
            }

            if (string.IsNullOrWhiteSpace(hashedPassword))
            {
                throw new ArgumentNullException(nameof(telegramId));
            }
            _id = id;
            _telegramId = telegramId;
            _hashedPassword = hashedPassword;
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

        public string TelegramId
        {
            get
            {
                return _telegramId;
            }
            set
            {
                ArgumentNullException.ThrowIfNull(value);
                _telegramId = value;
            }
        }

        public string HashedPassword
        {
            get
            {
                return _hashedPassword;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }
                _hashedPassword = value;
            }
        }
    }
}
