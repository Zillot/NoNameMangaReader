using Flurl;
using Flurl.Http;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebParser.BL.Services.ParseOrders;
using WebParser.Model.DTOModels;
using Newtonsoft.Json;
using Flurl.Http.Configuration;
using System.Net.Http;
using System.Net;

namespace WebParser.BL.Services.PageParser
{
    public class PageParserService: IPageParserService
    {
        //private IParseOrdersService _parseOrdersService { get; set; }

        public PageParserService()//IParseOrdersService parseOrdersService)
        {
            //_parseOrdersService = parseOrdersService;
        }

        public void ParserWorker()
        {
            //var nexOrder = _parseOrdersService.PopNextOrderFromQueue();

            //ProccessOrder(nexOrder);
        }

        public async Task ProccessOrder(PageParseOrderDTO nexOrder)
        {
            FlurlHttp.Configure(settings => { settings.HttpClientFactory = new ProxyHttpClientFactory("http://190.53.46.14:38525"); });

            var htmltext = await new FlurlRequest(new Flurl.Url(nexOrder.Url)).GetStringAsync();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmltext);

            var mangaNode = doc.DocumentNode.SelectSingleNode("//*[@id='mangaBox']/div[2]/h1");
            var mangaNameRus = mangaNode.ChildNodes.FirstOrDefault(x => x.HasClass("name")).InnerText;
            var mangaNameEng = mangaNode.ChildNodes.FirstOrDefault(x => x.HasClass("eng-name")).InnerText;
            var mangaNameOrg = mangaNode.ChildNodes.FirstOrDefault(x => x.HasClass("original-name")).InnerText;

            var scoreNode = doc.DocumentNode.SelectSingleNode("//*[@id='mangaBox']/div[2]/div[1]/div[1]/div[3]/span/span");
            var mangaScore = scoreNode.Attributes.FirstOrDefault(x => x.Name == "data-score")?.Value;

            var descriptionNode = doc.DocumentNode.SelectSingleNode("//*[@id='mangaBox']/div[2]/div[1]/div[2]");
            var description = descriptionNode.ChildNodes.FirstOrDefault(x => x.Name=="p")?.InnerText;

            var posterNode = doc.DocumentNode.SelectSingleNode("//*[@id='mangaBox']/div[2]/div[1]/div[1]/div[1]/div/img");
            var posterUrl = posterNode.Attributes[1]?.Value;
            
            var mangaNodes = doc.DocumentNode.SelectSingleNode("//*[@id='mangaBox']/div[2]/div[1]/div[1]/div[4]");
            var items = mangaNodes.ChildNodes.Where(x => x.Name == "p");
            var tomov = getMangaInfo(items, "Томов");
            var perevod = getMangaInfo(items, "Перевод");
            var janri = getMangaInfo(items, "Жанры");
            var kategorii = getMangaInfo(items, "Категории");
            var avtor = getMangaInfo(items, "Автор");
            var god_vupuska = getMangaInfo(items, "Год выпуска");
            var izdatelstvo = getMangaInfo(items, "Издательство");
            var jurnalu = getMangaInfo(items, "Журналы");
            var perevodchiki = getMangaInfo(items, "Переводчики");

            var chapterNodes1 = doc.DocumentNode.SelectNodes("//div[contains(@class, 'chapters-link')]/table/tr");
            var chapterNodes2 = doc.DocumentNode.SelectNodes("//div[contains(@class, 'chapters-link')]/table/tbody/tr");
            var chapterNodes = chapterNodes1 ?? chapterNodes2;
            //*[@id="mangaBox"]/div[2]/div[4]/table/tbody/tr[2]
            var chapters = new List<MangaChapter>();
            foreach (var chapter in chapterNodes)
            {
                try
                {
                    chapters.Add(await getMangaChapter(chapter));
                }
                catch(Exception ex)
                {

                }
            };
        }

        public string getMangaInfo(IEnumerable<HtmlNode> nodes, string key)
        {
            var text = nodes.FirstOrDefault(x => x.InnerText.Contains(key))?.InnerText.Replace("\n", "") ?? "";
            var r = new Regex("[ ]+");
            text = r.Replace(text, " ");
            text = text.Replace(key + ":", " ");

            return text.Trim();
        }

        public async Task<MangaChapter> getMangaChapter(HtmlNode chapterNode)
        {
            var name = chapterNode.ChildNodes[1].ChildNodes[1].InnerText.Replace("\n", "");
            var r = new Regex("[ ]+");
            name = r.Replace(name, " ");

            var url = chapterNode.ChildNodes[1].ChildNodes[1].Attributes.FirstOrDefault(x => x.Name == "href").Value;

            FlurlHttp.Configure(settings => { settings.HttpClientFactory = new ProxyHttpClientFactory("http://180.250.41.124:8080"); });
            var htmltext = await new FlurlRequest(new Flurl.Url("http://readmanga.me" + url)).GetStringAsync();
            var line = htmltext.Split("rm_h.init( ")[1].Split(", 0, false);")[0];
            var json = JsonConvert.DeserializeObject<List<List<string>>>(line);
            var urlsForScreens = json.Select(x => x[1] + x[2]);

            return new MangaChapter
            {
                Name = name,
                Url = url,
                Urls = urlsForScreens
            };
        }

        public class MangaChapter
        {
            public string Url { get; set; }
            public string Name { get; set; }
            public IEnumerable<string> Urls { get; set; }
        }

        public class ProxyHttpClientFactory : DefaultHttpClientFactory
        {
            private string _address;

            public ProxyHttpClientFactory(string address)
            {
                _address = address;
            }

            public override HttpMessageHandler CreateMessageHandler()
            {
                return new HttpClientHandler
                {
                    Proxy = new WebProxy(_address),
                    UseProxy = true
                };
            }
        }
    }
}
