using System;
using System.Collections.Generic;
using Brasserie.Core.Domains;
using Brasserie.Data.Repositories.Interfaces;
using Brasserie.Service.Beers.Services.Interfaces;

namespace Brasserie.Service.Beers.Services
{
    public class BeerService : IBeerService
    {
        private readonly IBeerRepository _beerRepository;
        private readonly IBrewerRepository _brewerRepository;

        public BeerService(IBrewerRepository brewerRepository, IBeerRepository beerRepository)
        {
            _brewerRepository = brewerRepository;
            _beerRepository = beerRepository;
        }

        public Beer CreateBeer(CreateBeerCommand command)
        {
            if (command == null)  throw new Exception("Command can't be null"); ;
            if (command.Name == null) throw new Exception("Name of beer does not exist");
            if (command.Price <= 0) throw new Exception("Beer can't be free, Add a good amount");
           
            var brewer = _brewerRepository.FindById(command.BrewerId);
            if (brewer == null) throw new Exception("Brewer does not exist");
            
            var beer = new Beer
            {
                Name = command.Name,
                AlcoholPercentage = command.AlcoholPercentage,
                Price = command.Price,
                Brewer = brewer
            };
           
            _beerRepository.Create(beer);
            return beer;
        }

        public void Delete(int id)
        {
            _beerRepository.Delete(id);
        }

        public Beer FindById(int id)
        {
            return _beerRepository.FindById(id);
        }

        public IEnumerable<Beer> GetAll()
        {
            var beers = _beerRepository.GetAll();
            return beers;
        }
    }
}
