using Microsoft.Extensions.DependencyInjection;
using Common.BL.Services;
using RESTAPI.BL.Services;

namespace RESTAPI.Setups
{
    public class DependencyInjectionService
    {
        public void SetupServices(IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        }
    }
}
