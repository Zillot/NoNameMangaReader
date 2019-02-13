using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;

namespace Common.BL.Services
{
    public class JsonNetworkService: IJsonNetworkService
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
            object body)
        {
            return await Post<T>(relativeUri, parameters, body, new CancellationToken());
        }

        public async Task<T> Post<T>(
            string relativeUri,
            Dictionary<string, string> parameters,
            object body,
            CancellationToken cancellationToken)
        {
            var uri = $"{_baseAddress}{relativeUri}";

            return await new FlurlRequest(new Flurl.Url(uri))
                .SetQueryParams(parameters)
                .WithHeaders(new
                {
                    Authorization = $"Bearer {_authToken}"
                })
                .PostJsonAsync(body, cancellationToken)
                .ReceiveJson<T>();
        }

        public async Task<T> Get<T>(
            string relativeUri,
            Dictionary<string, string> parameters)
        {
            return await Get<T>(relativeUri, parameters, new CancellationToken());
        }

        public async Task<T> Get<T>(
            string relativeUri,
            Dictionary<string, string> parameters, 
            CancellationToken cancellationToken)
        {
            var uri = $"{_baseAddress}{relativeUri}";

            return await new FlurlRequest(new Flurl.Url(uri))
                .SetQueryParams(parameters)
                .WithHeaders(new
                {
                    Authorization = $"Bearer {_authToken}"
                })
                .GetAsync(cancellationToken)
                .ReceiveJson<T>();
        }
    }
}
