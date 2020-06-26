﻿// <auto-generated />
using System;
using Brasserie.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BP.Data.Migrations
{
    [DbContext(typeof(BrasserieContext))]
    [Migration("20200626113326_ChangeContext")]
    partial class ChangeContext
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Brasserie.Core.Domains.Beer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("AlcoholPercentage")
                        .HasColumnType("float");

                    b.Property<int?>("BrewerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("BrewerId");

                    b.ToTable("Beers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AlcoholPercentage = 6.5999999999999996,
                            BrewerId = 1,
                            Name = "Leffe Blonde",
                            Price = 2.2000000000000002
                        },
                        new
                        {
                            Id = 2,
                            AlcoholPercentage = 8.5999999999999996,
                            BrewerId = 1,
                            Name = "Leffe Brune",
                            Price = 2.7999999999999998
                        },
                        new
                        {
                            Id = 3,
                            AlcoholPercentage = 7.5,
                            BrewerId = 2,
                            Name = "Chouffe",
                            Price = 3.1000000000000001
                        },
                        new
                        {
                            Id = 4,
                            AlcoholPercentage = 8.8000000000000007,
                            BrewerId = 3,
                            Name = "Chimay Bleue",
                            Price = 3.0
                        },
                        new
                        {
                            Id = 5,
                            AlcoholPercentage = 7.9000000000000004,
                            BrewerId = 3,
                            Name = "Chimay Brune",
                            Price = 2.7999999999999998
                        });
                });

            modelBuilder.Entity("Brasserie.Core.Domains.Brewer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Brewers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Abbaye de Leffe"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Achouffe"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Abbaye Notre-Dame de Scourmont"
                        });
                });

            modelBuilder.Entity("Brasserie.Core.Domains.Wholesaler", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Wholesalers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "HappyHour"
                        },
                        new
                        {
                            Id = 2,
                            Name = "GetDrunk"
                        });
                });

            modelBuilder.Entity("Brasserie.Core.Domains.WholesalerBeer", b =>
                {
                    b.Property<int>("BeerId")
                        .HasColumnType("int");

                    b.Property<int>("WholesalerId")
                        .HasColumnType("int");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("BeerId", "WholesalerId");

                    b.HasIndex("WholesalerId");

                    b.ToTable("WholesalerBeers");

                    b.HasData(
                        new
                        {
                            BeerId = 1,
                            WholesalerId = 1,
                            Stock = 38
                        },
                        new
                        {
                            BeerId = 1,
                            WholesalerId = 2,
                            Stock = 12
                        },
                        new
                        {
                            BeerId = 2,
                            WholesalerId = 1,
                            Stock = 18
                        },
                        new
                        {
                            BeerId = 2,
                            WholesalerId = 2,
                            Stock = 21
                        },
                        new
                        {
                            BeerId = 3,
                            WholesalerId = 1,
                            Stock = 5
                        },
                        new
                        {
                            BeerId = 4,
                            WholesalerId = 2,
                            Stock = 12
                        },
                        new
                        {
                            BeerId = 3,
                            WholesalerId = 2,
                            Stock = 18
                        },
                        new
                        {
                            BeerId = 5,
                            WholesalerId = 1,
                            Stock = 16
                        });
                });

            modelBuilder.Entity("Brasserie.Core.Domains.Beer", b =>
                {
                    b.HasOne("Brasserie.Core.Domains.Brewer", "Brewer")
                        .WithMany("Beers")
                        .HasForeignKey("BrewerId");
                });

            modelBuilder.Entity("Brasserie.Core.Domains.WholesalerBeer", b =>
                {
                    b.HasOne("Brasserie.Core.Domains.Beer", "Beer")
                        .WithMany("WholesalerBeers")
                        .HasForeignKey("BeerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Brasserie.Core.Domains.Wholesaler", "Wholesaler")
                        .WithMany("WholesalerBeers")
                        .HasForeignKey("WholesalerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}