using System.Collections.Generic;
using Brasserie.Core.Domains;

namespace Brasserie.Data.Repositories.Interfaces
{
    public interface IBeerRepository
    {
        //Pour moi check si j'ai tout
        IEnumerable<Beer> GetAll();
        void CreateBeer(Beer beer);
        Beer FindBeerById(int beerId);
        void Delete(int beerId);
    }
}
