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
    [Migration("20201216110648_AddShuttleDbSet")]
    partial class AddShuttleDbSet
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
#pragma warning restore 612, 618
        }
    }
}
