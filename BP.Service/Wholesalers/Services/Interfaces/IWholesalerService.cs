using BP.Core.Domaine;
using System;
using System.Collections.Generic;
using System.Text;

namespace BP.Service.Wholesalers.Services.Interfaces
{
   public interface IWholesalerService
    {
        void SellNewBeer(SellNewBeerCommand command);
        void UpdateStock(SellNewBeerCommand command);

        Quotation GetQuotation(int wholesalerId,GetQuotationCommand command);
    }
}
