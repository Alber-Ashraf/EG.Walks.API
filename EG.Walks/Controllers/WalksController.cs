using AutoMapper;
using EG.Walks.Domain.DTOs;
using EG.Walks.Domain.Entities;
using EG.Walks.Infrastructure.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EG.Walks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            // Use the repository to get all walks
            var walksDomainModel = await _unitOfWork.Walk.GetAllWalksAsync();
            // Check if the list of walks is empty
            if (walksDomainModel == null || !walksDomainModel.Any())
            {
                // If the list is empty, return a NotFound response
                return NotFound("No walks found.");
            }
            // Map the domain model to a list of DTOs
            var walksDto = _mapper.Map<List<WalkDto>>(walksDomainModel);
            // Return the list of walks
            return Ok(walksDto);
        }

        // Create a new walk
        [HttpPost]
        public async Task<IActionResult> CreateWalkAsync([FromBody] CreateWalkRequestDto createWalkRequestDto )
        {
            // Map the incoming DTO to the domain model
            var walkDomainModel = _mapper.Map<Walk>(createWalkRequestDto);
            // Use the repository to create the walk
            walkDomainModel = await _unitOfWork.Walk.CreateWalkAsync(walkDomainModel);
            // Save changes to the database
            await _unitOfWork.SaveAsync();
            // Map the created walk to a DTO for the response
            var walkDto = _mapper.Map<WalkDto>(walkDomainModel);
            // Return the created walk
            return CreatedAtAction(nameof(GetById), new { id = walkDto.Id }, walkDto);
        }
    }
}
