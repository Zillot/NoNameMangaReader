using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using WebParser.DL.Repositories.Base;
using WebParser.Model.Models;

namespace WebParser.DL.Repositories
{
    public class ProxyRepository : RedisBaseRepository, IProxyRepository
    {
        protected override string repositoryKey { get { return "proxyRepository"; } }

        public ProxyRepository(IConfiguration configuration) : base(configuration) { }

        public DateTime? GetLastProxyUpdate()
        {
            return get<DateTime?>($"{repositoryKey}:LastProxyUpdate");
        }

        public void ProxyUpdateTrigger()
        {
            save($"{repositoryKey}:LastProxyUpdate", DateTime.Now, 600);
        }

        public void Save(List<ProxyServer> proxyServers)
        {
            save($"{repositoryKey}:proxy", proxyServers, 600000);
        }

        public List<ProxyServer> Get()
        {
            return get<List<ProxyServer>>($"{repositoryKey}:proxy");
        }
    }
}
