using Brasserie.Core.Domains;
using Brasserie.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace UnitTesting
{
    public class ServiceContext
    {
        protected DbContextOptions<BrasserieContext> ContextOptions { get; }

        protected ServiceContext(DbContextOptions<BrasserieContext> contextOptions)
        {
            ContextOptions = contextOptions;

            Seed();
        }

        private void Seed()
        {
            using (var context = new BrasserieContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

            }

        }
    }
}