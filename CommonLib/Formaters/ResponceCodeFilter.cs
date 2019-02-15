using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace CommonLib.Formaters
{
    public class ResponceCodeFilter
    {
        public static async Task ManageResponceCodes(HttpContext context, Func<Task> next)
        {
            await next();

            if (context.Response.StatusCode != 200 && !context.Response.HasStarted)
            {
                GetResponceForCodeError(context);
                await next();
            }
        }

        public static void GetResponceForCodeError(HttpContext context)
        {
            string originalPath = context.Request.Path.Value;
            context.Items["originalPath"] = originalPath;
            context.Request.Method = "GET";

            switch (context.Response.StatusCode)
            {
                case 401: SetErrorResponce(context, "AUTH.5", "Unauthorized"); break;
                case 404: SetErrorResponce(context, "SYSTEMS.2", "Wrong route"); break;
                case 405: SetErrorResponce(context, "AUTH.6", "Method Not Allowed"); break;
                default: SetErrorResponce(context, "1", "Unhadeled status error"); break;
            }
        }

        public static void SetErrorResponce(HttpContext context, string errorCode, string errorMessage)
        {
            context.Request.Path = $"/Errors/Error";
            context.Request.QueryString = new QueryString($"?errorCode={errorCode}&errorText={errorMessage}&StatusCode={context.Response.StatusCode}");
        }
    }
}
