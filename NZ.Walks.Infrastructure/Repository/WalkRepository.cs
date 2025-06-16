using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EG.Walks.Domain.Entities;
using EG.Walks.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EG.Walks.Infrastructure.Repository
{
    public class WalkRepository
    {
        private readonly EGWalksDbContext _dbContext;
        public WalkRepository(EGWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // To Get All Walks
        public async Task<IEnumerable<Walk>> GetAllWalksAsync()
        {
            return await _dbContext.Walks.ToListAsync();
        }
        // To Add a new walk
        public async Task<Walk> CreateWalkAsync(Walk walk)
        {
            await _dbContext.Walks.AddAsync(walk);
            return walk; // Return the added walk
        }
    }
}
