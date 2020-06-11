using BP.Core.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BP.Service
{
    public class CreateBeerCommand
    {
        public string Name { get; set; }
        public double AlcoholPercentage { get; set; }
        public double Price { get; set; }
        public int BrewerId { get; set;}
    }
}
