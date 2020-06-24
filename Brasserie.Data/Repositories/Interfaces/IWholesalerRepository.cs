using Brasserie.Core.Domains;
using System.Collections.Generic;

namespace Brasserie.Data.Repositories.Interfaces
{
    public interface IWholesalerRepository
    {
        void SellNewBeer(WholesalerBeer wholesalerBeer);
        void UpdateStock(WholesalerBeer wholesalerBeer);
        Wholesaler FindById(int Id);
        List<WholesalerBeer> GetAll();
    }
}
