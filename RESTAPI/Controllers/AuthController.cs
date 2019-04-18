using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CommonLib.Services;
using RESTAPI.Controllers.Base;

namespace RESTAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private IDummyNetworkService _dummyNetworkService { get; set; }

        public AuthController(IDummyNetworkService dummyNetworkService)
        {
            _dummyNetworkService = dummyNetworkService;

            //TODO move it to some other place and use balancer
            _dummyNetworkService.SetBaseUri("http://localhost:51002/");
        }

        [HttpPost]
        public async Task<string> UserLogin(object emptyBody)
        {
            return await _dummyNetworkService.Post("auth/userLogin", null, this.GetRawBody());
        }

        [HttpPost]
        public async Task<string> AppLogin(object jsonModel)
        {
            return await _dummyNetworkService.Post("auth/appLogin", null, this.GetRawBody());
        }
    }
}
