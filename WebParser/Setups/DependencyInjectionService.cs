using Microsoft.Extensions.DependencyInjection;
using WebParser.BL.Services.PageParser;
using WebParser.BL.Services.ParseOrders;

namespace RESTAPI.Setups
{
    public class DependencyInjectionService
    {
        public void SetupServices(IServiceCollection services)
        {
            services.AddTransient<IPageParserService, PageParserService>(); 
            services.AddTransient<IParseOrdersService, ParseOrdersService>();
        }
    }
}
