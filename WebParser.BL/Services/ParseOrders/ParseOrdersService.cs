using System;
using System.Linq;
using System.Collections.Generic;
using WebParser.Model.DTOModels;
using WebParser.Model.Enums;

namespace WebParser.BL.Services.ParseOrders
{
    public class ParseOrdersService: IParseOrdersService
    {
        private static List<PageParseOrderDTO> _queueOfOrdersToParse { get; set; }

        public ParseOrdersService()
        {
            _queueOfOrdersToParse = new List<PageParseOrderDTO>();
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

            lock (_queueOfOrdersToParse)
            {
                _queueOfOrdersToParse.Add(newParseOrder);
            }

            return newParseOrder;
        }

        public PageParseOrderDTO GetParseOrderByUrl(string url)
        {
            lock (_queueOfOrdersToParse)
            {
                return _queueOfOrdersToParse.FirstOrDefault(x => x.Url == url);
            }
        }

        public PageParseOrderDTO GetParseOrderByGUID(string orderGUID)
        {
            lock (_queueOfOrdersToParse)
            {
                return _queueOfOrdersToParse.FirstOrDefault(x => x.OrderGUID == orderGUID);
            }
        }

        public PageParseOrderDTO PopNextOrderFromQueue()
        {
            lock (_queueOfOrdersToParse)
            {
                var lisOrPriorities = new List<OrderPriority>()
                {
                    OrderPriority.Hight, OrderPriority.Normal, OrderPriority.Low
                };

                foreach (var priority in lisOrPriorities)
                {
                    var nextOrder = _queueOfOrdersToParse.FirstOrDefault(x => x.Priority == priority);

                    if (nextOrder != null)
                    {
                        _queueOfOrdersToParse.Remove(nextOrder);
                        return nextOrder;
                    }
                }

                return null;
            }
        }
    }
}
