
namespace NZWalks.API.Models.DTO;

public class AddWalkDto
{
    public string Name { get; set; }
    public string Description { get; set; }

    public string? WalkImageUrl { get; set; }
    public Guid DifficultyId { get; set; }
    public Guid RegionId { get; set; }
}