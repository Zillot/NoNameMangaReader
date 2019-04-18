using WebParser.BL.Providers;
using WebParser.BL.Services.ParseOrders;
using WebParser.Model.DTOModels;

namespace WebParser.BL.Services.PageParser
{
    public class PageParserService: IPageParserService
    {
        private IParseOrdersService _parseOrdersService { get; set; }
        private IProxyService _proxyService { get; set; }

        public PageParserService(
            IParseOrdersService parseOrdersService,
            IProxyService proxyService)
        {
            _parseOrdersService = parseOrdersService;
            _proxyService = proxyService;
        }

        public void ParserWorker()
        {
            var nexOrder = _parseOrdersService.PopNextOrderFromQueue();

            ProccessOrder(nexOrder);
        }

        public void ProccessOrder(PageParseOrderDTO nextOrder)
        {
            _proxyService.UpdateProxyList();

            switch (nextOrder.PageProvider)
            {
                case "ReadManga":
                    {
                        var provider = new MangaReaderProvider(_proxyService);
                        var result = provider.ProccessUrl(nextOrder.Url);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }
}
