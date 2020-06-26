using Brasserie.Core.Domains;
using System.Collections.Generic;

namespace Brasserie.Service.Wholesalers.Services.Interfaces
{
    public interface IWholesalerService
    {
        WholesalerBeer AddNewBeerToWholesaler(SellBeerCommand command);
        WholesalerBeer UpdateWholesalerBeer(UpdateStockCommand command);
        double GetQuotation(QuotationCommand command);
        List<WholesalerBeer> GetAll();
    }
}
