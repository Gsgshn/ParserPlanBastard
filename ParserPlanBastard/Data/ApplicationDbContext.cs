using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ParserPlanBastard.Models.Entities;

namespace ParserPlanBastard.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>,int>
    {
        public ApplicationDbContext(DbContextOptions options)
            :base(options)       
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Logging> Logging { get; set; }
        public DbSet<Models.Entities.File> Files { get; set; }
        public DbSet<Element> Elements { get; set; }
    }
}
