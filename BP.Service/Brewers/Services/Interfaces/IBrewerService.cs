using BP.Core.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BP.Service.Brewers.Services.Interfaces
{
    public interface IBrewerService
    {
        IEnumerable<Brewer> GetAllBeers();
        Brewer FindBrewerById(int Id);
    }
}
