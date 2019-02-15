using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CommonLib.Formaters
{
    public class VoidResponceFilter
    {
        public static async Task ManageVoidResponce(HttpContext context, Func<Task> next)
        {
            await next();

            //TODO define somehow that the body is empty
            if (context.Response.StatusCode == 200 && context.Response.ContentType == null)
            {
                GetEmptyResponce(context);
                await next();
            }
        }

        public static void GetEmptyResponce(HttpContext context)
        {
            context.Request.Path = $"/Errors/Empty";
        }
    }
}
