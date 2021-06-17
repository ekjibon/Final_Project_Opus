using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSPromotionAndTransfer.Models;
using OPUSERP.Areas.HRPMSPromotionAndTransfer.Models.Lang;
using OPUSERP.Data.Entity;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Data.Entity.PromotionAndTransfer;
using OPUSERP.HRPMS.Services.PromotionAndTransfer.Interfaces;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSPromotionAndTransfer.Controllers
{
    [Authorize]
    [Area("HRPMSPromotionAndTransfer")]
    public class PromotionEntryController : Controller
    {
        private readonly LangGenerate<PromotionTransferLn> _lang;
        private readonly IPromotionTransferEntryService promotionTransferEntryService;
        private readonly UserManager<ApplicationUser> _userManager;

        public PromotionEntryController(IHostingEnvironment hostingEnvironment, IPromotionTransferEntryService promotionTransferEntryService, UserManager<ApplicationUser> userManager)
        {
            _lang = new LangGenerate<PromotionTransferLn>(hostingEnvironment.ContentRootPath);
            this.promotionTransferEntryService = promotionTransferEntryService;
            _userManager = userManager;
        }

        // GET: PromotionEntry
        public async Task<IActionResult> Index(int id)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string org = user.org;

            PromotionEntryViewModel model = new PromotionEntryViewModel
            {
                fLang = _lang.PerseLang("PromotionTransfer/PromotionEntryEN.json", "PromotionTransfer/PromotionEntryBN.json", Request.Cookies["lang"]),
                promotionEntries = await promotionTransferEntryService.GetPromotionEntryByOrg("ddm"),
                promotionEntry = await promotionTransferEntryService.GetPromotionEntryById(id)
            };
            if (model.promotionEntry == null) model.promotionEntry = new PromotionEntry();

            return View(model);
        }

        // GET: PromotionEntry/Details/5
        public async Task<IActionResult> Details(int id)
        {
            PromotionEntryViewModel model = new PromotionEntryViewModel
            {
                fLang = _lang.PerseLang("PromotionTransfer/PromotionEntryEN.json", "PromotionTransfer/PromotionEntryBN.json", Request.Cookies["lang"]),
                promotionEntries = await promotionTransferEntryService.GetPromotionEntry(),
                promotionEntry = await promotionTransferEntryService.GetPromotionEntryById(id)
            };
            return View(model);
        }

        // GET: PromotionEntry/Create
        public async Task<IActionResult> Approve(int id)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string org = user.org;

            PromotionEntryViewModel model = new PromotionEntryViewModel
            {
                fLang = _lang.PerseLang("PromotionTransfer/PromotionEntryEN.json", "PromotionTransfer/PromotionEntryBN.json", Request.Cookies["lang"]),
                promotionEntries = await promotionTransferEntryService.GetPromotionEntryByStatusAndOrg("pending","ddm"),
                promotionEntry = await promotionTransferEntryService.GetPromotionEntryById(id)
            };
            return View(model);
        }

        // POST: PromotionEntry/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] PromotionEntryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("PromotionTransfer/PromotionEntryEN.json", "PromotionTransfer/PromotionEntryBN.json", Request.Cookies["lang"]);
                model.promotionEntries = await promotionTransferEntryService.GetPromotionEntry();
                return View(model);
            }

            PromotionEntry data = new PromotionEntry
            {
                Id = model.promotionId,
                employeeId = model.id,
                date = model.date,
                payScale =model.payScale,
                nature =model.nature,
                basic =model.basic,
                goNumber = model.goNumber,
                goDate =model.goDate,
                remark =model.remark,
                status = "pending"
            };
            await promotionTransferEntryService.SavePromotionEntry(data);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Action([FromForm] int id, string type)
        {
            if (type == "approve") await promotionTransferEntryService.UpdatePromotionStatus(id, "approved");
            else if (type == "reject") await promotionTransferEntryService.UpdatePromotionStatus(id, "rejected");

            return RedirectToAction(nameof(Index));

        }

    }
}