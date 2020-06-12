using System.Collections.Generic;
using Brasserie.Core.Domains;
using Brasserie.Data.Repositories.Interfaces;
using Brasserie.Service.Brewers.Services.Interfaces;

namespace Brasserie.Service.Brewers.Services
{
    public class BrewerService : IBrewerService
    {
        private readonly IBrewerRepository _brewerRepository;

        public BrewerService(IBrewerRepository brewerRepository)
        {
            _brewerRepository = brewerRepository;
        }

        public Brewer FindBrewerById(int Id)
        {
            var brewer = _brewerRepository.FindBrewerById(Id);
            return brewer;
        }

        public IEnumerable<Brewer> GetAllBeers()
        {
            var brewers = _brewerRepository.GetAllBeers();
            return brewers;
        }
    }
}
