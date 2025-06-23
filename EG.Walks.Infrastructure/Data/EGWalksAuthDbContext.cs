using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EG.Walks.Infrastructure.Data
{
    public class EGWalksAuthDbContext : IdentityDbContext
    {
        public EGWalksAuthDbContext(DbContextOptions<EGWalksAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var adminRoleId = "787e36e7-fd00-4aea-bbd2-a9e649b0e7f2";
            var userRoleId = "b1c3f8d2-4c5e-4f6a-8b7c-9d0e1f2a3b4c";

            // Define roles with unique IDs and concurrency stamps
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId,
                    Name = "User",
                    NormalizedName = "USER"
                }
            };

            // Seed roles into the database
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
