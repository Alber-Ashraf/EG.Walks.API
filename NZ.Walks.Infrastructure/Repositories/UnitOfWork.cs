using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EG.Walks.Application.Interfaces;
using EG.Walks.Infrastructure.Data;
using EG.Walks.Infrastructure.Repositories;
using EG.Walks.Infrastructure.Repository.IRepository;
using Microsoft.Extensions.Configuration;

namespace EG.Walks.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EGWalksDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public IRegionRepository Region { get; private set; }
        public IWalkRepository Walk { get; private set; }
        public ITokenRepository Token { get; private set; }

        public UnitOfWork(EGWalksDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            Region = new RegionRepository(_dbContext);
            Walk = new WalkRepository(_dbContext);
            Token = new TokenRepository(_configuration);
        }
        public async Task SaveAsync()
        {
            // Save changes to the database asynchronously
            await _dbContext.SaveChangesAsync();
        }
    }
}
