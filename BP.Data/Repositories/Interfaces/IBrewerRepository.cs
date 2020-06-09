using BP.Core.Domaine;
using System;
using System.Collections.Generic;
using System.Text;

namespace BP.Data.Repositories.Interfaces
{
    public interface IBrewerRepository
    {
        IEnumerable<Brewer> GetAllBeers();
     
        Brewer FindById(int Id);
    }
}
