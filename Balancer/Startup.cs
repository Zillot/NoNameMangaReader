using Balancer.Setups;
using CommonLib.Formaters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Balancer
{
    public class Startup
    {
        private IConfiguration _configuration { get; }
        private DependencyInjectionService _dependencyInjectionService { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            _dependencyInjectionService = new DependencyInjectionService();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _dependencyInjectionService.SetupServices(services);

            services.AddCors();

            services.AddMvc(options =>
            {
                options.OutputFormatters.RemoveType<TextOutputFormatter>();
                options.OutputFormatters.RemoveType<JsonOutputFormatter>();
                options.OutputFormatters.RemoveType<StringOutputFormatter>();

                options.OutputFormatters.Add(new CustomJsonFormatter());
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //response status manager
            app.Use(ResponseCodeFilter.ManageResponseCodes);
            //void response manager
            app.Use(VoidResponseFilter.ManageVoidResponse);

            app.UseMiddleware(typeof(ApiExceptionMiddleware));

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller}/{action}");
            });
        }
    }
}
