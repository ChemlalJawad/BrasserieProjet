﻿// <auto-generated />
using System;
using BP.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BP.Data.Migrations
{
    [DbContext(typeof(BrasserieContext))]
    [Migration("20200608105448_InitialContext")]
    partial class InitialContext
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BP.Core.Domaine.Beer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("AlcoolPercentage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("BrewerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("BrewerId");

                    b.ToTable("Beers");
                });

            modelBuilder.Entity("BP.Core.Domaine.Brewer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Brewers");
                });

            modelBuilder.Entity("BP.Core.Domaine.Wholesaler", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Wholesalers");
                });

            modelBuilder.Entity("BP.Core.Domaine.WholesalerBeer", b =>
                {
                    b.Property<int>("BeerId")
                        .HasColumnType("int");

                    b.Property<int>("WholesalerId")
                        .HasColumnType("int");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("BeerId", "WholesalerId");

                    b.HasIndex("WholesalerId");

                    b.ToTable("WholesalerBeer");
                });

            modelBuilder.Entity("BP.Core.Domaine.Beer", b =>
                {
                    b.HasOne("BP.Core.Domaine.Brewer", "Brewer")
                        .WithMany("Beers")
                        .HasForeignKey("BrewerId");
                });

            modelBuilder.Entity("BP.Core.Domaine.WholesalerBeer", b =>
                {
                    b.HasOne("BP.Core.Domaine.Beer", "Beer")
                        .WithMany("WholesalerBeers")
                        .HasForeignKey("BeerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BP.Core.Domaine.Wholesaler", "Wholesaler")
                        .WithMany("WholesalerBeers")
                        .HasForeignKey("WholesalerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}