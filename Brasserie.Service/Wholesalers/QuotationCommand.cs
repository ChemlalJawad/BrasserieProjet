using Brasserie.Core.Domains;
using System.Collections;
using System.Collections.Generic;

namespace Brasserie.Service.Wholesalers.Services.Interfaces
{
    public class QuotationCommand
    {
        public int WholesalerId { get; set; }
        public double TotalPrice {get;set;}
        public IEnumerable<ItemCommand> Items { get; set; }
    }
}
