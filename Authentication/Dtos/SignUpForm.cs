using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Dtos
{
    public class SignUpForm
    {
        public IFormFile? ImageFile { get; set; }

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        public string? StreetName { get; set; }

        public string? PostalCode { get; set; }

        public string? City { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
