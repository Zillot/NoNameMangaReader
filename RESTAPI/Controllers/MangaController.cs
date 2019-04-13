using Microsoft.AspNetCore.Mvc;
using RESTAPI.Controllers.Base;

namespace RESTAPI.Controllers
{
    public class MangaController : ControllerBase
    {
        [HttpGet]
        public dynamic Get(int id)
        {
            return null;
        }

        [HttpGet]
        public dynamic Post(dynamic DataModel)
        {
            return null;
        }
    }
}
