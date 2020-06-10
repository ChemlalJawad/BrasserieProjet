using BP.Core.Domaine;
using System;
using System.Collections.Generic;
using System.Text;

namespace BP.Service.Wholesalers.Services
{
    public class GetQuotationCommand
    {
        public int BeerId { get; set; }
        public int Quantity { get; set; }

    }
}
