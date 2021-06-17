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
    public class TransferEntryController : Controller
    {
        private readonly LangGenerate<PromotionTransferLn> _lang;
        private readonly IPromotionTransferEntryService promotionTransferEntryService;
        private readonly UserManager<ApplicationUser> _userManager;

        public TransferEntryController(IHostingEnvironment hostingEnvironment, IPromotionTransferEntryService promotionTransferEntryService, UserManager<ApplicationUser> userManager)
        {
            this.promotionTransferEntryService = promotionTransferEntryService;
            _lang = new LangGenerate<PromotionTransferLn>(hostingEnvironment.ContentRootPath);
            _userManager = userManager;
        }

        // GET: TransferEntry
        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string org = user.org;

            TransferEntryViewModel model = new TransferEntryViewModel
            {
                fLang = _lang.PerseLang("PromotionTransfer/TransferEntryEN.json", "PromotionTransfer/TransferEntryBN.json", Request.Cookies["lang"]),
                transferEntries = await promotionTransferEntryService.GetTransferEntryByOrg("ddm")
            };
            return View(model);
        }

        // POST: TransferEntry/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] TransferEntryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("PromotionTransfer/TransferEntryEN.json", "PromotionTransfer/TransferEntryBN.json", Request.Cookies["lang"]);
                model.transferEntries = await promotionTransferEntryService.GetTransferEntry();
                return View(model);
            }

            TransferEntry data = new TransferEntry
            {
                Id = model.transfarId,
                employeeId = model.id,
                currentDesignation = model.currentDesignation,
                currentPlace = model.currentPlace,
                currentGrade = model.currentGrade,
                newDesignation = model.newDesignation,
                newPlace = model.newPlace,
                newGrade = model.newGrade,
                orderDate = model.orderDate,
                effectDate = model.effectDate,
                joiningDate = model.joiningDate,
                remark = model.remark,
                status = "Pending"
            };

            await promotionTransferEntryService.SaveTransferEntry(data);

            return RedirectToAction(nameof(Index));
        }

        // GET: TransferEntry/Details/5
        public async Task<IActionResult> Details(int id)
        {
            TransferEntryViewModel model = new TransferEntryViewModel
            {
                fLang = _lang.PerseLang("PromotionTransfer/TransferEntryEN.json", "PromotionTransfer/TransferEntryBN.json", Request.Cookies["lang"]),
                transferEntrie = await promotionTransferEntryService.GetTransferEntryById(id)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Action([FromForm] int id, string type)
        {
            if (type == "approve") await promotionTransferEntryService.UpdateTransferStatus(id, "approved");
            else if (type == "reject") await promotionTransferEntryService.UpdateTransferStatus(id, "rejected");

            return RedirectToAction(nameof(Index));

        }

        // GET: TransferEntry/Create
        public async Task<IActionResult> Approve()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string org = user.org;

            TransferEntryViewModel model = new TransferEntryViewModel
            {
                fLang = _lang.PerseLang("PromotionTransfer/TransferEntryEN.json", "PromotionTransfer/TransferEntryBN.json", Request.Cookies["lang"]),
                transferEntries = await promotionTransferEntryService.GetTransferEntryByStatusByOrg("Pending", "ddm")
            };
            return View(model);
        }


    }
}