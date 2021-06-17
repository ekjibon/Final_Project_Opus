using System.Threading.Tasks;
using OPUSERP.Areas.HRPMSMasterData.Models;
using OPUSERP.Areas.HRPMSMasterData.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace OPUSERP.Areas.HRPMSMasterData.Controllers
{
    [Authorize]
    [Area("HRPMSMasterData")]
    public class TravelPurposeController : Controller
    {
        private readonly LangGenerate<TravelPurposeLn> _lang;
        private readonly ITravelService travelService;

        public TravelPurposeController(IHostingEnvironment hostingEnvironment, ITravelService travelService)
        {
            _lang = new LangGenerate<TravelPurposeLn>(hostingEnvironment.ContentRootPath);
            this.travelService = travelService;
        }
        // GET: TravelPurpose
        public async Task<IActionResult> Index()
        {
            TravelPurposeViewModel model = new TravelPurposeViewModel
            {
                fLang = _lang.PerseLang("MasterData/TravelPurposeEN.json", "MasterData/TravelPurposeBN.json", Request.Cookies["lang"]),
                travelPurposes = await travelService.GetTravelPurposes()
            };
            return View(model);
        }

        // POST: TravelPurpose/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] TravelPurposeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("MasterData/TravelPurposeEN.json", "MasterData/TravelPurposeBN.json", Request.Cookies["lang"]);
                model.travelPurposes = await travelService.GetTravelPurposes();
                return View(model);
            }

            TravelPurpose data = new TravelPurpose
            {
                Id= model.travelPurposeId,
                purposeName = model.purposeName,
                purposeNameBn = model.purposeNameBn,
                purposeShortName = model.purposeShortName
            };

            await travelService.SaveTravelPurpose(data);

            return RedirectToAction(nameof(Index));
        }

    }
}