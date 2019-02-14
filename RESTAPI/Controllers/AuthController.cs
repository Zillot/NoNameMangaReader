﻿using Microsoft.AspNetCore.Mvc;
using RESTAPI.BL.Services;
using RESTAPI.Model.DTOModels;

namespace RESTAPI.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
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