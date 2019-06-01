using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NNMR.Models.DBModels
{
    [Table("UserProgress")]
    public class UserProgressDB
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ChapterId { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
