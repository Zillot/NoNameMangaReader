using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommonLib.Attributes;
using Flurl.Http;
using Newtonsoft.Json;

namespace CommonLib.Services
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

        public async Task<string> PostObject(
            string relativeUri,
            Dictionary<string, string> parameters,
            object bodyObject)
        {
            var uri = $"{_baseAddress}{relativeUri}";

            try
            {
                return await new FlurlRequest(new Flurl.Url(uri))
                    .SetQueryParams(parameters)
                    .WithHeaders(new
                    {
                        PrivateSSH = HaveSSHFilter.ExpectedSSH
                    })
                    .PostJsonAsync(bodyObject)
                    .ReceiveString();
            }
            catch (FlurlHttpException ex)
            {
                return ex.GetResponseStringAsync().Result ?? "no response";
            }
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
                    .WithHeaders(new
                    {
                        PrivateSSH = HaveSSHFilter.ExpectedSSH
                    })
                    .PostJsonAsync(JsonConvert.DeserializeObject(body))
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
                    .WithHeaders(new
                    {
                        PrivateSSH = HaveSSHFilter.ExpectedSSH
                    })
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
