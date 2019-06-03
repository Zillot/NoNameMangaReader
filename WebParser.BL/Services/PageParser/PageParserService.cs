using CommonLib.Services;
using System.Threading.Tasks;
using WebParser.BL.Providers;
using WebParser.BL.Services.ParseOrders;
using WebParser.Model.DTOModels;

namespace WebParser.BL.Services.PageParser
{
    public class PageParserService: IPageParserService
    {
        private IParseOrdersService _parseOrdersService { get; set; }
        private IProxyService _proxyService { get; set; }
        private IDummyNetworkService _dummyNetworkService { get; set; }

        private static bool _workerIsWorking { get; set; } = false;
        private static bool _skipWorker { get; set; } = false;
        private static bool _workerTask { get; set; } = false;

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

        public void AwakeWorker()
        {
            _skipWorker = false;
        }

        public void InitializeWorker()
        {
            if (_workerIsWorking)
            {
                return;
            }

            _workerIsWorking = true;
            Task.Run(() => ParserWorker());
        }

        public void ParserWorker()
        {
            var nexOrder = _parseOrdersService.PopNextOrderFromQueue();

            if (nexOrder != null)
            {
                ProccessOrder(nexOrder);
            }
            else
            {
                //Thread.CurrentThread.Suspend);
            }
        }

        public void ProccessOrder(PageParseOrderDTO nextOrder)
        {
            _proxyService.UpdateProxyList();

            switch (nextOrder.PageProvider)
            {
                case "ReadManga":
                    {
                        var provider = new MangaReaderProvider(_proxyService, _dummyNetworkService);
                        Task.Run(() => provider.ProccessUrl(nextOrder.Url)).Wait();

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
