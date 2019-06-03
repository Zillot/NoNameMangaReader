using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommonLib.Services
{
    public interface IDummyNetworkService
    {
        void SetBaseUri(string baseUri);
        
        Task<string> PostObject(
            string relativeUri,
            Dictionary<string, string> parameters,
            object body);

        Task<string> Post(
            string relativeUri,
            Dictionary<string, string> parameters,
            string body);

        Task<string> Get(
            string relativeUri,
            Dictionary<string, string> parameters);
    }
}
