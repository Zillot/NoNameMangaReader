using System.Collections.Generic;

namespace WebParser.Model.Models
{
    public class Manga
    {
        public string PosterUrl { get; set; }

        public string NameRus { get; set; }
        public string NameEng { get; set; }
        public string NameOrg { get; set; }

        public string Description { get; set; }
        public float Score { get; set; }

        public string Volumes { get; set; }
        public string Genre { get; set; }
        public string Categories { get; set; }
        public string Author { get; set; }
        public string PushlishYear { get; set; }
        public string Publisher { get; set; }
        public string Magazines { get; set; }
        public string State { get; set; }
        public string Translators { get; set; }

        public List<MangaChapter> Chapters { get; set; }
    }
}
