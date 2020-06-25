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
            if (command.Items == null) throw new Exception("Command can't be null !");
                        
            var wholesaler = _wholesalerRepository.FindById(command.WholesalerId);
            if (wholesaler == null) throw new Exception("Wholesaler does not exist!");

            if (command.Items
                    .GroupBy(e => e.BeerId)
                    .ToList()
                    .Count()  < command.Items.Count())
            throw new Exception("You can't have duplicates items in your Order");

            var totalPrice = 0.00;

            foreach (var item in command.Items) {
                var beer = _beerRepository.FindById(item.BeerId);
                if (beer == null) throw new Exception("Beer does not exist");
                
                var stock = beer.WholesalerBeers
                        .Where(wb => wb.WholesalerId == command.WholesalerId && wb.BeerId == item.BeerId)
                        .SingleOrDefault()
                        .Stock;

                if (stock >= item.Quantity) 
                {
                    totalPrice += beer.Price * item.Quantity;
                }
                else
                {
                    throw new Exception("You don't have enough stocks!");
                }      
            }

            var stockTotal = 0;
            foreach(var item in command.Items)
            {
                stockTotal += item.Quantity;
            }
            if (stockTotal > 10 && stockTotal < 21)
            {
                var discountPrice10 = totalPrice * 0.10;
                totalPrice -= discountPrice10;
            }
            if (stockTotal > 20)
            {
                var discountPrice20 = totalPrice * 0.20;
                totalPrice -= discountPrice20;
            }

            return totalPrice;
        }

        public void AddNewBeerToWholesaler(SellBeerCommand command)
        {
            if (command == null) throw new Exception("Command can't be null");
            if (command.Stock < 0) throw new Exception("You can't add a negative stock");
           
            var beer = _beerRepository.FindById(command.BeerId);
            if (beer == null) throw new Exception("Beer does not exist");
            
            var wholesaler = _wholesalerRepository.FindById(command.WholesalerId);
            if (wholesaler == null) throw new Exception("Wholesaler does not exist");

            foreach (var item in wholesaler.WholesalerBeers.ToList())
            {
                if (item.WholesalerId == command.WholesalerId && item.BeerId == command.BeerId) 
                {
                     throw new Exception("Wholesaler already sell this beer");                  
                }
            }
            var addBeer = new WholesalerBeer()
            {
                BeerId = command.BeerId,
                WholesalerId = command.WholesalerId,
                Stock = command.Stock
            };
            _wholesalerRepository.Add(addBeer);          
        }

        public WholesalerBeer UpdateWholesalerBeer(UpdateStockCommand command)
        {
            if (command == null) throw new Exception("Command can't be null");

            var beer = _beerRepository.FindById(command.BeerId);
            if (beer == null) throw new Exception("Beer does not exist");

            var wholesaler = _wholesalerRepository.FindById(command.WholesalerId);
            if (wholesaler == null) throw new Exception("Wholesaler does not exist");

            if (command.Stock < 0) throw new Exception("You can't add a negative stock");
            var updateStock = new WholesalerBeer()
            {
                BeerId = command.BeerId,
                WholesalerId = command.WholesalerId,
                Stock = command.Stock
            };
            _wholesalerRepository.Update(updateStock);
            return updateStock; 
        }
        public List<WholesalerBeer> GetAll() 
        {
            var wholesalerbeers = _wholesalerRepository.GetAll();
            return wholesalerbeers;
        }   
    }
}
