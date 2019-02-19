using CommonLib.Attributes;
using CommonLib.Formaters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RESTAPI.Formaters;
using RESTAPI.Setups;

namespace RESTAPI
{
    public class Startup
    {
        private IConfiguration _configuration { get; set; }
        private DependencyInjectionService _dependencyInjectionService { get; set; }
        private JwtSetups _jwtSetups { get; set; }
        private SwaggerSetups _swaggerSetups { get; set; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            _dependencyInjectionService = new DependencyInjectionService();
            _jwtSetups = new JwtSetups();
            _swaggerSetups = new SwaggerSetups();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _dependencyInjectionService.SetupServices(services);
            _jwtSetups.SetupJwt(services, _configuration);
            _swaggerSetups.SetupSwaggerServices(services);

            services.AddScoped<HaveSSHFilter>();

            services.AddCors();

            services.AddMvc(options =>
                {
                    options.OutputFormatters.RemoveType<TextOutputFormatter>();
                    options.OutputFormatters.RemoveType<JsonOutputFormatter>();
                    options.OutputFormatters.RemoveType<StringOutputFormatter>();

                    options.OutputFormatters.Add(new RPJsonFormatter());
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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

            //shows UseCors with CorsPolicyBuilder.
            app.UseCors(builder =>
               builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials());

            app.UseAuthentication();

            //respponce status manager
            app.Use(ResponceCodeFilter.ManageResponceCodes);
            //void respponce manager
            app.Use(VoidResponceFilter.ManageVoidResponce);

            app.UseMiddleware(typeof(ApiExceptionMiddleware));

            _swaggerSetups.SetupSwaggerApp(app);
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "api/{controller}/{action}");
            });
        }
    }
}
