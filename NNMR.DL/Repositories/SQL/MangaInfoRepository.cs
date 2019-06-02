using CommonLib.EF;
using NNMR.Models.DBModels;
using WebParser.DL;

namespace NNMR.DL.Repositories
{
    public class MangaInfoRepository : BaseRepository<MangaInfoDB, NNMRDbContext>, IMangaInfoRepository
    {
        public MangaInfoRepository(NNMRDbContext dbContext): base(dbContext)
        {

        }
    }
}
