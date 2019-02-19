using CommonLib.Models.Exeptions;
using Microsoft.AspNetCore.Mvc;

namespace RESTAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class ErrorsController : ControllerBase
    {
        [HttpGet]
        public string Error(string errorCode, string errorText, int StatusCode)
        {
            throw new NNMRException(errorCode, errorText, StatusCode);
        }

        [HttpGet]
        public string Empty()
        {
            return "";
        } 
    }
}
