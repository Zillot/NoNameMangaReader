using CommonLib.Models.Exeptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTAPI.Controllers.Base;

namespace RESTAPI.Controllers
{
    public class ErrorsController : BaseController
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
