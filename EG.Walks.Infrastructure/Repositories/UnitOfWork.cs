using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EG.Walks.Application.Interfaces;
using EG.Walks.Infrastructure.Data;
using EG.Walks.Infrastructure.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using EG.Walks.Infrastructure.Repository.IRepository;

namespace EG.Walks.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EGWalksDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IRegionRepository Region { get; private set; }
        public IWalkRepository Walk { get; private set; }
        public ITokenRepository Token { get; private set; }
        public IImageRepository Image { get; private set; }

        public UnitOfWork(EGWalksDbContext dbContext, IConfiguration configuration, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            Region = new RegionRepository(_dbContext);
            Walk = new WalkRepository(_dbContext);
            Image = new ImageRepository(_dbContext, _webHostEnvironment, _httpContextAccessor);
            Token = new TokenRepository(_configuration);
        }
        public async Task SaveAsync()
        {
            // Save changes to the database asynchronously
            await _dbContext.SaveChangesAsync();
        }
    }
}
