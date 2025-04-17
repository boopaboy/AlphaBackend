using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class UpdateProjectForm
{
    [Required]
    public string Id { get; set; } = null!;

    public string? ImageFileName { get; set; }
    public IFormFile? NewImageFile { get; set; }

    [Required]
    public string ProjectName { get; set; } = null!;

    [Required]
    public int ClientId { get; set; }

    public string? Description { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }

    public decimal? Budget { get; set; }

    [Required]
    public string UserId { get; set; } = null!;

    [Required]
    public int StatusId { get; set; }
}