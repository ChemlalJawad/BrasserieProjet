using System;
using System.Collections.Generic;
using System.Text;

namespace BP.Core.Domaine
{
    public class Beer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double AlcoolPercentage { get; set; }
        public double Price { get; set; }
        public Brewer Brewer { get; set; }

        public ICollection<WholesalerBeer> WholesalerBeers { get; set; }
    }
}
