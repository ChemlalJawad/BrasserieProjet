using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BP.Core.Domains;
using BP.Service.Beers.Services.Interfaces;
using BP.Service;

namespace BrasserieProjet.Controllers
{
    [Route("api")]
    [ApiController]
    public class BeersController : ControllerBase
    {
        private readonly IBeerService _beerService;

        public BeersController(IBeerService beerService)
        {
            _beerService = beerService;
        }

        [HttpGet]
        [Route("beers")]
        public ActionResult<IEnumerable<Beer>> GetBeers()
        {
            var beers = _beerService.GetAll();
            
            return Ok(beers);
        }
        
        [HttpGet]
        [Route("beers/{id}")]
        public ActionResult<Beer> FindBeerById([FromRoute] int Id)
        {
            var beer = _beerService.FindById(Id);
           
            return Ok(beer);
        }

        [HttpPost]
        [Route("beers")]
        public ActionResult<Beer> CreateBeer(CreateBeerCommand command)
        {
            var beer = _beerService.CreateBeer(command);

            return Ok(beer);
        }

        [HttpDelete]
        [Route("beers/{id}")]
        public ActionResult<Beer> Delete([FromRoute] int Id)
        {
            _beerService.Delete(Id);
           
            return Ok();
        }
    }
}
