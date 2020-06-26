using Brasserie.Core.Domains;
using Brasserie.Data;
using Brasserie.Data.Exceptions;
using Brasserie.Service.Beers.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Brasserie.Service.Beers.Services
{
    public class BeerService : IBeerService
    {
        private readonly BrasserieContext _brasserieContext;

        public BeerService(BrasserieContext brasserieContext)
        {
            _brasserieContext = brasserieContext;
        }

        public Beer CreateBeer(CreateBeerCommand command)
        {
            if (command == null)  throw new HttpBodyException("Command can't be null"); 
            if (command.Name == null) throw new HttpBodyException("Name of beer does not exist");
            if (command.Price <= 0) throw new HttpBodyException("Beer can't be free, Add a good amount");
           
            var brewer = _brasserieContext.Brewers.SingleOrDefault(b => b.Id == command.BrewerId);
            if (brewer == null) throw new NotFindObjectException("Brewer does not exist");
            
            var beer = new Beer
            {
                Name = command.Name,
                AlcoholPercentage = command.AlcoholPercentage,
                Price = command.Price,
                Brewer = brewer
            };

            _brasserieContext.Beers.Add(beer);
            _brasserieContext.SaveChanges();

            return beer;
        }

        public void Delete(int id)
        {
            var beer = FindById(id);
            if (beer == null) throw new NotFindObjectException("Beer does not exist");

            _brasserieContext.Beers.Remove(new Beer() { Id = id });
            _brasserieContext.SaveChanges();
        }

        public Beer FindById(int id)
        {
           return _brasserieContext.Beers
                .Include(b => b.Brewer)
                .FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Beer> GetAll()
        {
            var beers = _brasserieContext
                 .Beers
                 .Include(e => e.Brewer)
                 .ThenInclude(e => e.Beers)
                 .ThenInclude(e => e.WholesalerBeers)
                 .ToList();

            return beers;
        }
    }
}
