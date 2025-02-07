﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SimulasiBiayaImpor.SimulasiContext;

#nullable disable

namespace SimulasiBiayaImpors.Migrations
{
    [DbContext(typeof(SimulasiContext))]
    partial class SimulasiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SimulasiBiayaImpors.Models.BiayaImpor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Bm")
                        .HasColumnType("int");

                    b.Property<string>("KodeBarang")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<float>("NilaiBm")
                        .HasColumnType("real");

                    b.Property<float>("NilaiKomoditas")
                        .HasColumnType("real");

                    b.Property<string>("UraianBarang")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("WaktuInsert")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("BiayaImpors");
                });
#pragma warning restore 612, 618
        }
    }
}
