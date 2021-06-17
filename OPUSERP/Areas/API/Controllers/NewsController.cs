using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.CLUB.Services.News.Interfaces;

namespace OPUSERP.Areas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class NewsController : ControllerBase
    {
        private readonly INewsInfoService newsInfoService;

        public NewsController(INewsInfoService newsInfoService)
        {
            this.newsInfoService = newsInfoService;
        }

        // GET: api/Notice
        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            return new OkObjectResult(await newsInfoService.GetNewsInformation());
        }

        // GET: api/Notice
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return new OkObjectResult(await newsInfoService.GetNewsInformationById(id));
        }

    }
}