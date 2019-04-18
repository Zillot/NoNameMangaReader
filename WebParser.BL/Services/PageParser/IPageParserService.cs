using System.Threading.Tasks;
using WebParser.Model.DTOModels;

namespace WebParser.BL.Services.PageParser
{
    public interface IPageParserService
    {
        void ProccessOrder(PageParseOrderDTO nexOrder);
    };
}
