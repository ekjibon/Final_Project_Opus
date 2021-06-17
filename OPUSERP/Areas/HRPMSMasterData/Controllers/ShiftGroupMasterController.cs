using OPUSERP.Areas.HRPMSMasterData.Models;
using OPUSERP.Areas.HRPMSMasterData.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;



namespace OPUSERP.Areas.HRPMSMasterData.Controllers
{
    [Authorize]
    [Area("HRPMSMasterData")]
    public class ShiftGroupMasterController : Controller
    {
        private readonly LangGenerate<ShiftGroupLn> _lang;
        private readonly IShiftGroupMasterService shiftGroupMasterService;

        public ShiftGroupMasterController(IHostingEnvironment hostingEnvironment, IShiftGroupMasterService shiftGroupMasterService)
        {
            _lang = new LangGenerate<ShiftGroupLn>(hostingEnvironment.ContentRootPath);
            this.shiftGroupMasterService = shiftGroupMasterService;
        }

        // GET: ShiftGroupMaster
        public async Task<ActionResult> Index()
        {
            ShiftGroupMasterViewModel model = new ShiftGroupMasterViewModel
            {
                fLang = _lang.PerseLang("MasterData/ShiftGroupMasterEN.json", "MasterData/ShiftGroupMasterBN.json", Request.Cookies["lang"]),
                shiftGroupMasterlist = await shiftGroupMasterService.GetAllShiftGroupMaster()
            };
            return View(model);
        }

        // POST: ShiftGroupMaster/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ShiftGroupMasterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("MasterData/ShiftGroupMasterEN.json", "MasterData/ShiftGroupMasterBN.json", Request.Cookies["lang"]);
                model.shiftGroupMasterlist = await shiftGroupMasterService.GetAllShiftGroupMaster();
                return View(model);
            }

            ShiftGroupMaster data = new ShiftGroupMaster
            {
                Id = model.masterId,
                shiftName = model.shiftName,
                shiftNameBn = model.shiftNameBn,
               
            };

            await shiftGroupMasterService.SaveShiftGroupMaster(data);

            return RedirectToAction(nameof(Index));
        }


    }
}