using System;
using System.Collections.Generic;
using System.Text;

namespace WebParser.Model.Models
{
    public class MangaChapter
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Urls { get; set; }
    }
}
