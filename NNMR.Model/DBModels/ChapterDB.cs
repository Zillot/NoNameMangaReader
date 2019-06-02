using CommonLib.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NNMR.Models.DBModels
{
    [Table("Chapter")]
    public class ChapterDB : IBaseDBModel
    {
        public int Id { get; set; }
        public int MangaId { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
