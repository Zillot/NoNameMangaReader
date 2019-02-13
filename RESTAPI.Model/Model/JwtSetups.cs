using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace RESTAPI.Model
{
    public class JwtSetups
    {
        public static readonly string ValidIssuer = "NNMR";
        public static readonly string ValidAudience = "NNMRSCWS";

        public void SetupJwt(IServiceCollection services, IConfiguration configuration)
        {
            var secretForToken = configuration["SecretForToken"];
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretForToken));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKeys = new List<SecurityKey>() { secretKey },
                        ValidateIssuer = true,
                        ValidIssuer = ValidIssuer,
                        ValidateAudience = true,
                        ValidAudience = ValidAudience,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(10)
                    };
                });
        }
    }
}
