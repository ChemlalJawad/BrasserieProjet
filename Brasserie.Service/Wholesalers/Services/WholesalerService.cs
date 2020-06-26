using Brasserie.Core.Domains;
using Brasserie.Data;
using Brasserie.Data.Exceptions;
using Brasserie.Service.Wholesalers.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Brasserie.Service.Wholesalers.Services
{
    public class WholesalerService : IWholesalerService
    {
        private readonly BrasserieContext _brasserieContext;

        public WholesalerService(BrasserieContext brasserieContext)
        {
            _brasserieContext = brasserieContext;
        }

        public double GetQuotation(QuotationCommand command)
        {
            if (command.Items == null) throw new HttpBodyException("Command can't be null !");
                        
            var wholesaler = _brasserieContext.Wholesalers
                 .Include(e => e.WholesalerBeers)
                 .ThenInclude(e => e.Beer)
                 .SingleOrDefault(e => e.Id == command.WholesalerId);
            if (wholesaler == null) throw new NotFindObjectException("Wholesaler does not exist!");

            if (command.Items
                    .GroupBy(e => e.BeerId)
                    .ToList()
                    .Count()  < command.Items.Count())
            throw new DuplicateItemException("You can't have duplicates items in your Order");

            var totalPrice = 0.00;

            foreach (var item in command.Items) {
                
                var wb = wholesaler.WholesalerBeers
                        .SingleOrDefault(wb => wb.BeerId == item.BeerId);
                if (wb == null) throw new NotFindObjectException("Beer is not sell");        

                if (wb.Stock >= item.Quantity) 
                {
                    totalPrice += wb.Beer.Price * item.Quantity;
                }
                else
                {
                    throw new NotEnoughQuantityException("You don't have enough stocks!");
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

            return Math.Round(totalPrice,2);
        }

        public WholesalerBeer AddNewBeerToWholesaler(SellBeerCommand command)
        {
            if (command == null) throw new HttpBodyException("Command can't be null");
            if (command.Stock < 0) throw new HttpBodyException("You can't add a negative stock");
           
            var beer = _brasserieContext.Beers
                .FirstOrDefault(e => e.Id == command.BeerId);
            if (beer == null) throw new NotFindObjectException("Beer does not exist");
            
            var wholesaler = _brasserieContext.Wholesalers
                .Include(e => e.WholesalerBeers)
                .FirstOrDefault(w => w.Id == command.WholesalerId);
            if (wholesaler == null) throw new NotFindObjectException("Wholesaler does not exist");

           if (wholesaler.WholesalerBeers.Any(wb =>wb.WholesalerId == command.WholesalerId && wb.BeerId == command.BeerId))
           {
                throw new DuplicateItemException("Wholesaler already sell this beer");                  
           }
           
            var addBeer = new WholesalerBeer()
            {
                BeerId = command.BeerId,
                WholesalerId = command.WholesalerId,
                Stock = command.Stock
            };
            _brasserieContext.WholesalerBeers.Add(addBeer);
            _brasserieContext.SaveChanges();

            return addBeer;
        }

        public WholesalerBeer UpdateWholesalerBeer(UpdateStockCommand command)
        {
            if (command == null) throw new HttpBodyException("Command can't be null");

            var wholesalerBeer = _brasserieContext.WholesalerBeers
                .Find(command.BeerId, command.WholesalerId);
            
            if (command.Stock < 0) throw new HttpBodyException("You can't add a negative stock");

            wholesalerBeer.Stock = command.Stock;
            _brasserieContext.WholesalerBeers.Update(wholesalerBeer);
            
            return wholesalerBeer; 
        }
        public List<WholesalerBeer> GetAll() 
        {
            return _brasserieContext.WholesalerBeers
                 .Include(e => e.Wholesaler)
                 .ThenInclude(e => e.WholesalerBeers)
                 .ThenInclude(e => e.Beer)
                 .ToList(); 
        }   
    }
}
