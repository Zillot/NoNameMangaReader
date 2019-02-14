using Common.Model.Exeptions;
using Microsoft.AspNetCore.Mvc;
using RESTAPI.Controllers.Base;

namespace RESTAPI.Controllers
{
    public class ErrorsController : BaseController
    {
        [HttpGet]
        public string Error(string errorCode, string errorText, int StatusCode)
        {
            throw new CustomException(errorCode, errorText, StatusCode);
        }

        [HttpGet]
        public string Empty()
        {
            return "";
        } 
    }
}
