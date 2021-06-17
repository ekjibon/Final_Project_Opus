using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Program.Models;
using OPUSERP.Programs.Data.Entity;
using OPUSERP.Programs.Service.Interface;

namespace OPUSERP.Areas.Program.Controllers
{
    [Authorize]
    [Area("Program")]
    public class VideosController : Controller
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IProgramVideoService programVideoService;

        public VideosController(IHostingEnvironment hostingEnvironment, IProgramVideoService programVideoService)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.programVideoService = programVideoService;
        }

        public async Task<IActionResult> Index()
        {
            ProgramVideoViewModel model = new ProgramVideoViewModel
            {
                programViews = await programVideoService.GetProgramVideo()
            };
            return View(model);
        }

        public async Task<IActionResult> SingleVideo(int id)
        {
            ProgramVideoViewModel model = new ProgramVideoViewModel
            {
                programVideo = await programVideoService.GetProgramVideoById(id)
            };
            return View(model);
        }

        public async Task<IActionResult> UploadVideo()
        {
            ProgramVideoViewModel model = new ProgramVideoViewModel
            {
                programViews = await programVideoService.GetProgramVideo()
            };
            return View(model);
        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 409715200)]
        [RequestSizeLimit(409715200)]
        public async Task<IActionResult> UploadVideo([FromForm] string title, IFormFile file)
        {
            string fileName = $"{hostingEnvironment.WebRootPath}\\UploadVideo\\{file.FileName}";
            using (FileStream fileStream = System.IO.File.Create(fileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }

            string saveFileName = Path.Combine("UploadVideo", file.FileName);

            ProgramVideo programVideo = new ProgramVideo
            {
                title = title,
                url = saveFileName
            };

            await programVideoService.SaveProgramVideo(programVideo);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await programVideoService.DeleteProgramVideoById(id);
                return RedirectToAction(nameof(UploadVideo));
            }
            catch
            {
                return RedirectToAction(nameof(UploadVideo));
            }
        }

        [Route("global/api/GetProgramVideo/")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetProgramVideo()
        {
            return Json(await programVideoService.GetProgramVideo());
        }
    }
}