using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.Json;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
namespace NZWalks.API.Controllers;


[ApiController]
[Route("[controller]")]
// Route for this controller will be => http://localhost:port/api/regions
public class RegionController : ControllerBase
{
    private readonly NZWalksDbContext dbContext;
    private readonly IRegionRepository regionRepository;
    private readonly IMapper mapper;
    private readonly ILogger<RegionController> logger;

    public RegionController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper, ILogger<RegionController> logger)

    {
        this.dbContext = dbContext;
        this.regionRepository = regionRepository;
        this.mapper = mapper;
        this.logger = logger;
    }

    // GET: http://localhost:port/api/regions
    [HttpGet]
    // [Authorize(Roles = "Reader")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            logger.LogInformation("GET ALL Action method was invoked");
            logger.LogWarning("[-] Warning Log");
            //  Get Data
            var regions = await regionRepository.GetAllAsync();

            var regionsDto = mapper.Map<List<RegionDto>>(regions); // We used "regions" because the are the source of the mapping
            return Ok(regionsDto);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    // Get one Region (get region by ID)
    // Get: http://localhost:port/api/regions/id
    [HttpGet]
    [Route("{id:Guid}")]
    // [Authorize(Roles = "Reader")]
    // [Authorize]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var region = await regionRepository.GetByIdAsync(id);

        if (region == null) return NotFound();

        // Map Region Domain Model to Region DTO
        var regionDomain = mapper.Map<RegionDto>(region);
        return Ok(regionDomain);
    }
    // POST: http://localhost:port/api/regions
    [HttpPost]
    [ValidateModel] // Custom attribute to validate the model before executing the action
    // [Authorize(Roles = "Writer")]
    // [Authorize]
    public async Task<IActionResult> Create([FromBody] AddRegionDto addRegion)
    {
        // Map DTO to Domain Model
        var regionDomainModel = mapper.Map<Region>(addRegion);
        // Use Domain Model to Create Region
        await regionRepository.CreateAsync(regionDomainModel);

        // Map Domain Model Back to DTO
        var regionDto = mapper.Map<RegionDto>(regionDomainModel);
        return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDomainModel);
    }

    [HttpPatch]
    [Route("{id:Guid}")]
    [ValidateModel]
    // [Authorize(Roles = "Writer")]
    // [Authorize]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegion)
    {
        // Map DTO to Domain Model 
        var regionDomainModel = mapper.Map<Region>(updateRegion);

        regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
        if (regionDomainModel == null) return NotFound();

        // Convert Domain Model to DTO
        var regionDto = mapper.Map<RegionDto>(regionDomainModel);
        return Ok(regionDto);
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    // [Authorize]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var regionDomainModel = await regionRepository.DeleteAsync(id);
        if (regionDomainModel == null) return NotFound();
        return Ok();
    }
}