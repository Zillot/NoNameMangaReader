using System;
using WebParser.Model.Enums;

namespace WebParser.Model.DTOModels
{
    public class PageParseOrderDTO
    {
        public string OrderGUID { get; set; }
        public string Url { get; set; }
        public string PageProvider { get; set; }
        public DateTime OrderCreated { get; set; }
        public string StatusStr { get; set; }
        public ParseStatus Status { get; set; }
        public OrderPriority Priority { get; set; }
    }
}
