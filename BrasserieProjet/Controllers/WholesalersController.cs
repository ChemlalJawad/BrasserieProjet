using Microsoft.AspNetCore.Mvc;
using BP.Core.Domains;
using BP.Service.Wholesalers.Services.Interfaces;
using BP.Service.Wholesalers;

namespace BrasserieProjet.Controllers
{
    [Route("api")]
    [ApiController]
    public class WholesalersController : ControllerBase
    {
        private readonly IWholesalerService _wholesalerService;

        public WholesalersController(IWholesalerService wholesalerService )
        {
            _wholesalerService = wholesalerService;
        }

        [HttpPost]
        [Route("wholesaler")]
        public ActionResult<WholesalerBeer> SellNewBeer(SellBeerOrUpdateStockCommand command)
        {
            _wholesalerService.SellNewBeer(command);

           return Ok();
        }

        [HttpPost]
        [Route("wholesaler/{stock}")]
        public ActionResult<WholesalerBeer> UpdateStock(SellBeerOrUpdateStockCommand command, [FromRoute] int stock)
        {
            command.Stock = stock;
            _wholesalerService.UpdateStock(command);

            return Ok();
        }

    }
}
