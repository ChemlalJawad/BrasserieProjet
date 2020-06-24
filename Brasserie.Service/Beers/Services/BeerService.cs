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

        public BeerService(IBeerRepository beerRepository)
        {
            _beerRepository = beerRepository;
        }

        public BeerService(IBrewerRepository brewerRepository)
        {
            _brewerRepository = brewerRepository;
        }

        public BeerService(IBrewerRepository brewerRepository, IBeerRepository beerRepository)
        {
            _brewerRepository = brewerRepository;
            _beerRepository = beerRepository;
        }

        public Beer CreateBeer(CreateBeerCommand command)
        {
            if (command == null)  throw new Exception("Null"); ;
            if (command.Name == null) throw new Exception("Name of beer does not exist");
            if (command.Price <= 0) throw new Exception("Beer can't be free, Add a good amount");
            var brewer = new Brewer();
           
            try
            {
                 brewer = _brewerRepository.FindById(command.BrewerId);
            }
            catch(Exception e) 
            {
                throw new Exception("Brewer does not exist");
            }

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

        public void Delete(int Id)
        {
            _beerRepository.Delete(Id);
        }

        public Beer FindById(int Id)
        {
            return _beerRepository.FindById(Id);
        }

        public IEnumerable<Beer> GetAll()
        {
            var beers = _beerRepository.GetAll();
            if (beers == null) throw new Exception("List of beers is null");
            return beers;
        }
    }
}
