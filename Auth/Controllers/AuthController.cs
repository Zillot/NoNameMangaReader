using Microsoft.AspNetCore.Mvc;
using Auth.BL.Services;
using Auth.Model.DTOModels;
using CommonLib.Attributes;

namespace Auth.Controllers
{
    public class AuthController
    {
        private IAuthService _authService { get; set; }

        public AuthController(
            IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [HaveSSH]
        public string UserLogin([FromBody]UserCredentialsDTO credentials)
        {
            return _authService.Login(credentials);
        }

        [HttpPost]
        [HaveSSH]
        public string AppLogin([FromBody]AppCredentialsDTO credentials)
        {
            return _authService.Login(credentials);
        }
    }
}
