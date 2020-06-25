using System.Collections.Generic;
using System.Linq;
using Brasserie.Core.Domains;
using Brasserie.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Brasserie.Data.Repositories
{
    public class WholesalerRepository : IWholesalerRepository
    {
        private readonly BrasserieContext _brasserieContext;

        public WholesalerRepository(BrasserieContext brasserieContext)
        {
            _brasserieContext = brasserieContext;
        }

        public Wholesaler FindById(int id)
        {
            var wholesaler =  _brasserieContext.Wholesalers
                 .Include(e => e.WholesalerBeers)
                 .ThenInclude(e => e.Beer)
                 .SingleOrDefault(e => e.Id == id);
         
            return wholesaler;
        }

        public List<WholesalerBeer> GetAll()
        {
            var wholesalerbeers= _brasserieContext.WholesalerBeers
                 .Include(e => e.Wholesaler)
                 .ThenInclude(e => e.WholesalerBeers)
                 .ThenInclude(e => e.Beer)
                 .ToList();

            return wholesalerbeers;
        } 

        public void Add(WholesalerBeer wholesalerBeer)
        {
            _brasserieContext.WholesalerBeers.Add(wholesalerBeer);
            _brasserieContext.SaveChanges();
        }

        public void Update(WholesalerBeer wholesalerBeer)
        {
            _brasserieContext.WholesalerBeers.Update(wholesalerBeer);
            _brasserieContext.SaveChanges();
        }
    }
}
