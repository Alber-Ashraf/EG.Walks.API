using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EG.Walks.Infrastructure.Data
{
    public class EGWalksAuthDbContext : IdentityDbContext
    {
        public EGWalksAuthDbContext(DbContextOptions<EGWalksAuthDbContext> options) : base(options)
        {
        }
    }
}
