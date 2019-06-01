using CommonLib.EF;
using NNMR.Models.DBModels;
using WebParser.DL;

namespace NNMR.DL.Repositories
{
    public class MangaRepository : BaseRepository<MangaDB, NNMRDbContext>, IMangaRepository
    {
        public MangaRepository(NNMRDbContext dbContext): base(dbContext)
        {

        }
    }
}
