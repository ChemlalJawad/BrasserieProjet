using BP.Core.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BP.Service.Beers.Services.Interfaces
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
