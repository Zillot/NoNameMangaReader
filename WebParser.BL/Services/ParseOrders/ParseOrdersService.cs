using System;
using System.Linq;
using System.Collections.Generic;
using WebParser.BL.Services.PageParser;
using WebParser.Model.DTOModels;
using WebParser.Model.Enums;

namespace WebParser.BL.Services.ParseOrders
{
    public class ParseOrdersService: IParseOrdersService
    {
        private IPageParserService _pageParserService { get; set; }

        private static List<PageParseOrderDTO> queueOfOrdersToParse { get; set; }

        public ParseOrdersService(
            IPageParserService pageParserService)
        {
            _pageParserService = pageParserService;

            queueOfOrdersToParse = new List<PageParseOrderDTO>();
        }

        public PageParseOrderDTO TryToStartNewParse(string url, OrderPriority priority)
        {
            var parseOrder = GetParseOrderByUrl(url);
            if (parseOrder != null)
            {
                return parseOrder;
            }

            return StartNewParse(url, priority);
        }

        public PageParseOrderDTO StartNewParse(string url, OrderPriority priority)
        {
            var newParseOrder = new PageParseOrderDTO()
            {
                OrderCreated = DateTime.Now,
                OrderGUID = Guid.NewGuid().ToString(),
                Url = url,
                Priority = priority
            };

            lock (queueOfOrdersToParse)
            {
                queueOfOrdersToParse.Add(newParseOrder);
            }

            return newParseOrder;
        }

        public PageParseOrderDTO GetParseOrderByUrl(string url)
        {
            lock (queueOfOrdersToParse)
            {
                return queueOfOrdersToParse.FirstOrDefault(x => x.Url == url);
            }
        }

        public PageParseOrderDTO GetParseOrderByGUID(string orderGUID)
        {
            lock (queueOfOrdersToParse)
            {
                return queueOfOrdersToParse.FirstOrDefault(x => x.OrderGUID == orderGUID);
            }
        }

        public PageParseOrderDTO PopNextOrderFromQueue()
        {
            lock (queueOfOrdersToParse)
            {
                var lisOrPriorities = new List<OrderPriority>()
                {
                    OrderPriority.Hight, OrderPriority.Normal, OrderPriority.Low
                };

                foreach (var priority in lisOrPriorities)
                {
                    var nextOrder = queueOfOrdersToParse.FirstOrDefault(x => x.Priority == priority);

                    if (nextOrder != null)
                    {
                        queueOfOrdersToParse.Remove(nextOrder);
                        return nextOrder;
                    }
                }

                return null;
            }
        }
    }
}
