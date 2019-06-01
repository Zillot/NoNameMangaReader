using Microsoft.EntityFrameworkCore;
using WebParser.DL.DBModels;

namespace WebParser.DL
{
    public class WebParserDbContext: DbContext
    {
        public WebParserDbContext(DbContextOptions<WebParserDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<OrderDB> Orders { get; set; }
        public DbSet<OrderStatusDB> OrderStatuses { get; set; }
        public DbSet<PriorityTypeDB> PriorityTypes { get; set; }
        public DbSet<StatusTypeDB> StatusTypes { get; set; }
    }
}