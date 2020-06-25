using System.Collections.Generic;
using System.Linq;
using Brasserie.Core.Domains;
using Brasserie.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Brasserie.Data.Repositories
{
    public  class BrewerRepository : IBrewerRepository
    {
        private readonly BrasserieContext _brasserieContext;

        public BrewerRepository(BrasserieContext brasserieContext)
        {
            _brasserieContext = brasserieContext;
        }

        public Brewer FindById(int id)
        {
            var brewer = _brasserieContext.Brewers
                .Include(e => e.Beers)
                .ThenInclude(e => e.WholesalerBeers)
                .SingleOrDefault(e => e.Id == id);
            
            return brewer;
        }

        public IEnumerable<Brewer> GetAllBeers()
        {
            var beers = _brasserieContext.Brewers
                .Include(e => e.Beers)
                .ThenInclude(e => e.WholesalerBeers)
                .ThenInclude( e=> e.Wholesaler)
                .ToList();

            return beers;
        }
    }
}
