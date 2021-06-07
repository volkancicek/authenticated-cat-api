using System.Threading.Tasks;
using CAAS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CAAS.Controllers
{
    [Authorize]
    [ApiController]
    [Route("cat")]
    public class CatController : ControllerBase
    {
        private readonly ICatService _catService;

        public CatController(ICatService catService)
        {
            _catService = catService;
        }

        [HttpGet]
        [Route("random")]
        public async Task<IActionResult> Get()
        {
            var image = await _catService.GetRandomCatImage();
            return File(image, "image/jpeg");
        }
    }
}
