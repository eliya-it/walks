using NZWalks.API.Models;

namespace NZWalks.API.Repositories;

public interface IWalkRepository
{
    Task<Walk?> GetByIdAsync(Guid id);
    Task<Walk> CreateAsync(Walk walk);
    Task<List<Walk>> GetAllAsync();
    Task<Walk> UpdateAsync(Guid id, Walk walk);
    Task<Walk?> DeleteAsync(Guid id);
}