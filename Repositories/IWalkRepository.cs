using NZWalks.API.Models;

namespace NZWalks.API.Repositories;

public interface IWalkRepository
{
    Task<Walk?> GetByIdAsync(Guid id);
    Task<Walk> CreateAsync(Walk walk);
    Task<List<Walk>> GetAllAsync(string? filterBy = null, string? filterQuery = null, string? sortBy = null, string? sortDirection = null, int page = 1, int limit = 9);
    Task<Walk> UpdateAsync(Guid id, Walk walk);
    Task<Walk?> DeleteAsync(Guid id);
}