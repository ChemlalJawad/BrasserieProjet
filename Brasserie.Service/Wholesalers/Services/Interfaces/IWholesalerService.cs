namespace Brasserie.Service.Wholesalers.Services.Interfaces
{
   public interface IWholesalerService
    {
        void SellNewBeer(SellBeerOrUpdateStockCommand command);
        void UpdateStock(SellBeerOrUpdateStockCommand command);
    }
}
