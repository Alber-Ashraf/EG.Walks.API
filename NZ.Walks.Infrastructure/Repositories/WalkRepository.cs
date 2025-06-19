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
    public class WalkRepository : IWalkRepository
    {
        private readonly EGWalksDbContext _dbContext;
        public WalkRepository(EGWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // To Get All Walks
        public async Task<IEnumerable<Walk>> GetAllWalksAsync(string? filerOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true)
        {
            var walks = _dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            // Apply filtering if filterOn and filterQuery are provided
            if(string.IsNullOrWhiteSpace(filerOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if(filerOn.Equals("name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
                else if (filerOn.Equals("lengthInKm", StringComparison.OrdinalIgnoreCase) && double.TryParse(filterQuery, out double length))
                {
                    walks = walks.Where(x => x.LengthInKm == length);
                }
            }

            // Apply sorting if sortBy is provided
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending == true
                        ? walks.OrderBy(x => x.Name)
                        : walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("LengthInKm", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending == true
                        ? walks.OrderBy(x => x.LengthInKm)
                        : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            return await walks.ToListAsync();
        }
        // To Get a specific walk by ID
        public async Task<Walk?> GetWalkByIdAsync(Guid id)
        {
            return await _dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }
        // To Add a new walk
        public async Task<Walk> CreateWalkAsync(Walk walk)
        {
            await _dbContext.Walks.AddAsync(walk);
            return walk; // Return the added walk
        }
        // To Update an existing walk
        public async Task<Walk?> UpdateWalkAsync(Guid id, Walk walk)
        {
            var existingWalk = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            // Check if the walk exists
            if (existingWalk == null)
            {
                return null; // Return null if the walk does not exist
            }
            // Update the existing walk with the new values
            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.RegionId = walk.RegionId;

            return existingWalk;
        }
        // To Delete a walk by ID
        public async Task<Walk?> DeleteWalkAsync(Guid id)
        {
            // Find the walk by ID
            var walk = await _dbContext.Walks.FindAsync(id);
            // If the walk does not exist, return null
            if (walk == null)
                return null;
            // Remove the walk from the DbContext
            _dbContext.Walks.Remove(walk);
            return walk; // Return the deleted walk
        }
    }
}
