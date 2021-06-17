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
    public class AwardController : Controller
    {
        private readonly LangGenerate<AwardLn> _lang;
        private readonly IAwardPublicationService awardPublicationService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AwardController(IHostingEnvironment hostingEnvironment, IAwardPublicationService awardPublicationService, UserManager<ApplicationUser> userManager)
        {
            this.awardPublicationService = awardPublicationService;
            _lang = new LangGenerate<AwardLn>(hostingEnvironment.ContentRootPath);
            _userManager = userManager;
        }

        // GET: Award
        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string org = user.org;

            var model = new AwardEntryViewModel()
            {
                flang = _lang.PerseLang("AwardPublication/AwardEN.json", "AwardPublication/AwardBN.json", Request.Cookies["lang"]),
                awards = await awardPublicationService.GetAwardByOrg("ddm"),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] AwardEntryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeId = model.employeeId;
                model.flang = _lang.PerseLang("AwardPublication/AwardEN.json", "AwardPublication/AwardBN.json", Request.Cookies["lang"]);
                model.awards = await awardPublicationService.GetAwardByOrg("ddm");
                return View(model);
            }

            AwardEntry data = new AwardEntry
            {
                Id = model.awardId,
                employeeId = model.employeeId,
                awardName = model.awardName,
                awardDate = Convert.ToDateTime(model.awardDate),
                purpose = model.purpose,
                status = "Pending"
            };

            await awardPublicationService.SaveAward(data);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AwardApprove()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string org = user.org;

            var model = new AwardEntryViewModel()
            {
                flang = _lang.PerseLang("AwardPublication/AwardEN.json", "AwardPublication/AwardBN.json", Request.Cookies["lang"]),
                awards = await awardPublicationService.GetAwardEntryByStatusAndOrg("Pending","ddm"),
            };
            return View(model);
        }
        
        public async Task<ActionResult> Details(int id)
        {
            var model = new AwardEntryViewModel
            {
                flang = _lang.PerseLang("AwardPublication/AwardEN.json", "AwardPublication/AwardBN.json", Request.Cookies["lang"]),
                award = await awardPublicationService.GetAwardById(id),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Action([FromForm] int id, string type)
        {
            if (type == "approve") await awardPublicationService.UpdateAwardStatus(id, "approved");
            else if (type == "reject") await awardPublicationService.UpdateAwardStatus(id, "rejected");

            return RedirectToAction(nameof(Index));

        }

    }
}