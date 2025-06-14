using EG.Walks.Domain.DTOs;
using EG.Walks.Domain.Entities;
using EG.Walks.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EG.Walks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly EGWalksDbContext _dbContext;
        public RegionsController(EGWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            // Fetch all regions from the database
            var regions = _dbContext.Regions.ToList();

            // Map the regions to a list of anonymous objects
            var regionDTOs = regions.Select(region => new
            {
                region.Id,
                region.Code,
                region.Name,
                region.RegionImageUrl
            }).ToList();

            // Return the list of regions
            return Ok(regionDTOs);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            // Fetch the region with the specified ID from the database
            var region = _dbContext.Regions.FirstOrDefault(x => x.Id == id);

            // Map the region to an anonymous object
            var regionDTO = new
            {
                region.Id,
                region.Code,
                region.Name,
                region.RegionImageUrl
            };

            // Return the region details
            return Ok(regionDTO);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto) 
        {
            // Create a new region entity from the DTO
            var region = new Region()
            {
                Id = Guid.NewGuid(),
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };
            // Add the new region to the database
            _dbContext.Regions.Add(region);
            // Save changes to the database
            _dbContext.SaveChanges();

            // Map the created region to DTO
            var regionDto = new
            {
                region.Id,
                region.Code,
                region.Name,
                region.RegionImageUrl
            };

            // Return the created region with a 201 Created status
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, new
            {
                regionDto.Id,
                regionDto.Code,
                regionDto.Name,
                regionDto.RegionImageUrl
            });
        }
    }
}
