using Microsoft.Extensions.DependencyInjection;
using Common.BL.Services;
using RESTAPI.Services;

namespace RESTAPI.Setups
{
    public class DependencyInjectionService
    {
        public void SetupServices(IServiceCollection services)
        {
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
            services.AddTransient<IDummyNetworkService, DummyNetworkService>();
        }
    }
}
