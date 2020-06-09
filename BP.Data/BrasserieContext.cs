using BP.Core.Domaine;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BP.Data
{
    public class BrasserieContext : DbContext
    {
        public BrasserieContext(DbContextOptions<BrasserieContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Beer>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Beer>()
                .HasOne(e => e.Brewer)
                .WithMany(e => e.Beers);

            modelBuilder.Entity<Brewer>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Wholesaler>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Brewer>()
                .HasMany(e => e.Beers)
                .WithOne(e => e.Brewer);

            modelBuilder.Entity<WholesalerBeer>()
                .HasKey(e => new { e.BeerId, e.WholesalerId });

            modelBuilder.Entity<WholesalerBeer>()
                .HasOne(e => e.Beer)
                .WithMany(e => e.WholesalerBeers)
                .HasForeignKey(e => e.BeerId);

            modelBuilder.Entity<WholesalerBeer>()
                .HasOne(e => e.Wholesaler)
                .WithMany(e => e.WholesalerBeers)
                .HasForeignKey(e => e.WholesalerId);

       
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Seed();
        }

        public DbSet<Beer> Beers { get; set; }
        public DbSet<Brewer> Brewers { get; set; }
        public DbSet<Wholesaler> Wholesalers { get; set; }
        public DbSet<WholesalerBeer> WholesalerBeers { get; set; }
    }
}
