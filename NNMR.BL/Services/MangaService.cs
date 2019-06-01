using CommonLib.Models.Exeptions;
using CommonLib.Services;
using NNMR.DL.Repositories;
using NNMR.Model.DTOModels;
using NNMR.Models.DBModels;

namespace NNMR.BL.Services
{
    public class MangaService
    {
        private IDummyNetworkService _dummyNetworkService { get; set; }
        private IMangaRepository _mangaRepository { get; set; }

        public MangaService(
            IDummyNetworkService dummyNetworkService,
            IMangaRepository mangaRepository)
        {
            _dummyNetworkService = dummyNetworkService;
            _mangaRepository = mangaRepository;

            //TODO move it to some other place and use balancer
            _dummyNetworkService.SetBaseUri("http://localhost:51005/");
        }

        public MangaDTO GetManga(string url)
        {
            var manga = _mangaRepository.FirstOrDefault(x => x.URL == url);
            if (manga == null)
            {
                return ProcessManga(manga);
            }
            else
            {
                _dummyNetworkService.Post("ParseOrders/ProccessManga", null, null);
                throw new System.Exception();
            }
        }

        public MangaDTO GetManga(int id)
        {
            var manga = _mangaRepository.FirstOrDefault(x => x.Id == id);
            if (manga != null)
            {
                return ProcessManga(manga);
            }
            else
            {
                throw new ObjectNotFoundException();
            }
        }

        public MangaDTO ProcessManga(MangaDB manga)
        {
            return MangaDTO.FromDB(manga);
        }
    }
}
