using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.CLUB.Services.Gallery.Interface;

namespace OPUSERP.Areas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GalleryController : ControllerBase
    {
        private readonly IGalleryTitleService galleryTitleService;
        private readonly IGalleryContentService galleryContentService;

        public GalleryController(IGalleryTitleService galleryTitleService, IGalleryContentService galleryContentService)
        {
            this.galleryTitleService = galleryTitleService;
            this.galleryContentService = galleryContentService;
        }

        // GET: api/Notice
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return new OkObjectResult(await galleryTitleService.GetGalleryTitleByType(id));
        }
        
        // GET: api/Notice
        [HttpGet("GetGalleryContent/{id}")]
        public async Task<IActionResult> GetGalleryContent(int id)
        {
            return new OkObjectResult(await galleryContentService.GetGalleryContentByTitleId(id));
        }
    }
}