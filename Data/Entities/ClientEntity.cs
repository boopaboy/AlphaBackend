using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class ClientEntity
{
    [Key]
    public int Id { get; set; }
    public string? ImageFileName { get; set; }
    public string ClientName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? City { get; set; } 
    public string? Phone { get; set; }

    public string? Adress { get; set; }


}