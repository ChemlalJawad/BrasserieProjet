using BP.Core.Domaine;
using System;
using System.Collections.Generic;
using System.Text;

namespace BP.Data.Repositories.Interfaces
{
    public interface IBeerRepository
    {
        //Pour moi check si j'ai tout
        IEnumerable<Beer> GetAll();
        void CreateBeer(Beer beer);
        Beer FindById(int beerId);
        void Delete(int beerId);
    }
}
