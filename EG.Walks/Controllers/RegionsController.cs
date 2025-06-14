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

            // Check if the list is null or empty
            if (regionDTOs == null || !regionDTOs.Any())
            {
                return NotFound("No regions found.");
            }
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

            // Check if the region with the specified ID exists
            if (regionDTO == null)
            {
                return NotFound($"Region with ID {id} not found.");
            }

            // Return the region details
            return Ok(regionDTO);
        }
    }
}
