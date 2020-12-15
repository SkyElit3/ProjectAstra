using Microsoft.EntityFrameworkCore;
using ProjectAstra.Web.CrewApi.Core.Models;

namespace ProjectAstra.Web.CrewApi.Infrastructure.DataContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetIndexes(modelBuilder);
        }

        private void SetIndexes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shuttle>().HasIndex(shuttle => shuttle.Name);
        }

        public DbSet<Shuttle> Shuttles { get; set; }
    }
}