using Common.Model.Exeptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTAPI.Controllers.Base;
using RESTAPI.Model.Exceptions;
using RESTAPI.Model.Models;

namespace RESTAPI.Controllers
{
    public class APITestsController : BaseController
    {
        [HttpGet]
        public string IsAlive()
        {
            return "I am alive";
        }

        [HttpGet]
        public void GetVoid()
        {
            //will return "" as responce
        }

        [HttpGet]
        public string GetTest()
        {
            return "get is ok";
        }

        [HttpGet]
        public string GetMyVlaue(string value)
        {
            return $"here is your value {value}";
        }

        [HttpPost]
        public string PostTest()
        {
            return "post is ok";
        }

        [HttpPost]
        public string PostTestWithBody([FromBody] PostTestObject model)
        {
            return $"your name is {model.Name} and key is {model.Key}";
        }

        [HttpPost]
        public string PostTestWithBodyAndParams(string value, [FromBody] PostTestObject model)
        {
            return $"your name is {model.Name} and key is {model.Key}, and the value is {value}";
        }

        [HttpGet, Authorize]
        public string IsAuthorized()
        {
            return "your top security protected data";
        }

        [HttpGet]
        public void CustomError()
        {
            throw new CustomException("custom error code", "the error explanation", "the error detail here");
        }

        [HttpGet]
        public void ClaimError()
        {
            throw new TokenMissmatchException();
        }

        [HttpGet]
        public void UnError()
        {
            throw new UnhandeledException();
        }

        [HttpGet]
        public void UnhandeledError()
        {
            var arrayOnNoItems = new int[0];
            var justThrowMeThatError = arrayOnNoItems[10];
        }
    }
}
