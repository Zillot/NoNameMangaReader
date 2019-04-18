using System.Collections.Generic;
using System.Threading.Tasks;
using WebParser.Model.Models;
using System.Linq;

namespace WebParser.BL.Providers
{
    public class FreeProxyProvider : BasePageProvider<List<ProxyServer>>
    {
        private static readonly string xLines = "//*[@id='proxylisttable']/tbody/tr";

        public override async Task<List<ProxyServer>> ProccessUrl(string pageUrl)
        {
            getDoc("ProxyPage").LoadHtml(await GetHtml(pageUrl));

            var proxyNodes = ListXPath("ProxyPage", xLines);
            var proxys = new List<ProxyServer>();

            foreach (var proxyNode in proxyNodes)
            {
                var nodes = proxyNode.ChildNodes.Where(x => x.Name == "td").ToArray();

                proxys.Add(new ProxyServer()
                {
                    Ip = nodes[0].InnerText,
                    Port = nodes[1].InnerText,
                    Https = nodes[6].InnerText != "no"
                });
            }

            return proxys;
        }
    }
}
