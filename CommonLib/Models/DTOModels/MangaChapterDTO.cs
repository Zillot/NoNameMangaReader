using System.Collections.Generic;

namespace CommonLib.Models.DTOModels
{
    public class MangaChapterDTO
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Urls { get; set; }
    }
}
