using CommonLib.Redis;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using WebParser.Model.Models;

namespace WebParser.DL.Repositories
{
    public class ProxyRepository : RedisBaseRepository, IProxyRepository
    {
        protected override string _repositoryKey { get { return "proxyRepository"; } }

        public ProxyRepository(IConfiguration configuration) : base(configuration) { }

        public DateTime? GetLastProxyUpdate()
        {
            return get<DateTime?>($"{_repositoryKey}:LastProxyUpdate");
        }

        public void ProxyUpdateTrigger()
        {
            save($"{_repositoryKey}:LastProxyUpdate", DateTime.Now, 600);
        }

        public void Save(List<ProxyServer> proxyServers)
        {
            save($"{_repositoryKey}:proxy", proxyServers, 600000);
        }

        public List<ProxyServer> Get()
        {
            return get<List<ProxyServer>>($"{_repositoryKey}:proxy");
        }
    }
}
