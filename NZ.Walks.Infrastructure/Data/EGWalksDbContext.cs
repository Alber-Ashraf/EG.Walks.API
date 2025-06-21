using EG.Walks.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EG.Walks.Infrastructure.Data
{
    public class EGWalksDbContext : DbContext
    {
        public EGWalksDbContext(DbContextOptions<EGWalksDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var regions = new List<Region>()
            {
                new Region()
                {
                    Id = Guid.Parse("249ddc21-7b4a-436c-8d26-421dbdd66daf"),
                    Code = "S01",
                    Name = "South Sinai",
                    RegionImageUrl = "https://example.com/images/southsinai.jpg"
                },
                new Region()
                {
                    Id = Guid.Parse("eff6c0e2-8bec-4ae3-97d0-5d096d82b337"),
                    Code = "S03",
                    Name = "Sohage",
                    RegionImageUrl = "https://example.com/images/sohage.jpg"
                },
                new Region()
                {
                    Id = Guid.Parse("6583823e-a0ff-4fd7-9d4c-92dcbc6f4638"),
                    Code = "R01",
                    Name = "Red Sea",
                    RegionImageUrl = "https://example.com/images/redsea.jpg"
                },
                new Region()
                {
                    Id = Guid.Parse("97a16dca-646b-4f3d-bd42-9d684a8ca1fd"),
                    Code = "G01",
                    Name = "Giza",
                    RegionImageUrl = "https://example.com/images/giza.jpg"
                },
                new Region()
                {
                    Id = Guid.Parse("744a5216-4c28-4235-8d52-9edb90073da0"),
                    Code = "A01",
                    Name = "Alexandria",
                    RegionImageUrl = "https://example.com/images/alexandria.jpg"
                },
                new Region()
                {
                    Id = Guid.Parse("fb1394dc-9e33-41c1-9a9a-a3faf8eac4e6"),
                    Code = "A02",
                    Name = "Aswan",
                    RegionImageUrl = "https://example.com/images/aswan.jpg"
                },
                new Region()
                {
                    Id = Guid.Parse("a7b29cd6-3ca8-4a65-8f5b-a7962e0ab63b"),
                    Code = "L01",
                    Name = "Luxor",
                    RegionImageUrl = "https://example.com/images/luxor.jpg"
                },
                new Region()
                {
                    Id = Guid.Parse("8d117c3d-47a3-4726-965b-d1b073168bb6"),
                    Code = "S02",
                    Name = "Sharqia",
                    RegionImageUrl = "https://example.com/images/sharqia.jpg"
                },
                new Region()
                {
                    Id = Guid.Parse("ad5bdb93-c574-4b9d-8075-d9035704969c"),
                    Code = "C01",
                    Name = "Cairo",
                    RegionImageUrl = "https://example.com/images/cairo.jpg"
                },
                new Region()
                {
                    Id = Guid.Parse("c4d3ff86-415b-40fe-8681-dc058d5de9d7"),
                    Code = "Q01",
                    Name = "Quina",
                    RegionImageUrl = "https://example.com/images/quina.jpg"
                }
            };
            modelBuilder.Entity<Region>().HasData(regions);

            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("d9801dca-5af2-446f-9478-580dd32414ff"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("08ad254f-4369-4654-80dd-ffce9e1ca07f"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("849cb121-a3d5-4d6d-a27b-e9b1623e9237"),
                    Name = "Hard"
                }
            };
            modelBuilder.Entity<Difficulty>().HasData(difficulties);
        }
    }
}
