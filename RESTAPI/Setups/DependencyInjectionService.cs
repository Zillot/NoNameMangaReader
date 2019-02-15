using CommonLib.Services;
using Microsoft.Extensions.DependencyInjection;

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
