using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Custom.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace RESTAPI.Formaters
{
    public class RPJsonFormatter : TextOutputFormatter
    {
        public readonly static JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public RPJsonFormatter()
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

            var str = context.Object.ToString();

            var looksLikeObject = str.StartsWith("{") && str.EndsWith("}");
            var looksLikeArray = str.StartsWith("[") && str.EndsWith("]");

            if (!looksLikeObject && !looksLikeArray)
            {
                str = JsonConvert.SerializeObject(formatData(str), JsonSerializerSettings);
            }

            return context.HttpContext.Response.WriteAsync(
                str,
                selectedEncoding,
                context.HttpContext.RequestAborted);
        }

        private object formatData(object obj)
        {
            return new CustomResponse(CustomResponseType.success)
            {
                Response = obj
            };
        }
    }
}
