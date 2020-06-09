using BP.Core.Domaine;
using BP.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
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
