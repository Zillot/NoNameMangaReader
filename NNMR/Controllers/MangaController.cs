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
        private MangaService _mangaService { get; set; }

        public MangaController(MangaService mangaService)
        {
            _mangaService = mangaService;
        }

        [HttpPost]
        public async Task SaveManga(MangaDTO manga)
        {
            _mangaService.SaveManga(manga);
        }

        [HttpGet]
        public async Task GetManga(string url)
        {
            _mangaService.GetManga(url);
        }
    }
}
