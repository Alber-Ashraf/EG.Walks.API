using AutoMapper;
using EG.Walks.Domain.DTOs.Requests;
using EG.Walks.Domain.DTOs.Responses;
using EG.Walks.Domain.Entities;
using EG.Walks.Filtters;
using EG.Walks.Infrastructure.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EG.Walks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WalksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public WalksController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // Get all walks
        //https://localhost:****/api/Walks?filterOn=****&filterQuery=****
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending, 
            [FromQuery] int page, [FromQuery] int pageSize)
        {
            // Use the repository to get all walks
            var walksDomainModel = await _unitOfWork.Walk.GetAllWalksAsync(filterOn, filterQuery, sortBy, isAscending ?? true, page, pageSize);
            // Check if the list of walks is empty
            if (walksDomainModel == null || !walksDomainModel.Any())
            {
                // If the list is empty, return a NotFound response
                return NotFound("No walks found.");
            }
            // Map the domain model to a list of DTOs
            var walksDto = _mapper.Map<List<WalkDtoResponse>>(walksDomainModel);
            // Return the list of walks
            return Ok(walksDto);
        }

        // Get a specific walk by ID
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // Use the repository to get the walk by ID
            var walkDomainModel = await _unitOfWork.Walk.GetWalkByIdAsync(id);
            // Check if the walk exists
            if (walkDomainModel == null)
            {
                // If the walk does not exist, return a NotFound response
                return NotFound();
            }
            // Map the domain model to a DTO for the response
            var walkDto = _mapper.Map<WalkDtoResponse>(walkDomainModel);
            // Return the walk
            return Ok(walkDto);
        }

        // Create a new walk
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateWalkRequestDto createWalkRequestDto)
        {
            // Map the incoming DTO to the domain model
            var walkDomainModel = _mapper.Map<Walk>(createWalkRequestDto);
            // Use the repository to create the walk
            walkDomainModel = await _unitOfWork.Walk.CreateWalkAsync(walkDomainModel);
            // Save changes to the database
            await _unitOfWork.SaveAsync();
            // Map the created walk to a DTO for the response
            var walkDto = _mapper.Map<WalkDtoResponse>(walkDomainModel);
            // Return the created walk
            return CreatedAtAction(nameof(GetById), new { id = walkDto.Id }, walkDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
            // Map the incoming DTO to the domain model
            var walkDomainModel = _mapper.Map<Walk>(updateWalkRequestDto);
            // Use the repository to update the walk
            walkDomainModel = await _unitOfWork.Walk.UpdateWalkAsync(id, walkDomainModel);
            // Check if the walk was updated successfully
            if (walkDomainModel == null)
            {
                // If the walk does not exist, return a NotFound response
                return NotFound();
            }
            // Save changes to the database
            await _unitOfWork.SaveAsync();
            // Map the updated walk to a DTO for the response
            var walkDto = _mapper.Map<WalkDtoResponse>(walkDomainModel);
            // Return the updated walk
            return Ok(walkDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // Use the repository to delete the walk
            var deletedWalk = await _unitOfWork.Walk.DeleteWalkAsync(id);
            // Check if the walk was deleted successfully
            if (deletedWalk == null)
            {
                // If the walk does not exist, return a NotFound response
                return NotFound();
            }
            // Save changes to the database
            await _unitOfWork.SaveAsync();

            // Map the deleted walk to a DTO for the response
            var walkDto = _mapper.Map<WalkDtoResponse>(deletedWalk);

            // Return the deleted walk with a 200 OK status
            return Ok(walkDto);
        }
    }
}
