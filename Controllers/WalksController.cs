using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NZWalks.API.Models.DTO;
using AutoMapper;
using NZWalks.API.Models;
using NZWalks.API.Repositories;
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
    public async Task<IActionResult> Create([FromBody] AddWalkDto addWalk)
    {
        if (addWalk == null)
        {
            return BadRequest("Walk cannot be null");
        }
        // Map DTO to Domain Model
        var walkDomainModel = mapper.Map<Walk>(addWalk);
        await walkRepository.CreateAsync(walkDomainModel);
        // Map Domain Model back to DTO

        return Ok(mapper.Map<WalkDto>(walkDomainModel));
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var walks = await walkRepository.GetAllAsync();
        // if (walks == null || walks.Count == 0)
        // {
        //     return NotFound("No walks found");
        // }
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
    public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, [FromBody] UpdateWalkDto updateWalkDto)
    {
        var walkDomainModel = mapper.Map<Walk>(updateWalkDto);
        walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

        if (walkDomainModel == null)
        {
            return NotFound($"Walk with id {id} not found");
        }

        return Ok(mapper.Map<WalkDto>(walkDomainModel));
        // Map DTO to Domain Model


    }
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteWalk([FromRoute] Guid id)
    {

        var walk = await walkRepository.DeleteAsync(id);
        if (walk == null)
        {
            return NotFound($"Walk with id {id} not found");
        }
        return Ok(mapper.Map<WalkDto>(walk));
    }
}