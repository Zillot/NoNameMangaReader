using System.Threading.Tasks;
using CommonLib.Models.DTOModels;
using Microsoft.AspNetCore.Mvc;
using NNMR.BL.Services;

namespace WebParser.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class MangaController : ControllerBase
    {
        private IMangaService _mangaService { get; set; }

        public MangaController(IMangaService mangaService)
        {
            _mangaService = mangaService;
        }

        [HttpPost]
        public async Task SaveManga([FromBody]MangaDTO manga)
        {
            _mangaService.SaveManga(manga);
        }

        [HttpPost]
        public async Task SaveMangaChapter(string mangaUrl, [FromBody]MangaChapterDTO chapter)
        {
            _mangaService.SaveMangaChapter(mangaUrl, chapter);
        }

        [HttpGet]
        public async Task<MangaDTO> GetManga(string url)
        {
            return _mangaService.GetManga(url);
        }

        [HttpGet]
        public async Task<MangaDTO> GetMangaById(int id)
        {
            return _mangaService.GetManga(id);
        }
    }
}
