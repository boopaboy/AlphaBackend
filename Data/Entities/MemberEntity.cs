using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class MemberEntity
    {
        [Key]
        public int Id { get; set; }
        public string? ImageUrl { get; set; }

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }

        public string JobTitle{ get; set; } = null!;

        public string? Adress { get; set; }

        public string?  PostalCode { get; set; }

        public string? City { get; set; }


    }
}
