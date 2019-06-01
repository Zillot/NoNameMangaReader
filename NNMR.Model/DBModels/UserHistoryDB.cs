using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NNMR.Models.DBModels
{
    [Table("UserHistory")]
    public class UserHistoryDB
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MangaId { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
