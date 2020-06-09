using BP.Core.Domaine;
using BP.Data.Repositories.Interfaces;
using BP.Service.Beers.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BP.Service.Beers.Services
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
            var brewer = _brewerRepository.FindById(command.BrewerId);
            if (brewer == null) return null;

            var beer = new Beer
            {
                Name = command.Name,
                AlcoolPercentage = command.AlcoolPercentage,
                Price = command.Price,
                Brewer = brewer
            };
            _beerRepository.CreateBeer(beer);

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
            return _beerRepository.GetAll();
        }
    }
}
