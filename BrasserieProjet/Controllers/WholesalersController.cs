using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BP.Core.Domaine;
using BP.Data;
using BP.Service.Wholesalers.Services.Interfaces;
using BP.Service.Wholesalers;
using BP.Service.Wholesalers.Services;

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
        [Route("wholesaler/add")]
        public ActionResult<WholesalerBeer> SellNewBeer(SellNewBeerCommand command)
        {
            _wholesalerService.SellNewBeer(command);

           return Ok();
        }
        [HttpPost]
        [Route("wholesaler/updateStock/{stock}")]
        public ActionResult<WholesalerBeer> UpdateStock(SellNewBeerCommand command, [FromRoute] int stock)
        {
            command.Stock = stock;
            _wholesalerService.UpdateStock(command);

            return Ok();
        }
        //TODO A finir 
        [HttpPost]
        [Route("wholesaler/{wholesalerId}/getquotation")]
        public ActionResult<Quotation> GetQuotation([FromBody] GetQuotationCommand command, [FromRoute] int id)
        {
            
            var quotation = _wholesalerService.GetQuotation(id,command);

            return Ok(quotation);
        }

    }
}
