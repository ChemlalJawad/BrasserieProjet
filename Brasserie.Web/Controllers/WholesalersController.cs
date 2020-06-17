using Brasserie.Core.Domains;
using Brasserie.Service.Wholesalers;
using Brasserie.Service.Wholesalers.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;



namespace Brasserie.Web.Controllers
{
    [Route("api")]
    [ApiController]
    public class WholesalersController : ControllerBase
    {
        private readonly IWholesalerService _wholesalerService;
       

        public WholesalersController(IWholesalerService wholesalerService)
        {
            _wholesalerService = wholesalerService;
           
        }

        [HttpPost]
        [Route("wholesalers")]
        public ActionResult<WholesalerBeer> SellNewBeer(SellBeerCommand command)
        {
            _wholesalerService.SellNewBeer(command);

           return Ok();
        }

        [HttpPost]
        [Route("wholesalers/{stock}")]
        public ActionResult<WholesalerBeer> UpdateStock(UpdateStockCommand command, [FromRoute] int stock)
        {
            command.Stock = stock;
            _wholesalerService.UpdateStock(command);

            return Ok();
        }

        [HttpPost]
        [Route("{wholesalerId}/quote")]
        public ActionResult<QuotationCommand> GetQuotations(QuotationCommand command)
        {
           
            var price = _wholesalerService.GetQuotation(command);
            command.TotalPrice = price;
            

            return Ok(command);
        }

    }
}
