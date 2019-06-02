using CommonLib.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NNMR.Models.DBModels
{
    [Table("UserFavorite")]
    public class UserFavoriteDB : IBaseDBModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MangaId { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
