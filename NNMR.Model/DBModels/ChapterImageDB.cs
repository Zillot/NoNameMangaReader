using CommonLib.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NNMR.Models.DBModels
{
    [Table("ChapterImage")]
    public class ChapterImageDB : IBaseDBModel
    {
        public int Id { get; set; }
        public int ChapterId { get; set; }
        public string URL { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
