using System.Collections.Generic;

namespace Brasserie.Core.Domains
{
    public class Beer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double AlcoholPercentage { get; set; }
        public double Price { get; set; }
        public Brewer Brewer { get; set; }
        public ICollection<WholesalerBeer> WholesalerBeers { get; set; }
    }
}
