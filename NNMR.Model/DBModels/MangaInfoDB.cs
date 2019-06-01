using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NNMR.Models.DBModels
{
    [Table("MangaInfo")]
    public class MangaInfoDB
    {
        public int Id { get; set; }
        public int MangaId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
