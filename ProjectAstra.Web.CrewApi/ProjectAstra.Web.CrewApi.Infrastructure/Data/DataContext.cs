using Microsoft.EntityFrameworkCore;
using ProjectAstra.Web.CrewApi.Core.Enums;
using ProjectAstra.Web.CrewApi.Core.Models;
using ProjectAstra.Web.CrewApi.Infrastructure.Extensions;

namespace ProjectAstra.Web.CrewApi.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Shuttle> Shuttles { get; set; }

        public DbSet<TeamOfExplorers> TeamsOfExplorers { get; set; }

        public DbSet<Explorer> Explorers { get; set; }

        public DbSet<HumanCaptain> HumanCaptains { get; set; }

        public DbSet<Robot> Robots { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetModelRelations(modelBuilder);
            SetIndexes(modelBuilder);
            SetDiscriminators(modelBuilder);
        }

        private static void SetModelRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeamOfExplorers>()
                .HasOne(s => s.Shuttle)
                .WithOne(t => t.TeamOfExplorers)
                .HasForeignKey<TeamOfExplorers>(t => t.ShuttleId);
            
            modelBuilder.Entity<Explorer>()
                .HasOne(t => t.TeamOfExplorers)
                .WithMany(t => t.Explorers)
                .HasForeignKey(t => t.TeamOfExplorersId);
        }

        private static void SetIndexes(ModelBuilder modelBuilder)
        {
            modelBuilder.SetIndexForEntities(nameof(IEntity.Name));
        }

        private static void SetDiscriminators(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Explorer>()
                .HasDiscriminator<ExplorerType>(nameof(ExplorerType))
                .HasValue<HumanCaptain>(ExplorerType.HumanCaptain)
                .HasValue<Robot>(ExplorerType.Robot);
        }
    }
}