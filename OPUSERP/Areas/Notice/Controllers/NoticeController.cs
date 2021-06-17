using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Notice.Models;
using OPUSERP.Areas.Notice.Models.Lang;
using OPUSERP.CLUB.Data.Notice;
using OPUSERP.CLUB.Services.Notice.Interfaces;
using OPUSERP.Helpers;

namespace OPUSERP.Areas.Notice.Controllers
{
    [Authorize]
    [Area("Notice")]
    public class NoticeController : Controller
    {
        private readonly INoticeInfoService noticeInfoService;
        private readonly INoticeTypeService noticeTypeService;
        private readonly INoticeAuthorService noticeAuthorService;
        private readonly IRlnNoticeAuthorService rlnNoticeAuthorService;
        private readonly LangGenerate<NoticeLn> _lang;

        private readonly string rootPath;
        private readonly MyPDF myPDF;

        public NoticeController(IHostingEnvironment hostingEnvironment, INoticeInfoService noticeInfoService, INoticeTypeService noticeTypeService, IConverter converter, INoticeAuthorService noticeAuthorService, IRlnNoticeAuthorService rlnNoticeAuthorService)
        {
            _lang = new LangGenerate<NoticeLn>(hostingEnvironment.ContentRootPath);
            this.noticeInfoService = noticeInfoService;
            this.noticeTypeService = noticeTypeService;
            this.noticeAuthorService = noticeAuthorService;
            this.rlnNoticeAuthorService = rlnNoticeAuthorService;

            //For PDF
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        public async Task<ActionResult> Index()
        {
            NoticeAithorViewModel model = new NoticeAithorViewModel
            {
                noticeAuthors = await noticeAuthorService.GetNoticeAuthor()
            };
            return View(model);
        }

        // POST: NewsInfo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([FromForm] NoticeAithorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //model.fLang = _lang.PerseLang("News/NewsEN.json", "News/NewsBN.json", Request.Cookies["lang"]);
                //model.newsInfos = await newsInfoService.GetNewsInformation();
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

            NoticeAuthor data = new NoticeAuthor
            {
                Id = model.Id,
                name = model.memberName,
                designation = model.designation,
                signature = fileFullName
            };
            await noticeAuthorService.SaveNoticeAuthor(data);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Details(int id)
        {
            NoticeInfoViewModel model = new NoticeInfoViewModel
            {
                Id = id,
                fLang = _lang.PerseLang("Notice/NoticeEN.json", "Notice/NoticeBN.json", Request.Cookies["lang"]),
                noticeInfo = await noticeInfoService.GetNoticeInformationById(id),
            };


            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> NoticeView(int id)
        {
            NoticeInfoViewModel model = new NoticeInfoViewModel
            {
                fLang = _lang.PerseLang("Notice/NoticeEN.json", "Notice/NoticeBN.json", Request.Cookies["lang"]),
                noticeInfo = await noticeInfoService.GetNoticeInformationById(id),
                rlnNoticeAuthors = await rlnNoticeAuthorService.GetRlnNoticeAuthorByNoticeId(id),
            };
            return View(model);
        }

        //PrintPDF
        [AllowAnonymous]
        public IActionResult pdspdf(int id)
        {
            string fileName;
            string status = myPDF.GeneratePDF(out fileName, $"Notice/Notice/NoticeView/{id}");

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await noticeInfoService.DeleteNoticeInformationById(id);
            return RedirectToAction(nameof(Create));
        }

        public async Task<IActionResult> DeleteAuthor(int id)
        {
            await noticeAuthorService.DeleteNoticeAuthorById(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Create()
        {
            NoticeInfoViewModel model = new NoticeInfoViewModel
            {
                fLang = _lang.PerseLang("Notice/NoticeEN.json", "Notice/NoticeBN.json", Request.Cookies["lang"]),
                noticeInfos = await noticeInfoService.GetNoticeInformation(),
                noticeTypes = await noticeTypeService.GetAllNoticeType(),
                noticeAuthors = await noticeAuthorService.GetNoticeAuthor()
            };
            return View(model);
        }

        // POST: NoticeInfo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] NoticeInfoViewModel model)
        {

            //return Json(model);

            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("Notice/NoticeEN.json", "Notice/NoticeBN.json", Request.Cookies["lang"]);
                model.noticeInfos = await noticeInfoService.GetNoticeInformation();
                model.noticeTypes = await noticeTypeService.GetAllNoticeType();
                model.noticeAuthors = await noticeAuthorService.GetNoticeAuthor();
                return View(model);
            }
            
            string fileName = string.Empty;
            string message = string.Empty;

            if (model.attachment != null)
            {
                message = FileSave.SaveFile(out fileName, model.attachment, "Notice");
            }

            NoticeInfo data = new NoticeInfo
            {
                Id = Convert.ToInt32(model.Id),
                subject = model.subject,
                notice = model.notice,
                noticeType = model.noticeType,
                status = 0,
                url = fileName,
                date = DateTime.Now
            };
            int id = await noticeInfoService.SaveNoticeInformation(data);
            if (model.author.Count() > 0)
            {
                foreach (var data1 in model.author)
                {
                    RlnNoticeAuthor data2 = new RlnNoticeAuthor
                    {
                        noticeAuthorId = data1,
                        noticeInfoId = id
                    };
                    await rlnNoticeAuthorService.SaveRlnNoticeAuthor(data2);
                }

            }
            return RedirectToAction(nameof(Create));
        }

        #region API Section

        [AllowAnonymous]
        [Route("global/api/getAllNotice")]
        [HttpGet]
        public async Task<IActionResult> getAllNotice()
        {            
            return Json(await noticeInfoService.GetNoticeInformation());
        }

        #endregion
    }
}