using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSAwardPublication.Models;
using OPUSERP.Areas.HRPMSAwardPublication.Models.Lang;
using OPUSERP.Data.Entity;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Data.Entity.AwardPublication;
using OPUSERP.HRPMS.Services.AwardPublication.Interfaces;
using System;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSAwardPublication.Controllers
{
    [Authorize]
    [Area("HRPMSAwardPublication")]
    public class PublicationController : Controller
    {
        private readonly LangGenerate<PublicationLn> _lang;
        private readonly IAwardPublicationService awardPublicationService;
        private readonly UserManager<ApplicationUser> _userManager;

        public PublicationController(IHostingEnvironment hostingEnvironment, IAwardPublicationService awardPublicationService, UserManager<ApplicationUser> userManager)
        {
            this.awardPublicationService = awardPublicationService;
            _lang = new LangGenerate<PublicationLn>(hostingEnvironment.ContentRootPath);
            _userManager = userManager;
        }

        // GET: Publication
        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string org = user.org;

            var model = new PublicationEntryViewModel()
            {
                fLang = _lang.PerseLang("AwardPublication/PublicationEN.json", "AwardPublication/PublicationBN.json", Request.Cookies["lang"]),
                publicationEntries = await awardPublicationService.GetPublicationByOrg("ddm")
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] PublicationEntryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeId = model.employeeId;
                model.fLang = _lang.PerseLang("AwardPublication/PublicationEN.json", "AwardPublication/PublicationBN.json", Request.Cookies["lang"]);
                model.publicationEntries = await awardPublicationService.GetPublicationByOrg("ddm");
                return View(model);
            }

            //return Json(model);

            PublicationEntry data = new PublicationEntry
            {
                Id = model.publicationId,
                employeeId = model.employeeId,
                nameOfPublication = model.nameOfPublication,
                Date = Convert.ToDateTime(model.Date),
                place = model.place,
                publicationType = model.publicationType,
                status = "Pending"
            };

            await awardPublicationService.SavePublication(data);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> PublicationApprove()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string org = user.org;

            var model = new PublicationEntryViewModel()
            {
                fLang = _lang.PerseLang("AwardPublication/PublicationEN.json", "AwardPublication/PublicationBN.json", Request.Cookies["lang"]),
                publicationEntries = await awardPublicationService.GetPublicationEntryByStatusByOrg("Pending", "ddm")
            };
            return View(model);
        }

        // GET: Publication/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var model = new PublicationEntryViewModel()
            {
                fLang = _lang.PerseLang("AwardPublication/PublicationEN.json", "AwardPublication/PublicationBN.json", Request.Cookies["lang"]),
                publicationEntrie = await awardPublicationService.GetPublicationById(id)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Action([FromForm] int id, string type)
        {
            if (type == "approve") await awardPublicationService.UpdatePublicationStatus(id, "approved");
            else if (type == "reject") await awardPublicationService.UpdatePublicationStatus(id, "rejected");

            return RedirectToAction(nameof(Index));

        }


    }
}