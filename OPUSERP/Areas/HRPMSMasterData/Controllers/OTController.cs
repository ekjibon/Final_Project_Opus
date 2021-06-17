using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSMasterData.Models;
using OPUSERP.HRPMS.Data.Entity.Wages;
using OPUSERP.HRPMS.Services.Employee.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OPUSERP.Areas.HRPMSMasterData.Controllers
{
    [Authorize]
    [Area("HRPMSMasterData")]
    public class OTController : Controller
    {
        // GET: /<controller>/
       // private readonly LangGenerate<DistrictLn> _lang;
        private readonly IWagesOTSetupMasterService wagesOTSetupMasterService;

        public OTController(IHostingEnvironment hostingEnvironment, IWagesOTSetupMasterService wagesOTSetupMasterService)
        {
           // _lang = new LangGenerate<DistrictLn>(hostingEnvironment.ContentRootPath);
            this.wagesOTSetupMasterService = wagesOTSetupMasterService;
        }
        // GET: District
        public async Task<IActionResult> Index()
        {
            OTSlotTypeViewModel model = new OTSlotTypeViewModel
            {
               
                oTSlotTypes = await wagesOTSetupMasterService.GetOTSlotType()
            };
            return View(model);
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([FromForm] OTSlotTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
               
                model.oTSlotTypes = await wagesOTSetupMasterService.GetOTSlotType();
                return View(model);
            }

            OTSlotType oTSlotType = new OTSlotType
            {
                Id = (int)model.slotTypeId,
               
                slotName = model.slotName,
                slotHour=model.slotHour
            };

            await wagesOTSetupMasterService.SaveOTSlotType(oTSlotType);

            return RedirectToAction(nameof(Index));

        }
    }
}
