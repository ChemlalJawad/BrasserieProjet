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
