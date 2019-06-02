using CommonLib.EF;
using NNMR.Models.DBModels;
using WebParser.DL;

namespace NNMR.DL.Repositories
{
    public class ChapterImageRepository : BaseRepository<ChapterImageDB, NNMRDbContext>, IChapterImageRepository
    {
        public ChapterImageRepository(NNMRDbContext dbContext): base(dbContext)
        {

        }
    }
}
