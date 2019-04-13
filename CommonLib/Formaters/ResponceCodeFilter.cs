using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace CommonLib.Formaters
{
    public class ResponseCodeFilter
    {
        public static async Task ManageResponseCodes(HttpContext context, Func<Task> next)
        {
            await next();

            if (context.Response.StatusCode != 200 && !context.Response.HasStarted)
            {
                GetResponseForCodeError(context);
                await next();
            }
        }

        public static void GetResponseForCodeError(HttpContext context)
        {
            string originalPath = context.Request.Path.Value;
            context.Items["originalPath"] = originalPath;
            context.Request.Method = "GET";

            switch (context.Response.StatusCode)
            {
                case 401: SetErrorResponse(context, "AUTH.5", "Unauthorized"); break;
                case 404: SetErrorResponse(context, "SYSTEMS.2", "Wrong route"); break;
                case 405: SetErrorResponse(context, "AUTH.6", "Method Not Allowed"); break;
                default: SetErrorResponse(context, "1", "Unhadeled status error"); break;
            }
        }

        public static void SetErrorResponse(HttpContext context, string errorCode, string errorMessage)
        {
            context.Request.Path = $"/Errors/Error";
            context.Request.QueryString = new QueryString($"?errorCode={errorCode}&errorText={errorMessage}&StatusCode={context.Response.StatusCode}");
        }
    }
}
