using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebParser.DL.DBModels
{
    [Table("UserProfile")]
    public class UserProfileDB
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
