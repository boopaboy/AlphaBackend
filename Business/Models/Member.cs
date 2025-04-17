using Microsoft.AspNetCore.Identity;

namespace Business.Models
{
    public class Member: IdentityUser
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }

        public string JobTitle { get; set; } = null!;

        public string? Adress { get; set; }

        public string? PostalCode { get; set; }

        public string? City { get; set; }
    }
}
