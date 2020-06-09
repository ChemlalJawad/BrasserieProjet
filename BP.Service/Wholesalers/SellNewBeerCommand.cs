using System;
using System.Collections.Generic;
using System.Text;

namespace BP.Service.Wholesalers
{
    public class SellNewBeerCommand
    {
        public int BeerId { get; set; }
        public int WholesalerId { get; set; }
        public int Stock { get; set; }
    }
}
