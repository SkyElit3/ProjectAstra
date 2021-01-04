﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectAstra.Web.CrewApi.Core.Enums;
using ProjectAstra.Web.CrewApi.Infrastructure.Data;

namespace ProjectAstra.Web.CrewApi.Presentation.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210104151612_UpdateExplorerType")]
    partial class UpdateExplorerType
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<byte>("ExplorerType")
                        .HasColumnType("tinyint unsigned");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(256)");

                    b.Property<Guid>("TeamOfExplorersId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("TeamOfExplorersId");

                    b.ToTable("Explorers");

                    b.HasDiscriminator<byte>("ExplorerType");
                });

            modelBuilder.Entity("ProjectAstra.Web.CrewApi.Core.Models.Shuttle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("MaxCrewCapacity")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Shuttles");
                });

            modelBuilder.Entity("ProjectAstra.Web.CrewApi.Core.Models.TeamOfExplorers", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(256)");

                    b.Property<Guid>("ShuttleId")
                        .HasColumnType("char(36)");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint unsigned");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("ShuttleId")
                        .IsUnique();

                    b.ToTable("TeamsOfExplorers");
                });

            modelBuilder.Entity("ProjectAstra.Web.CrewApi.Core.Models.HumanCaptain", b =>
                {
                    b.HasBaseType("ProjectAstra.Web.CrewApi.Core.Models.Explorer");

                    b.Property<string>("Grade")
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Password")
                        .HasColumnType("varchar(256)");

                    b.HasDiscriminator().HasValue((byte)0);
                });

            modelBuilder.Entity("ProjectAstra.Web.CrewApi.Core.Models.Robot", b =>
                {
                    b.HasBaseType("ProjectAstra.Web.CrewApi.Core.Models.Explorer");

                    b.Property<byte>("Type")
                        .HasColumnType("tinyint unsigned");

                    b.Property<int>("UnitsOfEnergy")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue((byte)1);
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
