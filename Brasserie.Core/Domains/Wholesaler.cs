using System.Collections.Generic;

namespace Brasserie.Core.Domains
{
    public class Wholesaler
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<WholesalerBeer> WholesalerBeers { get; set; }
    }
}
