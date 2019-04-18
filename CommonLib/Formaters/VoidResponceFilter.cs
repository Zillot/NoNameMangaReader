using System;
using System.Threading.Tasks;
using CommonLib.Models;
using Microsoft.AspNetCore.Http;

namespace CommonLib.Formaters
{
    public class VoidResponseFilter
    {
        private static ResponseFactory _responseFactory = new ResponseFactory(CustomResponseType.success);

        /// <summary />
        public static async Task ManageVoidResponse(HttpContext context, Func<Task> next)
        {
            await next();

            var voidResponse = context.Response.StatusCode == 200 && context.Response.ContentType == null;
            var nullResponse = context.Response.StatusCode == 204;

            if (voidResponse || nullResponse)
            {
                if (nullResponse)
                {
                    context.Response.StatusCode = 200;
                }

                SetEmptyResponse(context);
            }
        }

        /// <summary />
        public static void SetEmptyResponse(HttpContext httpContext)
        {
            _responseFactory.WriteFullResponseAsync(httpContext, null);
        }
    }
}
