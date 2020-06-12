using System.Collections.Generic;
using Brasserie.Core.Domains;

namespace Brasserie.Service.Beers.Services.Interfaces
{
    public interface IBeerService
    {
        //Pour moi check si j'ai tout
        IEnumerable<Beer> GetAll();
        Beer CreateBeer(CreateBeerCommand beer);
        Beer FindById(int Id);
        void Delete(int Id);
    }
}
