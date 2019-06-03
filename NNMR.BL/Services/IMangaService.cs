using CommonLib.Models.DTOModels;
using NNMR.Models.DBModels;

namespace NNMR.BL.Services
{
    public interface IMangaService
    {
        void SaveManga(MangaDTO manga); 
        void SaveMangaInfo(string key, string value, int mangaId);
        ChapterDB SaveMangaChapter(string mangaUrl, MangaChapterDTO chapter);
        ChapterDB SaveMangaChapter(int mangaId, MangaChapterDTO chapter);
        ChapterImageDB SaveChapterImage(MangaChapterDTO chapter, string imageUrl);
        ChapterImageDB SaveChapterImage(int chapterId, string imageUrl);
        MangaDTO GetManga(string url);
        MangaDTO GetManga(int id);
        MangaDTO ProcessManga(MangaDB manga);
    }
}
