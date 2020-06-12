using System.Collections.Generic;

namespace Brasserie.Core.Domains
{
    public class Brewer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Beer> Beers { get; set; }
    }
}
