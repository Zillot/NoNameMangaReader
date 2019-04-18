using CommonLib.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Formaters
{
    /// <summary />
    public class ResponseFactory
    {
        /// <summary />
        public readonly static JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        private CustomResponseType _responseType { get; set; }

        /// <summary />
        public ResponseFactory(CustomResponseType responseType)
        {
            _responseType = responseType;
        }

        /// <summary />
        private string getResponseInCommonJsonString(object obj)
        {
            return JsonConvert.SerializeObject(
                new CustomResponse(_responseType)
                {
                    Response = obj
                },
                JsonSerializerSettings);
        }

        /// <summary />
        private string getSafeLocalResponseAsRawJsonString(object obj)
        {
            var str = obj.ToString();

            var looksLikeObject = str.StartsWith("{") && str.EndsWith("}");
            var looksLikeArray = str.StartsWith("[") && str.EndsWith("]");

            if (!looksLikeObject && !looksLikeArray)
            {
                str = JsonConvert.SerializeObject(getResponseInCommonJsonString(str), JsonSerializerSettings);
            }

            return str;
        }

        /// <summary />
        public Task WriteFullResponseAsync(HttpContext httpContext, object response)
        {
            var str = getResponseInCommonJsonString(response);
            return writeResponseAsync(httpContext, str);
        }

        /// <summary />
        public Task WriteSealedResponseAsync(HttpContext httpContext, object response)
        {
            var str = getSafeLocalResponseAsRawJsonString(response);
            return writeResponseAsync(httpContext, str);
        }

        /// <summary />
        private Task writeResponseAsync(HttpContext httpContext, string str)
        {
            httpContext.Response.ContentLength = Encoding.UTF8.GetByteCount(str);
            httpContext.Response.ContentType = "application/json";
            return httpContext.Response.WriteAsync(str, Encoding.UTF8, httpContext.RequestAborted);
        }
    }
}
