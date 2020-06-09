using BP.Core.Domaine;
using BP.Data.Repositories.Interfaces;
using BP.Service.Wholesalers.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BP.Service.Wholesalers.Services
{
    public class WholesalerService : IWholesalerService
    {
        private readonly IWholesalerRepository _wholesalerRepository;

        public WholesalerService(IWholesalerRepository wholesalerRepository)
        {
            _wholesalerRepository = wholesalerRepository;
        }

        public void SellNewBeer(SellNewBeerCommand command)
        {
            var sellNewBeer = new WholesalerBeer()
            {
                BeerId = command.BeerId,
                WholesalerId = command.WholesalerId,
                Stock = command.Stock
            };
            _wholesalerRepository.SellNewBeer(sellNewBeer);
        }

        public void UpdateStock(SellNewBeerCommand command)
        {
            var updateStock = new WholesalerBeer()
            {
                BeerId = command.BeerId,
                WholesalerId = command.WholesalerId,
                Stock = command.Stock
            };
            _wholesalerRepository.UpdateStock(updateStock);
        }
    }
}
