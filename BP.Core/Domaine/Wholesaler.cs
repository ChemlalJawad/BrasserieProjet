using System;
using System.Collections.Generic;
using System.Text;

namespace BP.Core.Domaine
{
    public class Wholesaler
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<WholesalerBeer> WholesalerBeers { get; set; }
    }
}
