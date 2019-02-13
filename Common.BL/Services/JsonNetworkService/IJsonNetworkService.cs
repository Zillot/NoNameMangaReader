using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Common.BL.Services
{
    public interface IJsonNetworkService
    {
        void SetAuthToken(string token);
        void SetBaseUri(string baseUri);

        Task<T> Post<T>(
            string relativeUri,
            Dictionary<string, string> parameters,
            object body);
        Task<T> Post<T>(
            string relativeUri,
            Dictionary<string, string> parameters,
            object body,
            CancellationToken cancellationToken);

        Task<T> Get<T>(
            string relativeUri,
            Dictionary<string, string> parameters);
        Task<T> Get<T>(
            string relativeUri,
            Dictionary<string, string> parameters,
            CancellationToken cancellationToken);
    }
}
