using WebParser.Model.Models;

namespace WebParser.BL.Services.PageParser
{
    public interface IProxyService
    {
        void UpdateProxyList();
        ProxyServer GetProxy();
    }
}
