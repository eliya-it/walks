using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NZWalks.API.Models.DTO;
using AutoMapper;
using NZWalks.API.Models;
using NZWalks.API.Repositories;
using NZWalks.API.CustomActionFilters;
using System.Net;
namespace NZWalks.API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class WalksController : ControllerBase
{
    private IMapper mapper;
    private IWalkRepository walkRepository;

    public WalksController(IMapper mapper, IWalkRepository walkRepository)
    {
        this.mapper = mapper;
        this.walkRepository = walkRepository;
    }

    [HttpPost]
    [ValidateModel]
    public async Task<IActionResult> Create([FromBody] AddWalkDto addWalk)
    {
        // Map DTO to Domain Model
        var walkDomainModel = mapper.Map<Walk>(addWalk);
        await walkRepository.CreateAsync(walkDomainModel);
        // Map Domain Model back to DTO
        var walkWithRelations = await walkRepository.GetByIdAsync(walkDomainModel.Id);

        return Ok(mapper.Map<WalkDto>(walkWithRelations));
    }
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? filterBy, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] string? sortDirection, [FromQuery] int page = 1, [FromQuery] int limit = 9)
    {
        var walks = await walkRepository.GetAllAsync(filterBy, filterQuery, sortBy, sortDirection, page, limit);
        return Ok(mapper.Map<List<WalkDto>>(walks));
    }
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetWalkById([FromRoute] Guid id)
    {
        var walk = await walkRepository.GetByIdAsync(id);
        if (walk == null)
        {
            return NotFound($"Walk with id {id} not found");
        }
        return Ok(mapper.Map<WalkDto>(walk));
    }
    [HttpPatch("{id:guid}")]
    [ValidateModel]
    public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, [FromBody] UpdateWalkDto updateWalkDto)
    {

        var walkDomainModel = mapper.Map<Walk>(updateWalkDto);
        walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

        if (walkDomainModel == null)
        {
            return NotFound($"Walk with id {id} not found");
        }
        var walkWithRelations = await walkRepository.GetByIdAsync(id);

        return Ok(mapper.Map<WalkDto>(walkWithRelations));



    }
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteWalk([FromRoute] Guid id)
    {

        var walk = await walkRepository.DeleteAsync(id);
        if (walk == null)
        {
            return NotFound($"Walk with id {id} not found");
        }
        var walkWithRelations = await walkRepository.GetByIdAsync(id);
        return Ok(mapper.Map<WalkDto>(walkWithRelations));
    }
}