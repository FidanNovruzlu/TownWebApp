using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TownWebApp.Models;

namespace TownWebApp.DAL
{
    public class TownDbContext:IdentityDbContext<AppUser>
    {
        public TownDbContext(DbContextOptions<TownDbContext> options):base(options)
        {

        }
        public DbSet<Introdaction> Introdactions { get; set; }
        public DbSet<Setting> Settings { get; set; }
    }
}
