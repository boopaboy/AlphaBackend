using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class AddStatusForm
{
    [Required]
    public string StatusName { get; set; } = null!;
}
