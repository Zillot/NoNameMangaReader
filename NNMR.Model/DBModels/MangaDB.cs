using CommonLib.Models;
using CommonLib.Models.DTOModels;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NNMR.Models.DBModels
{
    [Table("Manga")]
    public class MangaDB: IBaseDBModel
    {
        public int Id { get; set; }
        public string NameENG { get; set; }
        public string URL { get; set; }
        public int ProviderId { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public static MangaDTO FromDB(MangaDB manga)
        {
            return new MangaDTO()
            {
                NameEng = manga.NameENG,
            };
        }
    }
}
