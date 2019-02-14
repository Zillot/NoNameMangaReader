using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl.Http;

namespace RESTAPI.Services
{
    public class DummyNetworkService : IDummyNetworkService
    {
        //TODO Cache and response with cache if posible instead of <no response>
        private Uri _baseAddress { get; set; }

        public DummyNetworkService()
        {

        }

        public void SetBaseUri(string baseUri)
        {
            _baseAddress = new Uri(baseUri);
        }

        public async Task<string> Post(
            string relativeUri,
            Dictionary<string, string> parameters,
            string body)
        {
            var uri = $"{_baseAddress}{relativeUri}";

            try
            {
                return await new FlurlRequest(new Flurl.Url(uri))
                    .SetQueryParams(parameters)
                    .PostStringAsync(body)
                    .ReceiveString();
            }
            catch (FlurlHttpException ex)
            {
                return ex.GetResponseStringAsync().Result ?? "no response";
            }
        }
        
        public async Task<string> Get(
            string relativeUri,
            Dictionary<string, string> parameters)
        {
            var uri = $"{_baseAddress}{relativeUri}";

            try
            {
                return await new FlurlRequest(new Flurl.Url(uri))
                    .SetQueryParams(parameters)
                    .GetAsync()
                    .ReceiveString();
            }
            catch(FlurlHttpException ex)
            {
                return ex.GetResponseStringAsync().Result ?? "no response";
            }
        }
    }
}
