using CommonLib.EF;
using NNMR.Models.DBModels;
using WebParser.DL;

namespace NNMR.DL.Repositories
{
    public class ChapterRepository : BaseRepository<ChapterDB, NNMRDbContext>, IChapterRepository
    {
        public ChapterRepository(NNMRDbContext dbContext): base(dbContext)
        {

        }
    }
}
