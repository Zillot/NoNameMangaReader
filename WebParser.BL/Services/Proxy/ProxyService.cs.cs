using WebParser.BL.Providers;
using WebParser.DL.Repositories;
using WebParser.Model.Models;
using System.Linq;
using System;

namespace WebParser.BL.Services.PageParser
{
    public class ProxyService : IProxyService
    {
        private IProxyRepository _proxyRepository { get; set; }

        public ProxyService(IProxyRepository proxyRepository)
        {
            _proxyRepository = proxyRepository;
        }

        public void UpdateProxyList()
        {
            if (_proxyRepository.GetLastProxyUpdate() != null)
            {
                var provider = new FreeProxyProvider(this);
                var newProxy = provider.ProccessUrl("https://free-proxy-list.net").Result;

                _proxyRepository.Save(newProxy);
                _proxyRepository.ProxyUpdateTrigger();
            }
        }

        public ProxyServer GetProxy()
        {
            var proxys = _proxyRepository.Get().OrderBy(x => x.LastUsed);
            var nextProxy = proxys.LastOrDefault();

            if (nextProxy != null)
            {
                nextProxy.LastUsed = DateTime.Now;
            }

            return nextProxy;
        }
    }
}
