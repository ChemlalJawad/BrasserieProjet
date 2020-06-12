using Brasserie.Core.Domains;
using Brasserie.Data.Repositories.Interfaces;
using Brasserie.Service.Wholesalers.Services.Interfaces;

namespace Brasserie.Service.Wholesalers.Services
{
    public class WholesalerService : IWholesalerService
    {
        private readonly IWholesalerRepository _wholesalerRepository;
       
        public WholesalerService(IWholesalerRepository wholesalerRepository)
        {
            _wholesalerRepository = wholesalerRepository;
        }
             
        public void SellNewBeer(SellBeerOrUpdateStockCommand command)
        {
            var sellNewBeer = new WholesalerBeer()
            {
                BeerId = command.BeerId,
                WholesalerId = command.WholesalerId,
                Stock = command.Stock
            };
            _wholesalerRepository.SellNewBeer(sellNewBeer);
        }

        public void UpdateStock(SellBeerOrUpdateStockCommand command)
        {
            var updateStock = new WholesalerBeer()
            {
                BeerId = command.BeerId,
                WholesalerId = command.WholesalerId
            };
            _wholesalerRepository.UpdateStock(updateStock);
        }

        //TODO A faire GetQuotation
    }
}
