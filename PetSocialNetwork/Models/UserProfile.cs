namespace PetSocialNetwork.Models
{
    public class UserProfile
    {
        private Guid _id;
        private string _firstName;
        private string _lastName;
        private Gender _gender;
        private string _profession;
        private string _animal;
        private Gender _petGender;
        protected UserProfile() { }
        public UserProfile(Guid id, string firstName, string lastName, string profession, Gender gender, string animal, Gender petGender)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentNullException(nameof(firstName));
            }
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentNullException(nameof(lastName));
            }
            if (string.IsNullOrWhiteSpace(profession))
            {
                throw new ArgumentNullException(nameof(profession));
            }
            if (string.IsNullOrWhiteSpace(animal))
            {
                throw new ArgumentNullException(nameof(animal));
            }
            _id = id;
            _firstName = firstName;
            _lastName = lastName;
            _gender = gender;
            _profession = profession;
            _animal = animal;
            _petGender = petGender;
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
