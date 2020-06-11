using BP.Core.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BP.Service.Wholesalers.Services.Interfaces
{
   public interface IWholesalerService
    {
        void SellNewBeer(SellBeerOrUpdateStockCommand command);
        void UpdateStock(SellBeerOrUpdateStockCommand command);
    }
}
