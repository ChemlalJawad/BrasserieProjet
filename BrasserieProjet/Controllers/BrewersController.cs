using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BP.Core.Domaine;
using BP.Data;
using BP.Service.Brewers.Services.Interfaces;

namespace BrasserieProjet.Controllers
{
    [Route("api")]
    [ApiController]
    public class BrewersController : ControllerBase
    {
        private readonly IBrewerService _context;

        public BrewersController(IBrewerService context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("brewers")]
        public ActionResult<IEnumerable<Brewer>> GetBeers()
        {
            var brewers = _context.GetAllBeers();
            return Ok(brewers);
        }

        // GET: api/beer/1
        [HttpGet]
        [Route("brewer/{id}")]
        public ActionResult<Beer> FindBeer([FromRoute] int Id)
        {
            var brewer = _context.FindBrewer(Id);
            return Ok(brewer);
        }

    }
}
