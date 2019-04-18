using CommonLib.Models.Exeptions;
using CommonLib.Services;
using NNMR.DL.Repositories;
using NNMR.Model.DMModels;

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

        public DMManga GetManga(string url)
        {
            var manga = _mangaRepository.FirstOrDefault(x => x.Url == url);
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

        public DMManga GetManga(int id)
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

        public DMManga ProcessManga(DMManga manga)
        {
            return null;
        }
    }
}
