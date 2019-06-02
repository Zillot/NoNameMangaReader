using CommonLib.Models.DTOModels;
using CommonLib.Models.Exeptions;
using CommonLib.Services;
using NNMR.DL.Repositories;
using NNMR.Models.DBModels;

namespace NNMR.BL.Services
{
    public class MangaService
    {
        private IDummyNetworkService _dummyNetworkService { get; set; }

        private IMangaRepository _mangaRepository { get; set; }
        private IChapterRepository _chapterRepository { get; set; }
        private IChapterImageRepository _chapterImageRepository { get; set; }
        private IMangaInfoRepository _mangaInfoRepository { get; set; }

        public MangaService(
            IDummyNetworkService dummyNetworkService,

            IMangaRepository mangaRepository,
            IChapterRepository chapterRepository,
            IChapterImageRepository chapterImageRepository,
            IMangaInfoRepository mangaInfoRepository)
        {
            _dummyNetworkService = dummyNetworkService;

            _mangaRepository = mangaRepository;
            _chapterRepository = chapterRepository;
            _chapterImageRepository = chapterImageRepository;
            _mangaInfoRepository = mangaInfoRepository;

            //TODO move it to some other place and use balancer
            _dummyNetworkService.SetBaseUri("http://localhost:51005/");
        }

        public void SaveManga(MangaDTO manga)
        {
            var mangaDb = new MangaDB()
            {
                NameENG = manga.NameEng,
                ProviderId = manga.ProviderId,
                URL = manga.URL
            };

            _mangaRepository.Add(mangaDb);

            AddInfo(manga.NameOrg, "1", mangaDb.Id);
            AddInfo(manga.NameRus, "1", mangaDb.Id);
            AddInfo(manga.Author, "1", mangaDb.Id);
            AddInfo(manga.Categories, "1", mangaDb.Id);
            AddInfo(manga.Description, "1", mangaDb.Id);
            AddInfo(manga.Genre, "1", mangaDb.Id);
            AddInfo(manga.Magazines, "1", mangaDb.Id);
            AddInfo(manga.PosterUrl, "1", mangaDb.Id);
            AddInfo(manga.Publisher, "1", mangaDb.Id);
            AddInfo(manga.PushlishYear, "1", mangaDb.Id);
            AddInfo(manga.Score.ToString(), "1", mangaDb.Id);
            AddInfo(manga.State, "1", mangaDb.Id);
            AddInfo(manga.Translators, "1", mangaDb.Id);
            AddInfo(manga.Volumes, "1", mangaDb.Id);

            foreach (var chapter in manga.Chapters)
            {
                var chapterDb = new ChapterDB()
                {
                    MangaId = mangaDb.Id,
                    Name = chapter.Name,
                    URL = chapter.Url,
                };

                _chapterRepository.Add(chapterDb);

                foreach (var chapterImg in chapter.Urls)
                {
                    var chapterImgDb = new ChapterImageDB()
                    {
                        ChapterId = chapterDb.Id,
                        URL = chapterImg
                    };

                    _chapterImageRepository.Add(chapterImgDb);
                }
            }
        }

        public void AddInfo(string key, string value, int mangaId)
        {
            var mangaInfoDb = new MangaInfoDB()
            {
                Key = key,
                Value = value,
                MangaId = mangaId
            };

            _mangaInfoRepository.Add(mangaInfoDb);
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
            return MangaDB.FromDB(manga);
        }
    }
}
