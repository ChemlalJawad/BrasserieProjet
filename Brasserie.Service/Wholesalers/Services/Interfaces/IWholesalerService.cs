using System;

namespace Brasserie.Service.Wholesalers.Services.Interfaces
{
   public interface IWholesalerService
    {
        void SellNewBeer(SellBeerCommand command);
        void UpdateStock(UpdateStockCommand command);
        double GetQuotation(QuotationCommand command);
    }
}
