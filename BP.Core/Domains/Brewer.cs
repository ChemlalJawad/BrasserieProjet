using System.Collections.Generic;

namespace BP.Core.Domains
{
    public class Brewer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Beer> Beers { get; set; }
    }
}
