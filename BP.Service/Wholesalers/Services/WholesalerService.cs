using BP.Core.Domaine;
using BP.Data.Repositories.Interfaces;
using BP.Service.Wholesalers.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BP.Service.Wholesalers.Services
{
    public class WholesalerService : IWholesalerService
    {
        private readonly IWholesalerRepository _wholesalerRepository;
        private readonly IBeerRepository _beerRepository;

        public WholesalerService(IWholesalerRepository wholesalerRepository, IBeerRepository beerRepository)
        {
            _wholesalerRepository = wholesalerRepository;
            _beerRepository = beerRepository;
        }

        //TODO à finir!
        public Quotation GetQuotation(int wholesalerId,GetQuotationCommand command)
        { 
            var beer = _beerRepository.FindById(command.BeerId);
            var wholesaler = _wholesalerRepository.Find(wholesalerId);
            var stock = wholesaler.WholesalerBeers.Where(e => e.BeerId == beer.Id).FirstOrDefault().Stock;
            
            if (stock < command.Quantity) throw new Exception("Le nombre de bières commandé ne doit pas être supérieur au stock du grossiste");

            if (beer == null) throw new Exception("La commande ne peut pas être vide");
            if (wholesaler.WholesalerBeers.Where(e => e.BeerId == beer.Id).Count() < 1) {
                throw new Exception("La bière doit être vendue par le grossiste !");
            }
            var quotation = new Quotation();
            var totalPrice = beer.Price * command.Quantity;

            if(command.Quantity > 10 && command.Quantity < 21)
            {
                totalPrice -= totalPrice * 0.1;
                quotation.Discount = 0.1;
            }
            if (command.Quantity > 20) { totalPrice -= totalPrice * 0.2;
                quotation.Discount = 0.2;
            }
           
            quotation.Total = totalPrice;
            quotation.Items.Add(new CommandLine() 
            { Beer=beer,
              Quantity =command.Quantity 
            });

            return quotation;
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
                WholesalerId = command.WholesalerId
            };
            _wholesalerRepository.UpdateStock(updateStock);
        }
    }
}
