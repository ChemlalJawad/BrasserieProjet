namespace Brasserie.Service.Wholesalers
{
    public class UpdateStockCommand
    {
        public int BeerId { get; set; }
        public int WholesalerId { get; set; }
        public int Stock { get; set; }
    }
}
