using BP.Core.Domaine;
using BP.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BP.Data.Repositories
{
    public class WholesalerRepository : IWholesalerRepository
    {
        private readonly BrasserieContext _brasserieContext;

        public WholesalerRepository(BrasserieContext brasserieContext)
        {
            _brasserieContext = brasserieContext;
        }

        public Wholesaler Find(int Id)
        {
          var wholesaler =  _brasserieContext.Wholesalers
                .Include(e => e.WholesalerBeers)
                .ThenInclude(e => e.Beer)
                .SingleOrDefault(e => e.Id == Id);

            if (wholesaler == null) throw new Exception("wholesaler null");
           return wholesaler;
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
