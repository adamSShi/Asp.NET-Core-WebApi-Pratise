using Microsoft.EntityFrameworkCore;
using PratiseAPI.Models.Domain;

namespace PratiseAPI.Data
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) 
        {
        }

        public DbSet<Difficulty> Difficulties { get; set; }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}
