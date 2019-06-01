using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebParser.DL.DBModels
{
    [Table("OrderStatus")]
    public class OrderStatusDB
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int StatusId { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
