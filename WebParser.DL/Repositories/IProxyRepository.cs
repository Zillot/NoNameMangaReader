using System;
using System.Collections.Generic;
using WebParser.DL.Repositories.Base;
using WebParser.Model.Models;

namespace WebParser.DL.Repositories
{
    public interface IProxyRepository : IRedisBaseRepository
    {
        DateTime? GetLastProxyUpdate();
        void ProxyUpdateTrigger();
        void Save(List<ProxyServer> proxyServers);
        List<ProxyServer> Get();
    }
}
