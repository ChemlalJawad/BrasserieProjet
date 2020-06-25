using System.Collections.Generic;
using Brasserie.Core.Domains;

namespace Brasserie.Service.Beers.Services.Interfaces
{
    public interface IBeerService
    {
        IEnumerable<Beer> GetAll();
        Beer CreateBeer(CreateBeerCommand beer);
        Beer FindById(int id);
        void Delete(int id);
    }
}
