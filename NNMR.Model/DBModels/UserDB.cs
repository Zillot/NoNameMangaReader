using CommonLib.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NNMR.Models.DBModels
{
    [Table("User")]
    public class UserDB : IBaseDBModel
    {
        public int Id { get; set; }
        public int AuthId { get; set; }
        public string Login { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
