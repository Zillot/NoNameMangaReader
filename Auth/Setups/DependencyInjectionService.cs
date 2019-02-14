using Auth.BL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Setups
{
    public class DependencyInjectionService
    {
        public void SetupServices(IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
        }
    }
}
