using System.Collections.Generic;
using Brasserie.Core.Domains;

namespace Brasserie.Service.Brewers.Services.Interfaces
{
    public interface IBrewerService
    {
        IEnumerable<Brewer> GetAllBeers();
        Brewer FindBrewerById(int Id);
    }
}
