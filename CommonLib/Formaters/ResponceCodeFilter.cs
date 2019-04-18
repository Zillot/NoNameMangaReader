using CommonLib.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace CommonLib.Formaters
{
    /// <summary />
    public class ResponseCodeFilter
    {
        private static ResponseFactory _responseFactory = new ResponseFactory(CustomResponseType.error);

        /// <summary />
        public static async Task ManageResponseCodes(HttpContext context, Func<Task> next)
        {
            await next();

            if (context.Response.StatusCode != 200 && !context.Response.HasStarted)
            {
                GetResponseForCodeError(context);
                await next();
            }
        }

        /// <summary />
        public static void GetResponseForCodeError(HttpContext context)
        {
            string originalPath = context.Request.Path.Value;
            context.Items["originalPath"] = originalPath;
            context.Request.Method = "GET";

            switch (context.Response.StatusCode)
            {
                case 401: SetErrorResponse(context, "A.401", "Unauthorized"); break;
                case 403: SetErrorResponse(context, "A.403", "Forbiden"); break;
                case 404: SetErrorResponse(context, "S.404", $"Wrong route {context.Request.Path}"); break;
                case 405: SetErrorResponse(context, "A.405", "Method Not Allowed"); break;
                default: SetErrorResponse(context, "1", "Unhadeled status error"); break;
            }
        }

        /// <summary />
        public static void SetErrorResponse(HttpContext httpContext, string errorCode, string errorMessage)
        {
            _responseFactory.WriteFullResponseAsync(httpContext, new
            {
                errorCode,
                errorMessage
            });
        }
    }
}
