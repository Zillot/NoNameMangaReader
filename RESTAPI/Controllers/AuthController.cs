using RESTAPI.Services;
using Microsoft.AspNetCore.Mvc;
using RESTAPI.Controllers.Base;
using System.Threading.Tasks;

namespace RESTAPI.Controllers
{
    public class AuthController : BaseController
    {
        public IDummyNetworkService _dummyNetworkService { get; set; }

        public AuthController(IDummyNetworkService dummyNetworkService)
        {
            _dummyNetworkService = dummyNetworkService;

            //TODO move it to some other place and use balancer
            _dummyNetworkService.SetBaseUri("http://localhost:51002/");
        }

        [HttpPost]
        public async Task<string> UserLogin(object emptyBody)
        {
            return await _dummyNetworkService.Post("auth/userLogin", null, getRawBody());
        }

        [HttpPost]
        public async Task<string> AppLogin(object jsonModel)
        {
            return await _dummyNetworkService.Post("auth/appLogin", null, getRawBody());
        }
    }
}
