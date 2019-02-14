using Microsoft.AspNetCore.Mvc;
using Auth.BL.Services;
using Auth.Model.DTOModels;

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
        public ActionResult<string> UserLogin(UserCredentialsDTO credentials)
        {
            return _authService.Login(credentials);
        }

        [HttpPost]
        public ActionResult<string> AppLogin(AppCredentialsDTO credentials)
        {
            return _authService.Login(credentials);
        }
    }
}
