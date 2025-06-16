using EG.Walks.Domain.DTOs;
using EG.Walks.Domain.Entities;
using EG.Walks.Infrastructure.Data;
using EG.Walks.Infrastructure.Repository.IRepository;
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
        private readonly IUnitOfWork _unitOfWork;
        // Constructor to inject the database context
        public RegionsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Get all regions
        //https://localhost:xxxx/api/Regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Fetch all regions from the database
            var regions = await _unitOfWork.Region.GetAllRegionsAsync();

            // Check if the regions list is empty
            if (regions == null || !regions.Any())
            {
                // If the regions list is empty, return a NotFound response
                return NotFound("No regions found.");
            }
            // Map the regions to a list of anonymous objects
            var regionDtos = regions.Select(region => new
            {
                region.Id,
                region.Code,
                region.Name,
                region.RegionImageUrl
            }).ToList();

            // Return the list of regions
            return Ok(regionDtos);
        }

        // Get a specific region by ID
        // https://localhost:xxxx/api/Regions/{id}
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // Fetch the region with the specified ID from the database
            var region = await _unitOfWork.Region.GetRegionByIdAsync(id);
            // Check if the region exists
            if (region == null)
            {
                // If the region does not exist, return a NotFound response
                return NotFound();
            }

            // Map the region to an anonymous object
            var regionDto = new
            {
                region.Id,
                region.Code,
                region.Name,
                region.RegionImageUrl
            };

            // Return the region details
            return Ok(regionDto);
        }

        // Create a new region
        // https://localhost:xxxx/api/Regions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto) 
        {
            // Map the incoming DTO to a Region entity
            var region = new Region()
            {
                Id = Guid.NewGuid(),
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };
            // Add the new region to the database
            region = await _unitOfWork.Region.CreateRegionAsync(region);
            // Save changes to the database
            await _unitOfWork.SaveAsync();

            // Map the created region to DTO
            var regionDto = new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            // Return the created region with a 201 Created status
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        // Update an existing region
        // https://localhost:xxxx/api/Regions/{id}
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            // Map the incoming DTO to a Region entity
            var region = new Region()
            {
                Id = id,
                Code = updateRegionRequestDto.Code,
                Name = updateRegionRequestDto.Name,
                RegionImageUrl = updateRegionRequestDto.RegionImageUrl
            };
            // Update the existing region with the new values
            var updatedRegion = await _unitOfWork.Region.UpdateRegionAsync(region);
            // Check if the region was found and updated
            if (updatedRegion == null)
            {
                // If the region does not exist, return a NotFound response
                return NotFound();
            }
            // Save changes to the database
            await _unitOfWork.SaveAsync();

            // Map the updated region to DTO
            var regionDto = new RegionDto
            {
                Id = updatedRegion.Id,
                Code = updatedRegion.Code,
                Name = updatedRegion.Name,
                RegionImageUrl = updatedRegion.RegionImageUrl
            };
            // Return the updated region with a 200 OK status
            return Ok(regionDto);
        }

        // Delete a region
        // https://localhost:xxxx/api/Regions/{id}
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // Attempt to delete the region with the specified ID
            var deletedRegion = await _unitOfWork.Region.DeleteRegionAsync(id);
            if (deletedRegion == null)
            {
                return NotFound();
            }

            // Save changes to the database after deletion
            await _unitOfWork.SaveAsync();

            // Map the deleted region to an anonymous object
            var regionDto = new RegionDto
            {
                Id = deletedRegion.Id,
                Code = deletedRegion.Code,
                Name = deletedRegion.Name,
                RegionImageUrl = deletedRegion.RegionImageUrl
            };

            // Return a NoContent response indicating successful deletion
            return Ok(regionDto);
        }
    }
}
