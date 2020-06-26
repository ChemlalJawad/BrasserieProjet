using Brasserie.Core.Domains;
using Brasserie.Data;
using Brasserie.Service.Brewers.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Brasserie.Service.Brewers.Services
{
    public class BrewerService : IBrewerService
    {
        private readonly BrasserieContext _brasserieContext;

        public BrewerService(BrasserieContext brasserieContext)
        {
            _brasserieContext = brasserieContext;
        }

        public Brewer FindBrewerById(int id)
        {         
            return _brasserieContext.Brewers
                .Include(e => e.Beers)
                .ThenInclude(e => e.WholesalerBeers)
                .SingleOrDefault(e => e.Id == id);
        }

        public IEnumerable<Brewer> GetAllBeers()
        {
            return _brasserieContext.Brewers
                .Include(e => e.Beers)
                .ThenInclude(e => e.WholesalerBeers)
                .ThenInclude(e => e.Wholesaler)
                .ToList(); 
        }
    }
}
