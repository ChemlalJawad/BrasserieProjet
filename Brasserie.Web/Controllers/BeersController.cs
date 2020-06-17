using System.Collections.Generic;
using Brasserie.Core.Domains;
using Brasserie.Service.Beers;
using Brasserie.Service.Beers.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Brasserie.Web.Controllers
{
    [Route("api/beers")]
    [ApiController]
    public class BeersController : ControllerBase
    {
        private readonly IBeerService _beerService;

        public BeersController(IBeerService beerService)
        {
            _beerService = beerService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Beer>> GetBeers()
        {
            var beers = _beerService.GetAll();
            
            return Ok(beers);
        }
        
        [HttpGet]
        [Route("{id}")]
        public ActionResult<Beer> FindBeerById([FromRoute] int Id)
        {
            var beer = _beerService.FindById(Id);
           
            return Ok(beer);
        }

        [HttpPost]
        public ActionResult<Beer> CreateBeer(CreateBeerCommand command)
        {
            var beer = _beerService.CreateBeer(command);

            return Ok(beer);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult<Beer> Delete([FromRoute] int Id)
        {
            _beerService.Delete(Id);
           
            return Ok();
        }
    }
}
