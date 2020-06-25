using Brasserie.Core.Domains;
using System.Collections.Generic;

namespace Brasserie.Data.Repositories.Interfaces
{
    public interface IWholesalerRepository
    {
        void Add(WholesalerBeer wholesalerBeer);
        void Update(WholesalerBeer wholesalerBeer);
        Wholesaler FindById(int id);
        List<WholesalerBeer> GetAll();
    }
}
