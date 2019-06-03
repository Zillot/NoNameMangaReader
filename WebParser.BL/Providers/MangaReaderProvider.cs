using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebParser.BL.Services.PageParser;
using CommonLib.Models.DTOModels;
using CommonLib.Services;

namespace WebParser.BL.Providers
{
    public class MangaReaderProvider: BasePageProvider<MangaDTO>
    {
        private IDummyNetworkService _dummyNetworkService { get; set; }

        private static readonly string xName = "//*[@id='mangaBox']/div[2]/h1";
        private static readonly string xScore = "//*[@id='mangaBox']/div[2]/div[1]/div[1]/div[3]/span/span";
        private static readonly string xDescription = "//*[@id='mangaBox']/div[2]/div[1]/div[2]";
        private static readonly string xPoster = "//*[@id='mangaBox']/div[2]/div[1]/div[1]/div[1]/div/img";
        private static readonly string xInfo = "//*[@id='mangaBox']/div[2]/div[1]/div[1]/div[4]";
        private static readonly string xChapters = "//div[contains(@class, 'chapters-link')]/table/tr";

        public MangaReaderProvider(
            IProxyService proxyService,
            IDummyNetworkService dummyNetworkService) : base(proxyService)
        {
            _dummyNetworkService = dummyNetworkService;
        }

        public override async Task<MangaDTO> ProccessUrl(string pageUrl)
        {
            GetDoc("MangaPage").LoadHtml(await GetHtml(pageUrl));

            var nameNode = GetXPath("MangaPage", xName);
            var nameRus = nameNode.ChildNodes.FirstOrDefault(x => x.HasClass("name")).InnerText;
            var nameEng = nameNode.ChildNodes.FirstOrDefault(x => x.HasClass("eng-name")).InnerText;
            var nameOrg = nameNode.ChildNodes.FirstOrDefault(x => x.HasClass("original-name")).InnerText;

            var score = GetXPath("MangaPage", xScore)
                .Attributes.FirstOrDefault(x => x.Name == "data-score")?.Value;

            var description = GetXPath("MangaPage", xPoster)
                .ChildNodes.FirstOrDefault(x => x.Name == "p")?.InnerText;

            var posterUrl = GetXPath("MangaPage", xDescription)
                .Attributes[1]?.Value;

            var infoNodes = GetXPath("MangaPage", xInfo);
            var items = infoNodes.ChildNodes.Where(x => x.Name == "p");
            var volumes = GetMangaInfo(items, "Томов");
            var genre = GetMangaInfo(items, "Жанры");
            var categories = GetMangaInfo(items, "Категории");
            var author = GetMangaInfo(items, "Автор");
            var pubshlishYear = GetMangaInfo(items, "Год выпуска");
            var publisher = GetMangaInfo(items, "Издательство");
            var magazines = GetMangaInfo(items, "Журналы");
            //will have value like: over, continuing, translate, etc
            var state = GetMangaInfo(items, "Перевод");
            var translators = GetMangaInfo(items, "Переводчики");

            var manga = new MangaDTO()
            {
                PosterUrl = posterUrl,

                NameRus = nameRus,
                NameEng = nameEng,
                NameOrg = nameOrg,

                Description = description,
                Score = float.Parse(score),

                URL = pageUrl,
                ProviderId = 1,

                Volumes = volumes,
                Genre = genre,
                Categories = categories,
                Author = author,
                PushlishYear = pubshlishYear,
                Publisher = publisher,
                Magazines = magazines,
                State = state,
                Translators = translators,

                Chapters = new List<MangaChapterDTO>()
            };

            Task.Run(() => _dummyNetworkService.PostObject("Manga/SaveManga", null, manga)).Wait();

            var chapterNodes = ListXPath("MangaPage", xChapters);
            foreach (var chapter in chapterNodes)
            {
                try
                {
                    var parsedChapter = await GetMangaChapter(chapter);
                    Task.Run(() => _dummyNetworkService.PostObject("Manga/SaveMangaChapter", new Dictionary<string, string>()
                    {
                        { "mangaUrl", manga.URL }
                    }, parsedChapter)).Wait();

                    manga.Chapters.Add(parsedChapter);
                }
                catch (Exception ex)
                {
                    //TODO retry logic
                }
            };

            return manga;
        }

        public string GetMangaInfo(IEnumerable<HtmlNode> nodes, string key)
        {
            var text = nodes.FirstOrDefault(x => x.InnerText.Contains(key))?.InnerText.Replace("\n", "") ?? "";
            var r = new Regex("[ ]+");
            text = r.Replace(text, " ");
            text = text.Replace(key + ":", " ");

            return text.Trim();
        }

        public async Task<MangaChapterDTO> GetMangaChapter(HtmlNode chapterNode)
        {
            var node = chapterNode.ChildNodes[1].ChildNodes[1];

            var name = node.InnerText.Replace("\n", "");
            var r = new Regex("[ ]+");
            name = r.Replace(name, " ");

            var url = node.Attributes.FirstOrDefault(x => x.Name == "href").Value;
            var htmlText = await GetHtml("http://readmanga.me" + url);

            var line = htmlText.Split("rm_h.init( ")[1].Split(", 0, false);")[0];
            var json = JsonConvert.DeserializeObject<List<List<string>>>(line);
            var urlsForScreens = json.Select(x => x[1] + x[2]);

            return new MangaChapterDTO
            {
                Name = name,
                URL = url,
                URLs = urlsForScreens
            };
        }
    }
}
