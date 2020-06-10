using System;
using System.Collections.Generic;
using System.Text;

namespace BP.Core.Domaine
{
    public class Quotation
    {
        public double Total { get; set; }
        public double Discount { get; set; }
        public ICollection<CommandLine> Items { get; set; }
    }
}
