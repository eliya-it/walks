namespace NZWalks.API.Models.DTO;

public class UpdateRegionDto
{
    public Guid Id { get; set; }

    public string Code { get; set; }

    public string Name { get; set; }

    public string? RegionImageUrl { get; set; }
}