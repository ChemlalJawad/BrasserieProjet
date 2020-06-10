using BP.Core.Domaine;
using System;
using System.Collections.Generic;
using System.Text;

namespace BP.Data.Repositories.Interfaces
{
    public interface IWholesalerRepository
    {
        void SellNewBeer(WholesalerBeer wholesalerBeer);
        void UpdateStock(WholesalerBeer wholesalerBeer);

        Wholesaler Find(int Id);
       
    }
}
