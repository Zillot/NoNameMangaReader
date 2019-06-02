using CommonLib.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebParser.DL.DBModels
{
    [Table("RoleType")]
    public class RoleTypeDB : IBaseDBModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Premission { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
