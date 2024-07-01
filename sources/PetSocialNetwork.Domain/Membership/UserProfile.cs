namespace PetSocialNetwork.Domain.Membership
{
    public class UserProfile
    {
        private Guid _id;
        private string _firstName;
        private string _lastName;
        private string _userName;
        private Gender _gender;
        private string? _profession;
        private string? _animal;
        private Gender _petGender;

        protected UserProfile() { }

        public UserProfile(Guid id, string firstName, string lastName, string userName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentNullException(nameof(firstName));
            }
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentNullException(nameof(lastName));
            }
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentNullException(nameof(userName));
            }
            _id = id;
            _firstName = firstName;
            _lastName = lastName;
            _userName = userName;        
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

        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }
                _firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }
                _lastName = value;
            }
        }

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }
                _userName= value;
            }
        }

        public Gender Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value;
            }
        }

        public string Profession
        {
            get
            {
                return _profession;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }
                _profession = value;
            }
        }

        public string Animal
        {
            get
            {
                return _animal;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }
                _animal = value;
            }
        }

        public Gender PetGender
        {
            get
            {
                return _petGender;
            }
            set
            {
                _petGender = value;
            }
        }
    }
}
