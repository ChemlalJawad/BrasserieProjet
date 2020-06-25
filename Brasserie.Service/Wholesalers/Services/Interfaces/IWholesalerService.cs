using Brasserie.Core.Domains;
using System;
using System.Collections.Generic;

namespace Brasserie.Service.Wholesalers.Services.Interfaces
{
   public interface IWholesalerService
    {
        void SellNewBeer(SellBeerCommand command);
        WholesalerBeer UpdateStock(UpdateStockCommand command);
        double GetQuotation(QuotationCommand command);
        List<WholesalerBeer> GetAll();
        List<Beer> GetAlls();
    }
}
