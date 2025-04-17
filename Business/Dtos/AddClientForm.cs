using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class AddClientForm
{
    public IFormFile? ImageFile { get; set; }

    [Required]
    public string ClientName { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!;

    public string? City { get; set; }
    public string? Phone { get; set; }

    public string? Adress { get; set; }
}
