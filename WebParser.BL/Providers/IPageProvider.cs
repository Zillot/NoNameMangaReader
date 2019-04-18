using System.Threading.Tasks;

namespace WebParser.BL.Providers
{
    public interface IPageProvider<T>
    {
        Task<T> ProccessUrl(string pageUrl);
    }
}
