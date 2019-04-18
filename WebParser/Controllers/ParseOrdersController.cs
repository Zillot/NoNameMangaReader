using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebParser.BL.Services.PageParser;
using WebParser.BL.Services.ParseOrders;
using WebParser.Model.DTOModels;

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
        public async Task ProccessManga(string url)
        {
            await Task.Run(() => _pageParserService.ProccessOrder(new PageParseOrderDTO()
            {
                Url = url,
                PageProvider = "ReadManga",
                OrderCreated = DateTime.Now,
                OrderGUID = Guid.NewGuid().ToString(),
                Priority= Model.Enums.OrderPriority.Hight
            }));
        }
    }
}
