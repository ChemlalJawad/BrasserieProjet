using System.Collections.Generic;
using Brasserie.Core.Domains;

namespace Brasserie.Data.Repositories.Interfaces
{
    public interface IBrewerRepository
    {
        IEnumerable<Brewer> GetAllBeers();
     
        Brewer FindBrewerById(int Id);
    }
}
