using System.Collections.Generic;

namespace CommonLib.Models.DTOModels
{
    public class MangaChapterDTO
    {
        public string URL { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> URLs { get; set; }
    }
}
