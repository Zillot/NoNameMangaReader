using CommonLib.Models.DTOModels;
using CommonLib.Services;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using WebParser.BL.Providers;
using WebParser.BL.Services.ParseOrders;
using WebParser.Model.DTOModels;
using WebParser.Model.Models;

namespace WebParser.BL.Services.PageParser
{
    public class PageParserService: IPageParserService
    {
        private IParseOrdersService _parseOrdersService { get; set; }
        private IProxyService _proxyService { get; set; }
        private IDummyNetworkService _dummyNetworkService { get; set; }

        public PageParserService(
            IParseOrdersService parseOrdersService,
            IProxyService proxyService,
            IDummyNetworkService dummyNetworkService)
        {
            _parseOrdersService = parseOrdersService;
            _proxyService = proxyService;
            _dummyNetworkService = dummyNetworkService;

            //TODO move it to some other place and use balancer
            _dummyNetworkService.SetBaseUri("http://localhost:51004/");
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
                        var result = Task.Run(() => provider.ProccessUrl(nextOrder.Url)).Result;
                        SaveManga(result);

                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void SaveManga(MangaDTO result)
        {
            _dummyNetworkService.Post("Manga/SaveManga", null, JsonConvert.SerializeObject(result));
        }
    }
}
