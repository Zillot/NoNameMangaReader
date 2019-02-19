using CommonLib.Models.Exeptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTAPI.Model.Exceptions;
using RESTAPI.Model.Models;

namespace RESTAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class APITestsController : ControllerBase
    {
        /// <summary>
        /// Check if server alive
        /// </summary>
        /// <returns>String with status</returns>
        [HttpGet]
        public string IsAlive()
        {
            return "I am alive";
        }

        /// <summary>
        /// Response with no return type
        /// </summary>
        [HttpGet]
        public void GetVoid()
        {
            //will return "" as response
        }

        /// <summary>
        /// Response for GET
        /// </summary>
        /// <returns>Static string</returns>
        [HttpGet]
        public string GetTest()
        {
            return "get is ok";
        }

        /// <summary>
        /// Response of model for GET
        /// </summary>
        /// <returns>Model with static values</returns>
        [HttpGet]
        public PostTestObject GetModel()
        {
            return new PostTestObject()
            {
                Key = "Your",
                Name = "Model"
            };
        }

        /// <summary>
        /// Response for GET with params
        /// </summary>
        /// <param name="value">Parameter of request</param>
        /// <returns>Static string in format "here is your value {value}"</returns>
        [HttpGet]
        public string GetMyVlaue(string value)
        {
            return $"here is your value {value}";
        }

        /// <summary>
        /// Response for POST
        /// </summary>
        /// <returns>Static string</returns>
        [HttpPost]
        public string PostTest()
        {
            return "post is ok";
        }

        /// <summary>
        /// Response for POST with body
        /// </summary>
        /// <param name="model">Body of request</param>
        /// <returns>Static string in format "your name is {model.Name} and key is {model.Key}"</returns>
        [HttpPost]
        [AllowAnonymous]
        public string PostTestWithBody([FromBody] PostTestObject model)
        {
            return $"your name is {model.Name} and key is {model.Key}";
        }

        /// <summary>
        /// Response for POST with body and parameter
        /// </summary>
        /// <param name="value">Parameter of request</param>
        /// <param name="model">Body of request</param>
        /// <returns>Static string in format "your name is {model.Name} and key is {model.Key}, and the value is {value}"</returns>
        [HttpPost]
        public string PostTestWithBodyAndParams(string value, [FromBody] PostTestObject model)
        {
            return $"your name is {model.Name} and key is {model.Key}, and the value is {value}";
        }

        /// <summary>
        /// Response to check authorization
        /// </summary>
        /// <returns>Static string</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [HttpGet, Authorize]
        public string IsAuthorized()
        {
            return "your top security protected data";
        }

        /// <summary>
        /// Response to check errors №1, custom error source
        /// </summary>
        [ProducesResponseType(400)]
        [HttpGet]
        [AllowAnonymous]
        public void CustomError()
        {
            throw new NNMRException("custom error code", "the error explanation", "the error detail here");
        }

        /// <summary>
        /// Response to check errors №2, specific error source
        /// </summary>
        [ProducesResponseType(405)]
        [HttpGet]
        public void ClaimError()
        {
            throw new TokenMissmatchException();
        }

        /// <summary>
        /// Response to check errors №3, custom unexpected error
        /// </summary>
        [ProducesResponseType(500)]
        [HttpGet]
        public void UnError()
        {
            throw new UnhandeledException();
        }

        /// <summary>
        /// Response to check errors №4, system unexpected error
        /// </summary>
        /// <response code="500">unhandled exception</response>
        [ProducesResponseType(500)]
        [HttpGet]
        public void UnhandeledError()
        {
            var arrayOnNoItems = new int[0];
            var justThrowMeThatError = arrayOnNoItems[10];
        }
    }
}
