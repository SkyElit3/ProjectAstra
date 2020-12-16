﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectAstra.Web.CrewApi.Infrastructure.DataContext;

namespace ProjectAstra.Web.CrewApi.Presentation.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ProjectAstra.Web.CrewApi.Core.Models.Explorer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ExplorerType")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET latin1");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET latin1");

                    b.Property<Guid>("TeamOfExplorersId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("TeamOfExplorersId");

                    b.ToTable("Explorers");

                    b.HasDiscriminator<string>("ExplorerType").HasValue("Explorer");
                });

            modelBuilder.Entity("ProjectAstra.Web.CrewApi.Core.Models.Shuttle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("MaxCrewCapacity")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255) CHARACTER SET latin1");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("Shuttles");
                });

            modelBuilder.Entity("ProjectAstra.Web.CrewApi.Core.Models.TeamOfExplorers", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255) CHARACTER SET latin1");

                    b.Property<Guid>("ShuttleId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Status")
                        .HasColumnType("longtext CHARACTER SET latin1");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.HasIndex("ShuttleId")
                        .IsUnique();

                    b.ToTable("TeamsOfExplorers");
                });

            modelBuilder.Entity("ProjectAstra.Web.CrewApi.Core.Models.HumanCaptain", b =>
                {
                    b.HasBaseType("ProjectAstra.Web.CrewApi.Core.Models.Explorer");

                    b.Property<string>("Grade")
                        .HasColumnType("longtext CHARACTER SET latin1");

                    b.Property<string>("Password")
                        .HasColumnType("longtext CHARACTER SET latin1");

                    b.HasDiscriminator().HasValue("HumanCaptain");
                });

            modelBuilder.Entity("ProjectAstra.Web.CrewApi.Core.Models.Robot", b =>
                {
                    b.HasBaseType("ProjectAstra.Web.CrewApi.Core.Models.Explorer");

                    b.Property<string>("Type")
                        .HasColumnType("longtext CHARACTER SET latin1");

                    b.Property<int>("UnitsOfEnergy")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Robot");
                });

            modelBuilder.Entity("ProjectAstra.Web.CrewApi.Core.Models.Explorer", b =>
                {
                    b.HasOne("ProjectAstra.Web.CrewApi.Core.Models.TeamOfExplorers", "TeamOfExplorers")
                        .WithMany("Explorers")
                        .HasForeignKey("TeamOfExplorersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectAstra.Web.CrewApi.Core.Models.TeamOfExplorers", b =>
                {
                    b.HasOne("ProjectAstra.Web.CrewApi.Core.Models.Shuttle", "Shuttle")
                        .WithOne("TeamOfExplorers")
                        .HasForeignKey("ProjectAstra.Web.CrewApi.Core.Models.TeamOfExplorers", "ShuttleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
