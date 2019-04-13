using CommonLib.Models;
using CommonLib.Models.Exeptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Threading.Tasks;

namespace CommonLib.Formaters
{
    public class ApiExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public readonly static JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public ApiExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            NNMRException eqEx = null;

            if (exception is NNMRException)
            {
                eqEx = exception as NNMRException;
                context.Response.StatusCode = eqEx.StatusCode ?? 400;
            }
            else
            {
                eqEx = new UnhandeledException();
                var msg = exception.GetBaseException().Message;
                string stack = exception.StackTrace;
                eqEx.Detail = $"{msg} {stack}";

                context.Response.StatusCode = 500;
            }

            var response = new CustomResponse(CustomResponseType.error) {
                Response = new {
                    eqEx.ErrorCode,
                    eqEx.ErrorMessage,
                    eqEx.Detail
                }
            };

            var result = new JsonResult(response, CustomJsonFormatter.JsonSerializerSettings);

            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(JsonConvert.SerializeObject(result.Value, JsonSerializerSettings));
        }
    }
}
