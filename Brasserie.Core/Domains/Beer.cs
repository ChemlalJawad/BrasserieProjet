using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Brasserie.Core.Domains
{
    public class Beer
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double AlcoholPercentage { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public Brewer Brewer { get; set; }
        public ICollection<WholesalerBeer> WholesalerBeers { get; set; }
    }
}
