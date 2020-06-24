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

        public Wholesaler FindById(int Id)
        {
            var wholesaler =  _brasserieContext.Wholesalers
                .Include(e => e.WholesalerBeers)
                .ThenInclude(e => e.Beer)
                .SingleOrDefault(e => e.Id == Id);
         
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
        public List<Wholesaler> GetAllWholesalers()
        {
           var wholesalers= _brasserieContext.Wholesalers
                 .Include(e => e.WholesalerBeers)
                 .ThenInclude(e => e.Beer)
                 .ToList();

           return wholesalers;
        }

        public void SellNewBeer(WholesalerBeer wholesalerBeer)
        {
            _brasserieContext.WholesalerBeers.Add(wholesalerBeer);
            _brasserieContext.SaveChanges();
        }

        public void UpdateStock(WholesalerBeer wholesalerBeer)
        {
            _brasserieContext.WholesalerBeers.Update(wholesalerBeer);
            _brasserieContext.SaveChanges();
        }
    }
}
