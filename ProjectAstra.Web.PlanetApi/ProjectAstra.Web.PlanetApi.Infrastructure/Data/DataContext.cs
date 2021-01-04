using Microsoft.EntityFrameworkCore;
using ProjectAstra.Web.PlanetApi.Core.Models;
using ProjectAstra.Web.PlanetApi.Infrastructure.Extensions;

namespace ProjectAstra.Web.PlanetApi.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Planet> Planets { get; set; }

        public DbSet<SolarSystem> SolarSystems { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetModelRelations(modelBuilder);
            SetIndexes(modelBuilder);
        }

        private void SetModelRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Planet>()
                .HasOne(p => p.SolarSystem)
                .WithMany(s => s.Planets)
                .HasForeignKey(p => p.SolarSystemId);
        }

        private void SetIndexes(ModelBuilder modelBuilder)
        {
            modelBuilder.SetIndexForEntities(nameof(IEntity.Name));
        }
    }
}