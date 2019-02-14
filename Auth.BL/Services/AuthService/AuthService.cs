using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Auth.Model.DTOModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Common.BL.Services;
using Auth.Model.Exceptions;

namespace Auth.BL.Services
{
	public class AuthService : IAuthService
    {
        private readonly int TOKEN_EXPIRATION_FOR_USER_HOURS = 2;
        private readonly int TOKEN_EXPIRATION_FOR_APP_HOURS = 120;

        private IConfiguration _configuration { get; set; }
        private IDateTimeProvider _dateTimeProvider { get; set; }

        public AuthService(
            IConfiguration configuration,
            IDateTimeProvider dateTimeProvider)
        {
            _configuration = configuration;
            _dateTimeProvider = dateTimeProvider;
        }

        public string Login(UserCredentialsDTO credentials)
        {
            //TODO: get from DB
            var validLogin = _configuration["HardcodedLogin"] == credentials.Login;
            var validPassword = _configuration["HardcodedPassword"] == credentials.Password;

            if (validLogin && validPassword)
            {
                var claims = new List<Claim>()
                {
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(), ClaimValueTypes.Integer64)
                };

                return GenerateToken(claims, DateTime.Now.AddHours(TOKEN_EXPIRATION_FOR_USER_HOURS));
            }
            else
            {
                throw new CredentialsException();
            }
        }

        public string Login(AppCredentialsDTO credentials)
        {
            //TODO: get from DB
            var validSSHKey = _configuration["HardcodedSSHKey"] == credentials.SSHKey;

            if (validSSHKey)
            {
                var claims = new List<Claim>()
                {
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(), ClaimValueTypes.Integer64)
                };

                return GenerateToken(claims, DateTime.Now.AddDays(TOKEN_EXPIRATION_FOR_APP_HOURS));
            }

            return null;
        }

        private string GenerateToken(List<Claim> claims, DateTime expires)
        {
            //TODO: get from DB
            var secretForToken = _configuration["SecretForToken"];
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretForToken));

            var token = new JwtSecurityToken(
                issuer: "NNMR",
                audience: "NNMRSCWS",
                claims: claims,
                expires: expires,
                notBefore: DateTime.Now,
                signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
