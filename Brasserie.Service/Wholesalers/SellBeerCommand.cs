namespace Brasserie.Service.Wholesalers
{
    public class SellBeerCommand
    {
        public int BeerId { get; set; }
        public int WholesalerId { get; set; }
        public int Stock { get; set; }
    }
}
