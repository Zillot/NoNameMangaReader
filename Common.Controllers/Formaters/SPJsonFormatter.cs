using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Custom.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Common.Controllers.Formaters
{
    public class CustomJsonFormatter : TextOutputFormatter
    {
        public readonly static JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public CustomJsonFormatter()
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

        public override bool CanWriteResult(OutputFormatterCanWriteContext context)
        {
            return true;
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            context.HttpContext.Response.ContentType = "application/json";

            var str = JsonConvert.SerializeObject(FormatData(context.Object), JsonSerializerSettings);

            return context.HttpContext.Response.WriteAsync(
                str,
                selectedEncoding,
                context.HttpContext.RequestAborted);
        }

        private object FormatData(object obj)
        {
            return new CustomResponce(CustomResponseType.success)
            {
                Response = obj
            };
        }
    }
}
