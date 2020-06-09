using System;
using System.Collections.Generic;
using System.Text;

namespace BP.Core.Domaine
{
    public class WholesalerBeer
    {
        
        public int BeerId { get; set; }
        public Beer Beer { get; set; }

        public int WholesalerId { get; set; }
        public Wholesaler Wholesaler { get; set; }
      
        public int Stock { get; set; }
    }
}
