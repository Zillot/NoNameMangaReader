using System;
using System.Text;
using System.Threading.Tasks;
using CommonLib.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CommonLib.Formaters
{
    public class VoidResponseFilter
    {
        public static async Task ManageVoidResponse(HttpContext context, Func<Task> next)
        {
            await next();
            
            var voidResponse = context.Response.StatusCode == 200 && context.Response.ContentType == null;
            var nullResponse = context.Response.StatusCode == 204;
            if (voidResponse || nullResponse)
            {
                SetEmptyResponse(context);
                await next();
            }
        }

        public static void SetEmptyResponse(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            var str = JsonConvert.SerializeObject(
                new CustomResponse(CustomResponseType.success),
                CustomJsonFormatter.JsonSerializerSettings);

            context.Response.StatusCode = 200;
            context.Response.ContentLength = str.Length;

            context.Response.WriteAsync(
                str,
                Encoding.UTF8,
                context.RequestAborted);
        }
    }
}
