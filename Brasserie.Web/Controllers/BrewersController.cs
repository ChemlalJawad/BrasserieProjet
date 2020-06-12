using System.Collections.Generic;
using Brasserie.Core.Domains;
using Brasserie.Service.Brewers.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Brasserie.Web.Controllers
{
    [Route("api")]
    [ApiController]
    public class BrewersController : ControllerBase
    {
        private readonly IBrewerService _brewerService;

        public BrewersController(IBrewerService brewerService)
        {
            _brewerService = brewerService;
        }

        [HttpGet]
        [Route("brewers")]
        public ActionResult<IEnumerable<Brewer>> GetBeers()
        {
            var brewers = _brewerService.GetAllBeers();
          
            return Ok(brewers);
        }
                
        [HttpGet]
        [Route("brewers/{id}")]
        public ActionResult<Beer> FindBeer([FromRoute] int Id)
        {
            var brewer = _brewerService.FindBrewerById(Id);
           
            return Ok(brewer);
        }

    }
}
