using BP.Core.Domaine;
using System;
using System.Collections.Generic;
using System.Text;

namespace BP.Service.Brewers.Services.Interfaces
{
    public interface IBrewerService
    {
        IEnumerable<Brewer> GetAllBeers();
        //Brewer CreateBrewer(CreateBrewerCommand command);
        Brewer FindBrewer(int Id);
    }
}
