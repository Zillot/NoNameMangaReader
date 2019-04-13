using WebParser.Model.DTOModels;
using WebParser.Model.Enums;

namespace WebParser.BL.Services.ParseOrders
{
    public interface IParseOrdersService
    {
        PageParseOrderDTO TryToStartNewParse(string url, OrderPriority priority);
        PageParseOrderDTO StartNewParse(string url, OrderPriority priority);
        PageParseOrderDTO GetParseOrderByGUID(string orderGUID);
        PageParseOrderDTO PopNextOrderFromQueue();
    }
}
