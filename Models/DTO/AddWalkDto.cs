
using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO;

public class AddWalkDto
{
    [Required]
    [MinLength(3, ErrorMessage = "Name must be at least 3 characters long.")]
    [MaxLength(100, ErrorMessage = "Name must be at most 100 characters long.")]
    public string Name { get; set; }

    [Required]
    [MinLength(10, ErrorMessage = "Name must be at least 10 characters long.")]
    [MaxLength(500, ErrorMessage = "Name must be at most 100 characters long.")]
    public string Description { get; set; }
    [Required]
    [Range(0, 500, ErrorMessage = "Length must be between 0 and 1000 km.")]
    public double LengthInKm { get; set; }
    public string? WalkImageUrl { get; set; }
    [Required]
    public Guid DifficultyId { get; set; }
    [Required]
    public Guid RegionId { get; set; }
}