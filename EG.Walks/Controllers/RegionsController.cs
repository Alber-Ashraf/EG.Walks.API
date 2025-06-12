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
            var reagions = _dbContext.Regions.ToList();
            if (reagions == null || !reagions.Any())
            {
                return NotFound("No regions found.");
            }
            return Ok(reagions);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var region = _dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (region == null)
            {
                return NotFound($"Region with ID {id} not found.");
            }
            return Ok(region);
        }
    }
}
