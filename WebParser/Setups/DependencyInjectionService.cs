using Microsoft.Extensions.DependencyInjection;
using WebParser.BL.Services.PageParser;
using WebParser.BL.Services.ParseOrders;
using WebParser.DL.Repositories;

namespace RESTAPI.Setups
{
    public class DependencyInjectionService
    {
        public void SetupServices(IServiceCollection services)
        {
            services.AddTransient<IPageParserService, PageParserService>(); 
            services.AddTransient<IParseOrdersService, ParseOrdersService>();
            services.AddTransient<IProxyService, ProxyService>();

            services.AddTransient<IProxyRepository, ProxyRepository>();
        }
    }
}
