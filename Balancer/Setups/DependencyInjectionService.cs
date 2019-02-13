using Microsoft.Extensions.DependencyInjection;

namespace Balancer.Setups
{
    public class DependencyInjectionService
    {
        public void SetupServices(IServiceCollection services)
        {
            //services.AddTransient<IAuthService, AuthService>();
        }
    }
}
