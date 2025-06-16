using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EG.Walks.Infrastructure.Data;
using EG.Walks.Infrastructure.Repository.IRepository;

namespace EG.Walks.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EGWalksDbContext _dbContext;
        public IRegionRepository Region { get; private set; }
        public IWalkRepository Walk { get; private set; }
        public UnitOfWork(EGWalksDbContext dbContext)
        {
            _dbContext = dbContext;
            Region = new RegionRepository(_dbContext);
            Walk = new WalkRepository(_dbContext);
        }
        public async Task SaveAsync()
        {
            // Save changes to the database asynchronously
            await _dbContext.SaveChangesAsync();
        }
    }
}
