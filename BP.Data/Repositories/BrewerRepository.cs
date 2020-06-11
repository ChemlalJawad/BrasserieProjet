using BP.Core.Domains;
using BP.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BP.Data.Repositories
{
    public  class BrewerRepository : IBrewerRepository
    {
        private readonly BrasserieContext _brasserieContext;

        public BrewerRepository(BrasserieContext brasserieContext)
        {
            _brasserieContext = brasserieContext;
        }

        public Brewer FindBrewerById(int Id)
        {
            var brewer = _brasserieContext.Brewers
                .Include(e => e.Beers)
                .ThenInclude(e => e.WholesalerBeers)
                .FirstOrDefault(e => e.Id == Id);
            
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
