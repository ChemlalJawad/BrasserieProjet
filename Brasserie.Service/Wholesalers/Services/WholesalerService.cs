using Brasserie.Core.Domains;
using Brasserie.Data.Repositories.Interfaces;
using Brasserie.Service.Wholesalers.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Brasserie.Service.Wholesalers.Services
{
    public class WholesalerService : IWholesalerService
    {
        private readonly IWholesalerRepository _wholesalerRepository;
        private readonly IBeerRepository _beerRepository;
        public WholesalerService(IWholesalerRepository wholesalerRepository,IBeerRepository beerRepository)
        {
            _wholesalerRepository = wholesalerRepository;
            _beerRepository = beerRepository;
        }

        public double GetQuotation(QuotationCommand command)
        {
            if (command.Items == null) throw new Exception("Command can' be null !");
                        
            var wholesaler = _wholesalerRepository.FindById(command.WholesalerId);
            if (wholesaler == null) throw new Exception("Wholesaler does not exist!");

            if(command.Items.GroupBy(e => e.BeerId)
                .Select( x => x.First())
                .ToList()
                .Count()  < command.Items.Count())
            throw new Exception("You can't have duplicates items in your Order");

            var totalPrice = 0.00;

            foreach (var item in command.Items) {
                var beer = _beerRepository.FindById(item.BeerId);
                if (beer == null) throw new Exception("Beer does not exist");
                
                var stock = beer.WholesalerBeers.
                    Where(e => e.WholesalerId == command.WholesalerId 
                    && e.BeerId == item.BeerId)
                    .SingleOrDefault().Stock;

                if(stock >= item.Quantity && stock != 0) 
                {
                    totalPrice += beer.Price * item.Quantity;
                }
                else
                {
                    throw new Exception("You don't have enough stocks!");
                }
          
                if(item.Quantity > 10 && item.Quantity < 21) 
                {
                    var discountPrice10 = (beer.Price * item.Quantity) * 0.10;
                    totalPrice -= discountPrice10;
                }
                if (item.Quantity > 20) 
                {
                    var discountPrice20 = (beer.Price * item.Quantity) * 0.20;
                    totalPrice -= discountPrice20;
                }
            
            }

            return totalPrice;
        }

        public void SellNewBeer(SellBeerCommand command)
        {
            var sellNewBeer = new WholesalerBeer()
            {
                BeerId = command.BeerId,
                WholesalerId = command.WholesalerId,
                Stock = command.Stock
            };
            _wholesalerRepository.SellNewBeer(sellNewBeer);
        }

        public void UpdateStock(UpdateStockCommand command)
        {
            var updateStock = new WholesalerBeer()
            {
                BeerId = command.BeerId,
                WholesalerId = command.WholesalerId
            };
            _wholesalerRepository.UpdateStock(updateStock);
        }

    }
}
