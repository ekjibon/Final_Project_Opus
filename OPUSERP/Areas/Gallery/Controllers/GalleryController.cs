using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Gallery.Models;
using OPUSERP.Areas.Gallery.Models.Lang;
using OPUSERP.CLUB.Data.Gallery;
using OPUSERP.CLUB.Services.Gallery.Interface;
using OPUSERP.Helpers;

namespace OPUSERP.Areas.Gallery.Controllers
{
    [Area("Gallery")]
    [Authorize]
    public class GalleryController : Controller
    {
        private readonly LangGenerate<GalleryLn> _lang;
        private readonly IGalleryTitleService galleryTitleService;
        private readonly IGalleryContentService galleryContentService;

        public GalleryController(IHostingEnvironment hostingEnvironment, IGalleryTitleService galleryTitleService, IGalleryContentService galleryContentService)
        {
            _lang = new LangGenerate<GalleryLn>(hostingEnvironment.ContentRootPath);
            this.galleryTitleService = galleryTitleService;
            this.galleryContentService = galleryContentService;
        }


        // GET: Gallery
        public async Task<IActionResult> Index()
        {
            GalleryTitleViewModel model = new GalleryTitleViewModel
            {
                fLang = _lang.PerseLang("Gallery/GalleryEN.json", "Gallery/GalleryBN.json", Request.Cookies["lang"]),
                galleryTitles = await galleryTitleService.GetGalleryTitle(),
            };
            return View(model);
        }

        // Delete: Children
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            await galleryTitleService.DeleteGalleryTitleById(id);
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Index");
        }

        // GET: Gallery
        public async Task<IActionResult> ImageGallery()
        {
            ContentViewModel model = new ContentViewModel
            {
                fLang = _lang.PerseLang("Gallery/GalleryEN.json", "Gallery/GalleryBN.json", Request.Cookies["lang"]),
                galleryTitles = await galleryTitleService.GetGalleryTitleByType(1),
            };
            return View(model);
        }

        // GET: Gallery
        public async Task<IActionResult> VideoGallery()
        {
            ContentViewModel model = new ContentViewModel
            {
                fLang = _lang.PerseLang("Gallery/GalleryEN.json", "Gallery/GalleryBN.json", Request.Cookies["lang"]),
                galleryTitles = await galleryTitleService.GetGalleryTitleByType(0),
            };
            return View(model);
        }

        // GET: Gallery
        public async Task<IActionResult> Image()
        {
            ContentViewModel model = new ContentViewModel
            {
                fLang = _lang.PerseLang("Gallery/GalleryEN.json", "Gallery/GalleryBN.json", Request.Cookies["lang"]),
                galleryTitles = await galleryTitleService.GetGalleryTitleByType(1),
                galleryContents = await galleryContentService.GetGalleryContentByType(1),
            };
            return View(model);
        }

        // POST: NewsInfo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Image([FromForm] ContentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("Gallery/GalleryEN.json", "Gallery/GalleryBN.json", Request.Cookies["lang"]);
                model.galleryTitles = await galleryTitleService.GetGalleryTitleByType(1);
                model.galleryContents = await galleryContentService.GetGalleryContentByType(1);
                return View(model);
            }

            string fileFullName;

            string fileName;
            string message = FileSave.SaveImage(out fileName, model.empPhoto);

            if (message == "success")
            {
                fileFullName = fileName;
            }
            else
            {
                model.fLang = _lang.PerseLang("Gallery/GalleryEN.json", "Gallery/GalleryBN.json", Request.Cookies["lang"]);
                model.galleryTitles = await galleryTitleService.GetGalleryTitleByType(1);
                model.galleryContents = await galleryContentService.GetGalleryContentByType(1);
                ModelState.AddModelError(string.Empty, message);
                return View(model);
                //fileFullName = null;
            }

            GalleryContent data = new GalleryContent
            {
                Id = 0,
                galleryTitleId = model.galleryTitleId,
                caption = model.caption,
                url = fileFullName
            };
            await galleryContentService.SaveGalleryContent(data);
            return RedirectToAction(nameof(Image));
        }
        
        // GET: Gallery
        public async Task<IActionResult> Video()
        {
            ContentViewModel model = new ContentViewModel
            {
                fLang = _lang.PerseLang("Gallery/GalleryEN.json", "Gallery/GalleryBN.json", Request.Cookies["lang"]),
                galleryTitles = await galleryTitleService.GetGalleryTitleByType(0),
                galleryContents = await galleryContentService.GetGalleryContentByType(0),
            };
            return View(model);
        }

        // POST: NewsInfo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Video([FromForm] ContentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("Gallery/GalleryEN.json", "Gallery/GalleryBN.json", Request.Cookies["lang"]);
                return View(model);
            }

            GalleryContent data = new GalleryContent
            {
                Id = 0,
                galleryTitleId = model.galleryTitleId,
                caption = model.caption,
                url = model.videoId,
            };
            await galleryContentService.SaveGalleryContent(data);
            return RedirectToAction(nameof(Video));
        }
        
        public async Task<IActionResult> Delete(int id, int type)
        {
            await galleryContentService.DeleteGalleryContentById(id);
            if (type == 1)
            {
                return RedirectToAction(nameof(Video));
            }
            else
            {
                return RedirectToAction(nameof(Image));
            }
        }
        
        // GET: Gallery/Details/5
        public async Task<IActionResult> GalleryDetails(int id)
        {
            ContentViewModel model = new ContentViewModel
            {
                fLang = _lang.PerseLang("Gallery/GalleryEN.json", "Gallery/GalleryBN.json", Request.Cookies["lang"]),
                galleryContents = await galleryContentService.GetGalleryContentByTitleId(id),
            };
            return View(model);
        }

        // GET: Gallery/Details/5
        public async Task<IActionResult> VideoDetails(int id)
        {
            ContentViewModel model = new ContentViewModel
            {
                fLang = _lang.PerseLang("Gallery/GalleryEN.json", "Gallery/GalleryBN.json", Request.Cookies["lang"]),
                galleryContents = await galleryContentService.GetGalleryContentByTitleId(id),
            };
            return View(model);
        }

        // GET: Gallery/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Gallery/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] GalleryTitleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("Gallery/GalleryEN.json", "Gallery/GalleryBN.json", Request.Cookies["lang"]);
                model.galleryTitles = await galleryTitleService.GetGalleryTitle();
                return View(model);
            }

            //return Json(model);

            GalleryTitle data = new GalleryTitle
            {
                Id = model.Id,
                title = model.title,
                type = model.type,
                description = model.Description,
                date = DateTime.Now,
            };
            await galleryTitleService.SaveGalleryTitle(data);
            return RedirectToAction(nameof(Index));
        }

        // GET: Gallery/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Gallery/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}