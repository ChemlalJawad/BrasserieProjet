
namespace BP.Core.Domains
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
