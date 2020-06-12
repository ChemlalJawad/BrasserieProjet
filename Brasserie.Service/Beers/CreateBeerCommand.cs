namespace Brasserie.Service.Beers
{
    public class CreateBeerCommand
    {
        public string Name { get; set; }
        public double AlcoholPercentage { get; set; }
        public double Price { get; set; }
        public int BrewerId { get; set;}
    }
}
