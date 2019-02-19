using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RESTAPI.Setups
{
    public class SwaggerSetups
    {
        public void SetupSwaggerServices(IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new Info()
                {
                    Title = "NNMR API gateway",
                    Description = "Public NNMR API"
                });

                var xmlPath = $"{AppDomain.CurrentDomain.BaseDirectory}RESTAPI.xml";
                opt.IncludeXmlComments(xmlPath);

                opt.AddSecurityDefinition("Bearer",
                    new ApiKeyScheme()
                    {
                        In = "header",
                        Description = "Please enter JWT with Bearer into field",
                        Name = "Authorization",
                        Type = "apiKey",
                    });

                opt.OperationFilter<SecurityRequirementsOperationFilter>();
            });
        }

        public void SetupSwaggerApp(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                //TODO make some style improvements
                //opt.InjectStylesheet("/swaggerstyle/custom.css");
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "NNMR API");
            });
        }
    }

    public class SecurityRequirementsOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            MethodInfo mInfo = null;
            context.ApiDescription.TryGetMethodInfo(out mInfo);

            var onlyAuthorize = mInfo
                .CustomAttributes
                .ToArray()
                .Any(x => x.AttributeType.Name == "AuthorizeAttribute");

            //TODO mark optional authorize requests
            var allowedAnonimouse = mInfo
                .CustomAttributes
                .ToArray()
                .Any(x => x.AttributeType.Name == "AllowAnonymousAttribute");

            if (onlyAuthorize)
            {
                if (allowedAnonimouse)
                {
                    operation.Summary = "OPTIONAL AUTH " + operation.Summary;
                }
                operation.Security = new List<IDictionary<string, IEnumerable<string>>>
                {
                    new Dictionary<string, IEnumerable<string>>
                    {
                        { "Bearer", Array.Empty<string>() }
                    }
                };
            }
        }
    }
}
