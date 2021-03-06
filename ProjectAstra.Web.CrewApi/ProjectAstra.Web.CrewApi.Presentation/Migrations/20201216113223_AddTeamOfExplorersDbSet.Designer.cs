﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectAstra.Web.CrewApi.Infrastructure.Data;

namespace ProjectAstra.Web.CrewApi.Presentation.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20201216113223_AddTeamOfExplorersDbSet")]
    partial class AddTeamOfExplorersDbSet
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

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
