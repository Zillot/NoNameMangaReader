using CommonLib.Models.DTOModels;
using CommonLib.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RESTAPI.Controllers
{
    public class MangaController : ControllerBase
    {
        private IDummyNetworkService _dummyNetworkService { get; set; }

        public MangaController(IDummyNetworkService dummyNetworkService)
        {
            _dummyNetworkService = dummyNetworkService;

            //TODO move it to some other place and use balancer
            _dummyNetworkService.SetBaseUri("http://localhost:51004/");
        }

        [HttpGet]
        public async Task<string> GetManga(string url)
        {
            return await _dummyNetworkService.Get("manga/GetManga", new Dictionary<string, string>()
            {
                { "url", url }
            });
        }

        [HttpGet]
        public async Task<string> GetMangaById(int id)
        {
            return await _dummyNetworkService.Get("manga/GetMangaById", new Dictionary<string, string>()
            {
                { "id", id.ToString() }
            });
        }
    }
}
