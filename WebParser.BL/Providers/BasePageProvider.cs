using CommonLib.Services;
using Flurl.Http;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebParser.BL.Services.PageParser;

namespace WebParser.BL.Providers
{
    public abstract class BasePageProvider<T> : IPageProvider<T>
    {
        public abstract Task<T> ProccessUrl(string pageUrl);
        private Dictionary<string, HtmlDocument> _docs { get; set; }

        protected IProxyService _proxyService { get; set; }

        public BasePageProvider(IProxyService proxyService)
        {
            _proxyService = proxyService;
        }

        public void Dispouse()
        {
            _docs.Clear();
        }

        protected HtmlDocument getDoc(string key)
        {
            if (_docs.ContainsKey(key))
            {
                return _docs[key];
            }
            else
            {
                var newDoc = new HtmlDocument();
                _docs.Add(key, newDoc);
                return newDoc;
            }
        }

        public async Task<string> GetHtml(string url)
        {
            var proxy = _proxyService.GetProxy();
            FlurlHttp.Configure(settings => { settings.HttpClientFactory = new ProxyHttpClientFactory(proxy.Url); });
            return await new FlurlRequest(new Flurl.Url(url)).GetStringAsync();
        }

        public HtmlNode GetXPath(string docKey, string xPath)
        {
            return getDoc(docKey).DocumentNode.SelectSingleNode(xPath);
        }

        public HtmlNodeCollection ListXPath(string docKey, string xPath)
        {
            return getDoc(docKey).DocumentNode.SelectNodes(xPath);
        }
    }
}
