using AutoMapper;
using EG.Walks.Domain.DTOs.Requests;
using EG.Walks.Domain.DTOs.Responses;
using EG.Walks.Domain.Entities;
using EG.Walks.Filtters;
using EG.Walks.Infrastructure.Data;
using EG.Walks.Infrastructure.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EG.Walks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionsController : ControllerBase
    {
        // Database context for accessing regions
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        // Constructor to inject the database context
        public RegionsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // Get all regions
        //https://localhost:xxxx/api/Regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Fetch all regions from the database
            var regionsDomainModel = await _unitOfWork.Region.GetAllRegionsAsync();

            // Check if the regions list is empty
            if (regionsDomainModel == null || !regionsDomainModel.Any())
            {
                // If the regions list is empty, return a NotFound response
                return NotFound("No regions found.");
            }
            // Map the regions to a list of anonymous objects
            var regionDtos = _mapper.Map<IEnumerable<RegionDtoResponse>>(regionsDomainModel);

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
            var regionDomainModel = await _unitOfWork.Region.GetRegionByIdAsync(id);
            // Check if the region exists
            if (regionDomainModel == null)
            {
                // If the region does not exist, return a NotFound response
                return NotFound();
            }

            // Map the region to an anonymous object
            var regionDto = _mapper.Map<RegionDtoResponse>(regionDomainModel);

            // Return the region details
            return Ok(regionDto);
        }

        // Create a new region
        // https://localhost:xxxx/api/Regions
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateRegionRequestDto createRegionRequestDto) 
        {
            // Map the incoming DTO to a Region entity
            var regionDomainModel = _mapper.Map<Region>(createRegionRequestDto);
            // Add the new region to the database
            regionDomainModel = await _unitOfWork.Region.CreateRegionAsync(regionDomainModel);
            // Save changes to the database
            await _unitOfWork.SaveAsync();

            // Map the created region to DTO
            var regionDto = _mapper.Map<RegionDtoResponse>(regionDomainModel);

            // Return the created region with a 201 Created status
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        // Update an existing region
        // https://localhost:xxxx/api/Regions/{id}
        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            // Map the incoming DTO to a Region entity
            var regionDomainModel = _mapper.Map<Region>(updateRegionRequestDto);
            // Update the existing region with the new values
            var updatedRegion = await _unitOfWork.Region.UpdateRegionAsync(id, regionDomainModel);
            // Check if the region was found and updated
            if (updatedRegion == null)
            {
                // If the region does not exist, return a NotFound response
                return NotFound();
            }
            // Save changes to the database
            await _unitOfWork.SaveAsync();

            // Map the updated region to DTO
            var regionDto = _mapper.Map<RegionDtoResponse>(updatedRegion);
            // Return the updated region with a 200 OK status
            return Ok(regionDto);
        }

        // Delete a region
        // https://localhost:xxxx/api/Regions/{id}
        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "Admin")]
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
            var regionDto = _mapper.Map<RegionDtoResponse>(deletedRegion);

            // Return the deleted region with a 200 OK status
            return Ok(regionDto);
        }
    }
}
