using Microsoft.Extensions.DependencyInjection;
using NNMR.BL.Services;

namespace NNMR.Setups
{
    public class DependencyInjectionService
    {
        public void SetupServices(IServiceCollection services)
        {
            services.AddTransient<IMangaService, MangaService>();
        }
    }
}
