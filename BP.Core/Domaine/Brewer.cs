using System;
using System.Collections.Generic;
using System.Text;

namespace BP.Core.Domaine
{
    public class Brewer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Beer> Beers { get; set; }
    }
}
