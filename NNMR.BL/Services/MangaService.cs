using Auth.Model.Exceptions;
using CommonLib.Models.DTOModels;
using CommonLib.Models.Exeptions;
using CommonLib.Services;
using NNMR.DL.Repositories;
using NNMR.Models.DBModels;
using System.Threading.Tasks;

namespace NNMR.BL.Services
{
    public class MangaService: IMangaService
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

            SaveMangaInfo(manga.NameOrg, "NameOrg", mangaDb.Id);
            SaveMangaInfo(manga.NameRus, "NameRus", mangaDb.Id);
            SaveMangaInfo(manga.Author, "Author", mangaDb.Id);
            SaveMangaInfo(manga.Categories, "Categories", mangaDb.Id);
            SaveMangaInfo(manga.Description, "Description", mangaDb.Id);
            SaveMangaInfo(manga.Genre, "Genre", mangaDb.Id);
            SaveMangaInfo(manga.Magazines, "Magazines", mangaDb.Id);
            SaveMangaInfo(manga.PosterUrl, "PosterUrl", mangaDb.Id);
            SaveMangaInfo(manga.Publisher, "Publisher", mangaDb.Id);
            SaveMangaInfo(manga.PushlishYear, "PushlishYear", mangaDb.Id);
            SaveMangaInfo(manga.Score.ToString(), "Score", mangaDb.Id);
            SaveMangaInfo(manga.State, "State", mangaDb.Id);
            SaveMangaInfo(manga.Translators, "Translators", mangaDb.Id);
            SaveMangaInfo(manga.Volumes, "Volumes", mangaDb.Id);

            foreach (var chapter in manga.Chapters)
            {
                var chapterDb = SaveMangaChapter(mangaDb.Id, chapter);

                foreach (var chapterImg in chapter.URLs)
                {
                    SaveChapterImage(chapterDb.Id, chapterImg);
                }
            }
        }

        public void SaveMangaInfo(string key, string value, int mangaId)
        {
            var mangaInfoDb = new MangaInfoDB()
            {
                Key = key,
                Value = value,
                MangaId = mangaId
            };

            _mangaInfoRepository.Add(mangaInfoDb);
        }

        public ChapterDB SaveMangaChapter(string mangaUrl, MangaChapterDTO chapter)
        {
            var mangaDb = _mangaRepository.FirstOrDefault(x => x.URL == mangaUrl);

            return SaveMangaChapter(mangaDb.Id, chapter);
        }

        public ChapterDB SaveMangaChapter(int mangaId, MangaChapterDTO chapter)
        {
            var chapterDb = new ChapterDB()
            {
                MangaId = mangaId,
                Name = chapter.Name,
                URL = chapter.URL,
            };

            _chapterRepository.Add(chapterDb);
            return chapterDb;
        }

        public ChapterImageDB SaveChapterImage(MangaChapterDTO chapter, string imageUrl)
        {
            var chapterId = _chapterRepository.FirstOrDefault(x => x.URL == chapter.URL);

            return SaveChapterImage(chapterId.Id, imageUrl);
        }

        public ChapterImageDB SaveChapterImage(int chapterId, string imageUrl)
        {
            var chapterImgDb = new ChapterImageDB()
            {
                ChapterId = chapterId,
                URL = imageUrl
            };

            _chapterImageRepository.Add(chapterImgDb);
            return chapterImgDb;
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
                Task.Run(() => _dummyNetworkService.Get("ParseOrders/ProccessManga", new System.Collections.Generic.Dictionary<string, string>()
                {
                    { "url", url }
                })).Wait();

                throw new MangaQueuedException();
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
