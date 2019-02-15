using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CommonLib.Services
{
    public interface IJsonNetworkService<ErrorModel>
    {
        void SetAuthToken(string token);
        void SetBaseUri(string baseUri);

        Task<T> Post<T>(
            string relativeUri,
            Dictionary<string, string> parameters,
            object body,
            Action<ErrorModel> errorCallback);
        Task<T> Post<T>(
            string relativeUri,
            Dictionary<string, string> parameters,
            object body,
            Action<ErrorModel> errorCallback,
            CancellationToken cancellationToken);

        Task<T> Get<T>(
            string relativeUri,
            Dictionary<string, string> parameters,
            Action<ErrorModel> errorCallback);
        Task<T> Get<T>(
            string relativeUri,
            Dictionary<string, string> parameters,
            Action<ErrorModel> errorCallback,
            CancellationToken cancellationToken);
    }
}
