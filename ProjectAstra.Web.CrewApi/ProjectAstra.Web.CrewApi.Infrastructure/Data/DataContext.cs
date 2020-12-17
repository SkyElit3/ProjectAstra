﻿using Microsoft.EntityFrameworkCore;
using ProjectAstra.Web.CrewApi.Core.Enums;
using ProjectAstra.Web.CrewApi.Core.Models;

namespace ProjectAstra.Web.CrewApi.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Shuttle> Shuttles { get; set; }

        public DbSet<TeamOfExplorers> TeamsOfExplorers { get; set; }

        public DbSet<Explorer> Explorers { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetIndexes(modelBuilder);
            modelBuilder.Entity<Explorer>()
                .HasDiscriminator<ExplorerTypeEnum>("ExplorerType")
                .HasValue<HumanCaptain>(ExplorerTypeEnum.HumanCaptain)
                .HasValue<Robot>(ExplorerTypeEnum.Robot);
        }

        private void SetIndexes(ModelBuilder modelBuilder)
        {
            // TODO : add reflection to assign all names as unique indexes
            modelBuilder.Entity<Shuttle>().HasIndex(shuttle => shuttle.Name).IsUnique();
            modelBuilder.Entity<TeamOfExplorers>().HasIndex(team => team.Name).IsUnique();
            modelBuilder.Entity<Explorer>().HasIndex(explorer => explorer.Name).IsUnique();
        }
    }
}