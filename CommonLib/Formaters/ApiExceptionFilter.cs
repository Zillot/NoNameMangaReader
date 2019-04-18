using CommonLib.Models;
using CommonLib.Models.Exeptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace CommonLib.Formaters
{
    public class ApiExceptionMiddleware
    {
        private static ResponseFactory _responseFactory = new ResponseFactory(CustomResponseType.error);
        private readonly RequestDelegate next;

        /// <summary />
        public ApiExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary />
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

        /// <summary />
        private Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            NNMRException eqEx = null;

            if (exception is NNMRException)
            {
                eqEx = exception as NNMRException;
                httpContext.Response.StatusCode = eqEx.StatusCode ?? 400;
                //bad request as default key for any manually trowed exception
            }
            else
            {
                eqEx = new UnhandeledException();
                var msg = exception.GetBaseException().Message;
                string stack = exception.StackTrace;
                eqEx.Detail = $"{msg} {stack}";

                httpContext.Response.StatusCode = 500;
            }

            var detail = string.IsNullOrWhiteSpace(eqEx.Detail) ? null : eqEx.Detail;
            var response = new
            {
                eqEx.ErrorCode,
                eqEx.ErrorMessage,
                detail
            };

            return _responseFactory.WriteFullResponseAsync(httpContext, response);
        }
    }
}
