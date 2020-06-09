using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BP.Core.Domaine;
using BP.Data;
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

        // GET: api/beers
        [HttpGet]
        [Route("beers")]
        public ActionResult<IEnumerable<Beer>> GetBeers()
        {
            var beers = _beerService.GetAll();
            return Ok(beers);
        }
        
        // GET: api/beer/1
        [HttpGet]
        [Route("beer/{id}")]
        public ActionResult<Beer> FindBeer([FromRoute] int Id)
        {
            var beer = _beerService.FindById(Id);
            return Ok(beer);
        }

        // GET: api/beer/5
        [HttpPost]
        [Route("beer")]
        public ActionResult<Beer> CreateBeer(CreateBeerCommand command)
        {
            var beer = _beerService.CreateBeer(command);

            return Ok(beer);
        }

        [HttpDelete]
        [Route("beer/{id}")]
        public ActionResult<Beer> Delete([FromRoute] int Id)
        {
            _beerService.Delete(Id);
            return Ok();


        }
    }
}
