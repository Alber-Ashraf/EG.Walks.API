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
    }
}
