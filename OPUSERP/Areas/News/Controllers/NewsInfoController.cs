using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.News.Models;
using OPUSERP.Areas.News.Models.Lang;
using OPUSERP.CLUB.Data.News;
using OPUSERP.CLUB.Services.News.Interfaces;
using OPUSERP.Helpers;

namespace OPUSERP.Areas.News.Controllers
{
    [Authorize]
    [Area("News")]
    public class NewsInfoController : Controller
    {
        private readonly INewsInfoService newsInfoService;
        private readonly LangGenerate<NewsLn> _lang;

        public NewsInfoController(IHostingEnvironment hostingEnvironment, INewsInfoService newsInfoService)
        {
            _lang = new LangGenerate<NewsLn>(hostingEnvironment.ContentRootPath);
            this.newsInfoService = newsInfoService;
            _lang = new LangGenerate<NewsLn>(hostingEnvironment.ContentRootPath);
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            NewsInfoViewModel model = new NewsInfoViewModel
            {
                fLang = _lang.PerseLang("News/NewsEN.json", "News/NewsBN.json", Request.Cookies["lang"]),
                newsInfo = await newsInfoService.GetNewsInformationById(id),
            };
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            NewsInfoViewModel model = new NewsInfoViewModel
            {
                fLang = _lang.PerseLang("News/NewsEN.json", "News/NewsBN.json", Request.Cookies["lang"]),
                newsInfos = await newsInfoService.GetNewsInformation(),
            };
            return View(model);
        }

        // POST: NewsInfo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] NewsInfoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("News/NewsEN.json", "News/NewsBN.json", Request.Cookies["lang"]);
                model.newsInfos = await newsInfoService.GetNewsInformation();
                return View(model);
            }

            //return Json(model);

            string fileFullName = string.Empty;

            string fileName = string.Empty;
            string message = "No Image";
            if (model.empPhoto != null)
            {
                message = FileSave.SaveImage(out fileName, model.empPhoto);
            }

            if (message == "success")
            {
                fileFullName = fileName;
            }

            NewsInfo data = new NewsInfo
            {
                Id = model.Id,
                title = model.title,
                description = model.description,
                url = fileFullName,
                date = model.date
            };
            await newsInfoService.SaveNewsInformation(data);
            return RedirectToAction(nameof(Create));
        }

        // GET: NewsInfo/NewsList
        public async Task<IActionResult> NewsList()
        {
            NewsInfoViewModel model = new NewsInfoViewModel
            {
                fLang = _lang.PerseLang("News/NewsEN.json", "News/NewsBN.json", Request.Cookies["lang"]),
                newsInfos =await newsInfoService.GetNewsInformation()
            };
            return View(model);
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            await newsInfoService.DeleteNewsInformationById(id);
            return RedirectToAction(nameof(Create));
        }

    }
}