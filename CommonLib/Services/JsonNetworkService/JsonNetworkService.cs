using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;

namespace CommonLib.Services
{
    public class JsonNetworkService<ErrorModel> : IJsonNetworkService<ErrorModel>
    {
        private Uri _baseAddress { get; set; }
        private string _authToken { get; set; }

        public JsonNetworkService()
        {

        }

        public void SetAuthToken(string token)
        {
            _authToken = token;
        }

        public void SetBaseUri(string baseUri)
        {
            _baseAddress = new Uri(baseUri);
        }

        public async Task<T> Post<T>(
            string relativeUri,
            Dictionary<string, string> parameters,
            object body,
            Action<ErrorModel> errorCallback)
        {
            return await Post<T>(relativeUri, parameters, body, errorCallback, new CancellationToken());
        }

        public async Task<T> Post<T>(
            string relativeUri,
            Dictionary<string, string> parameters,
            object body,
            Action<ErrorModel> errorCallback,
            CancellationToken cancellationToken)
        {
            var uri = $"{_baseAddress}{relativeUri}";

            try
            {
                return await new FlurlRequest(new Flurl.Url(uri))
                    .SetQueryParams(parameters)
                    .WithHeaders(new
                    {
                        Authorization = $"Bearer {_authToken}"
                    })
                    .PostJsonAsync(body, cancellationToken)
                    .ReceiveJson<T>();
            }
            catch(FlurlHttpException ex)
            {
                if (errorCallback != null)
                {
                    var errorResponse = ex.GetResponseJsonAsync<ErrorModel>().Result;
                    errorCallback(errorResponse);
                }

                return default(T);
            }
        }

        public async Task<T> Get<T>(
            string relativeUri,
            Dictionary<string, string> parameters,
            Action<ErrorModel> errorCallback)
        {
            return await Get<T>(relativeUri, parameters, errorCallback, new CancellationToken());
        }

        public async Task<T> Get<T>(
            string relativeUri,
            Dictionary<string, string> parameters,
            Action<ErrorModel> errorCallback, 
            CancellationToken cancellationToken)
        {
            var uri = $"{_baseAddress}{relativeUri}";
            try
            {
                return await new FlurlRequest(new Flurl.Url(uri))
                    .SetQueryParams(parameters)
                    .WithHeaders(new
                    {
                        Authorization = $"Bearer {_authToken}"
                    })
                    .GetAsync(cancellationToken)
                    .ReceiveJson<T>();
            }
            catch(FlurlHttpException ex)
            {
                if (errorCallback != null)
                {
                    var errorResponse = ex.GetResponseJsonAsync<ErrorModel>().Result;
                    errorCallback(errorResponse);
                }

                return default(T);
            }
        }
    }
}
