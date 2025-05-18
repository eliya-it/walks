using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data;

public class NZWalksDbContext : DbContext
{
    public NZWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {

    }

    public DbSet<Difficulty> Difficulties { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Walk> Walks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Seed data for Difficulties
        var difficulties = new List<Difficulty>
    {
        new Difficulty
        {
            Id = Guid.Parse("b0f1a2c4-5d3e-4b8c-9f7d-6a2e5f3b8c2d"),
            Name = "Easy"
        },
        new Difficulty
        {
            Id = Guid.Parse("b0f1a2c4-5d3e-4b8c-9f7d-6a2e5f3b8c1e"),
            Name = "Medium"
        },
        new Difficulty
        {
            Id = Guid.Parse("cdf1a2c4-5d3e-4b8c-9f7d-6a2e5f3b8c1e"),
            Name = "Hard"
        },
    };
        modelBuilder.Entity<Difficulty>().HasData(difficulties);

        // seed data for Regions
        var regions = new List<Region>
    {
        new Region
        {
            Id = Guid.Parse("a1b2c3d4-e5f6-4a8c-9b7d-6a2e5f3b8c2d"),
            Name = "North Island",
            Code = "NI",
            RegionImageUrl = "https://example.com/north-island.jpg"
        },
        new Region
        {
            Id = Guid.Parse("b1c2d3e4-f5a6-4b8c-8d7e-7b3c6d2e4f1a"),
            Name = "South Island",
            Code = "SI",
            RegionImageUrl = "https://example.com/south-island.jpg"
        },
        new Region
        {
            Id = Guid.Parse("c1d2e3f4-a5b6-4c8d-9e7f-1a2b3c4d5e6f"),
            Name = "Stewart Island",
            Code = "STI",
            RegionImageUrl = "https://example.com/stewart-island.jpg"
        },
        new Region
        {
            Id = Guid.Parse("d1e2f3a4-b5c6-4d7e-8f9a-2b3c4d5e6f7a"),
            Name = "Chatham Islands",
            Code = "CI",
            RegionImageUrl = "https://example.com/chatham-islands.jpg"
        },
    };
        modelBuilder.Entity<Region>().HasData(regions);
    }
}