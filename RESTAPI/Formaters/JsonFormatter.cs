using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CommonLib.Formaters;
using CommonLib.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace RESTAPI.Formaters
{
    public class JsonFormatter : TextOutputFormatter
    {
        private static ResponseFactory _responseFactory = new ResponseFactory(CustomResponseType.success);

        /// <summary />
        public JsonFormatter()
        {
            var types = MediaTypeHeaderValue.ParseList(new List<string>()
            {
                "text/html",
                "application/json",
                "text/plain"
            });

            foreach (var item in types)
            {
                SupportedMediaTypes.Add(item);
            }

            SupportedEncodings.Add(Encoding.UTF8);
        }

        /// <summary />
        public override bool CanWriteResult(OutputFormatterCanWriteContext context)
        {
            return true;
        }

        /// <summary />
        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            return _responseFactory.WriteFullResponseAsync(context.HttpContext, context.Object);
        }
    }
}
