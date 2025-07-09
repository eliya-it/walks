using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

public class SQLWalkRepository : IWalkRepository
{
    private NZWalksDbContext dbContext;

    public SQLWalkRepository(NZWalksDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Walk> CreateAsync(Walk walk)
    {
        await dbContext.Walks.AddAsync(walk);
        await dbContext.SaveChangesAsync();
        return walk;

    }

    public async Task<List<Walk>> GetAllAsync(string? filterBy = null, string? filterQuery = null, string? sortBy = null, string? sortDirection = "asc", int page = 1, int limit = 9)
    {
        var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();
        // Filtering
        if (string.IsNullOrWhiteSpace(filterBy) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
        {
            if (filterBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
            {
                walks = walks.Where(x => x.Name.ToLower().Contains(filterQuery.ToLower()));
            }


        }
        // Sorting
        if (string.IsNullOrWhiteSpace(sortBy) == false)
        {
            if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
            {
                walks = sortDirection == "asc" ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
            }
            else if (sortBy.Equals("LengthInKm", StringComparison.OrdinalIgnoreCase))
            {
                walks = sortDirection == "asc" ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
            }
            else if (sortBy.Equals("Description", StringComparison.OrdinalIgnoreCase))
            {
                walks = sortDirection == "asc" ? walks.OrderBy(x => x.Description) : walks.OrderByDescending(x => x.Description);
            }
        }
        // Pagination
        var skip = (page - 1) * limit; // Skip the first (page - 1) * limit records, means skip the 0 results for page 1, skip the first 9 results for page 2, etc.

        return await walks.Skip(skip).Take(limit).ToListAsync();
    }


    public async Task<Walk?> GetByIdAsync(Guid id)
    {
        return await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
    {
        var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
        var regionExists = await dbContext.Regions.AnyAsync(r => r.Id == walk.RegionId);

        if (existingWalk == null) return null;
        if (!regionExists)
        {
            throw new ArgumentException($"Region with ID {walk.RegionId} does not exist.");
        }

        existingWalk.Name = walk.Name;
        existingWalk.Description = walk.Description;
        existingWalk.LengthInKm = walk.LengthInKm;
        existingWalk.WalkImageUrl = walk.WalkImageUrl;
        existingWalk.Difficulty = walk.Difficulty;
        existingWalk.RegionId = walk.RegionId;
        await dbContext.SaveChangesAsync();
        return existingWalk;
    }
    public async Task<Walk?> DeleteAsync(Guid id)
    {
        var walk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
        if (walk == null) return null;
        dbContext.Walks.Remove(walk);
        await dbContext.SaveChangesAsync();
        return walk;
    }
}