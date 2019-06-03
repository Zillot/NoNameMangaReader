using System.Threading.Tasks;
using WebParser.Model.DTOModels;

namespace WebParser.BL.Services.PageParser
{
    public interface IPageParserService
    {
        void ParserWorker();
        void ProccessOrder(PageParseOrderDTO nexOrder);
    };
}
