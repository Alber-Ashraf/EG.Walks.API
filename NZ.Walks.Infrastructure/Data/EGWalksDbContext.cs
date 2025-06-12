using EG.Walks.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EG.Walks.Infrastructure.Data
{
    public class EGWalksDbContext : DbContext
    {
        public EGWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
    }
}
