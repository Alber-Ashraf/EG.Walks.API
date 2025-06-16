using EG.Walks.Domain.DTOs;
using EG.Walks.Domain.Entities;
using EG.Walks.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EG.Walks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        // Database context for accessing regions
        private readonly EGWalksDbContext _dbContext;
        // Constructor to inject the database context
        public RegionsController(EGWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Get all regions
        //https://localhost:xxxx/api/Regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Fetch all regions from the database
            var regions = await _dbContext.Regions.ToListAsync();

            // Check if the regions list is empty
            if (regions == null || !regions.Any())
            {
                // If the regions list is empty, return a NotFound response
                return NotFound("No regions found.");
            }
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

        // Get a specific region by ID
        // https://localhost:xxxx/api/Regions/{id}
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // Fetch the region with the specified ID from the database
            var region = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            // Check if the region exists
            if (region == null)
            {
                // If the region does not exist, return a NotFound response
                return NotFound();
            }

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

        // Create a new region
        // https://localhost:xxxx/api/Regions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto) 
        {
            // Validate the incoming request DTO
            if (addRegionRequestDto == null)
            {
                // If the request DTO is null, return a BadRequest response
                return BadRequest("Region data is required.");
            }

            // Create a new region entity from the DTO
            var region = new Region()
            {
                Id = Guid.NewGuid(),
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };
            // Add the new region to the database
            await _dbContext.Regions.AddAsync(region);
            // Save changes to the database
            await _dbContext.SaveChangesAsync();

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

        // Update an existing region
        // https://localhost:xxxx/api/Regions/{id}
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var region = new Region()
            {
                Id = id,
                Code = updateRegionRequestDto.Code,
                Name = updateRegionRequestDto.Name,
                RegionImageUrl = updateRegionRequestDto.RegionImageUrl
            };
            // Check if the region exists in the database
            var existingRegion = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                // If the region does not exist, return a NotFound response
                return NotFound();
            }
            // Update the existing region with the new values
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;
            // Save changes to the database
            await _dbContext.SaveChangesAsync();

            // Map the updated region to DTO
            var regionDto = new
            {
                existingRegion.Id,
                existingRegion.Code,
                existingRegion.Name,
                existingRegion.RegionImageUrl
            };

            // Return the updated region with a 200 OK status
            return Ok(new
            {
                regionDto.Id,
                regionDto.Code,
                regionDto.Name,
                regionDto.RegionImageUrl
            });
        }

        // Delete a region
        // https://localhost:xxxx/api/Regions/{id}
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // Check if the region exists in the database
            var existingRegion = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                // If the region does not exist, return a NotFound response
                return NotFound();
            }
            // Remove the existing region from the database
            _dbContext.Regions.Remove(existingRegion);
            // Save changes to the database
            await _dbContext.SaveChangesAsync();

            // Map the deleted region to DTO
            var regionDto = new
            {
                existingRegion.Id,
                existingRegion.Code,
                existingRegion.Name,
                existingRegion.RegionImageUrl
            };

            // Return a NoContent response indicating successful deletion
            return Ok(regionDto);
        }
    }
}
