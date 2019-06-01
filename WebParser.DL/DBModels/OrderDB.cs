using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebParser.DL.DBModels
{
    [Table("Order")]
    public class OrderDB
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string URL { get; set; }
        public string PageProvider { get; set; }
        public int PriorityId { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
