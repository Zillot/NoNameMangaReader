using Microsoft.EntityFrameworkCore;
using NNMR.Models.DBModels;

namespace WebParser.DL
{
    public class NNMRDbContext: DbContext
    {
        public NNMRDbContext(DbContextOptions<NNMRDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ChapterDB> Chapters { get; set; }
        public DbSet<ChapterImageDB> ChapterImages { get; set; }
        public DbSet<MangaDB> Mangas { get; set; }
        public DbSet<MangaInfoDB> MangaInfos { get; set; }
        public DbSet<ProviderTypeDB> ProviderTypes { get; set; }
        public DbSet<UserDB> Users { get; set; }
        public DbSet<UserFavoriteDB> UserFavorites { get; set; }
        public DbSet<UserHistoryDB> UserHistories { get; set; }
        public DbSet<UserProgressDB> UserProgresses { get; set; }
    }
}