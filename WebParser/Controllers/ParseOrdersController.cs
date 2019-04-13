using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebParser.BL.Services.PageParser;
using WebParser.BL.Services.ParseOrders;
using WebParser.Model.DTOModels;
using WebParser.Model.Enums;

namespace WebParser.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class ParseOrdersController : ControllerBase
    {
        private IParseOrdersService _parseOrdersService { get; set; }
        private IPageParserService _pageParserService { get; set; }

        public ParseOrdersController(
            IParseOrdersService parseOrdersService,
            IPageParserService pageParserService)
        {
            _parseOrdersService = parseOrdersService;
            _pageParserService = pageParserService;
        }

        [HttpGet]
        public async Task GetData()
        {
            await _pageParserService.ProccessOrder(new PageParseOrderDTO()
            {
                Url = "http://readmanga.me/ohotniki_za_energiei"
            });
        }
    }
}
