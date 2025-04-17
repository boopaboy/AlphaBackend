namespace Auth.Models
{
    public class SetAppUserProfile
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public string? PhoneNumber { get; set; }
    }


     public static implicit operator SetAppUserProfile(SignUp signUp)
        {
            return new SetAppUserProfile
            {
                FirstName = signUp.FirstName,
                LastName = signUp.LastName,
                PhoneNumber = signUp.PhoneNumber
            };
        }

    }
