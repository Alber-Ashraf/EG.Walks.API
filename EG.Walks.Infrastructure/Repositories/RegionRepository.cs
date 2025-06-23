using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EG.Walks.Domain.Entities;
using EG.Walks.Infrastructure.Data;
using EG.Walks.Infrastructure.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace EG.Walks.Infrastructure.Repository
{
    public class RegionRepository : IRegionRepository
    {
        private readonly EGWalksDbContext _dbContext;
        public RegionRepository(EGWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Add methods for region-specific data access here
        // To get all regions
        public async Task<IEnumerable<Region>> GetAllRegionsAsync()
        {
            return await _dbContext.Regions.ToListAsync();
        }
        // To get a specific region by ID
        public async Task<Region?> GetRegionByIdAsync(Guid id)
        {
            return await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }
        // To Add a new region
        public async Task<Region> CreateRegionAsync(Region region)
        {
            await _dbContext.Regions.AddAsync(region);
            return region; // Return the added region
        }
        // To Update an existing region
        public async Task<Region?> UpdateRegionAsync(Guid id, Region region)
        {
            var existingRegion = await _dbContext.Regions.FindAsync(id);
            // Check if the region exists
            if (existingRegion == null) {
                return null; // Return null if the region does not exist
            }
            // Update the existing region with the new values
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            return existingRegion;
        }
        // To Delete a region by ID
        public async Task<Region?> DeleteRegionAsync(Guid id)
        {
            // Find the region by ID
            var region = await _dbContext.Regions.FindAsync(id);
            // If the region does not exist, return null
            if (region == null)
                return null;

            // Remove the region from the database context
            _dbContext.Regions.Remove(region);
            return region;
        }
    }
}
