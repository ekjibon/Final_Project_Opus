using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSAttendence.Models;
using OPUSERP.HRPMS.Data.Entity.Wages;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OPUSERP.Areas.HRPMSAttendence.Controllers
{
    [Authorize]
    [Area("HRPMSAttendence")]
    public class OTSetupController : Controller
    {
        private readonly IWagesOTSetupMasterService wagesOTSetupMasterService;
        private readonly IShiftGroupMasterService shiftGroupMasterService;

        public OTSetupController( IWagesOTSetupMasterService wagesOTSetupMasterService, IShiftGroupMasterService shiftGroupMasterService)
        {
            // _lang = new LangGenerate<DistrictLn>(hostingEnvironment.ContentRootPath);
            this.wagesOTSetupMasterService = wagesOTSetupMasterService;
            this.shiftGroupMasterService = shiftGroupMasterService;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            OTSetaupViewModel model = new OTSetaupViewModel
            {

                oTSlotType = await wagesOTSetupMasterService.GetOTSlotType(),
               oTSetupMasters=await wagesOTSetupMasterService.GetOTSetupMaster(),
               shiftGroupMasters=await shiftGroupMasterService.GetAllShiftGroupMaster()
            };
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] OTSetaupViewModel model)
        {
           
            string username = HttpContext.User.Identity.Name;
          
            OTSetupMaster data = new OTSetupMaster
            {
                Id = (int)model.oTSetupMasterId,
                bufferingmin = model.bufferingmin,
                shiftGroupMasterId=model.shiftGroupMasterId,
             
                createdBy = HttpContext.User.Identity.Name,
                createdAt = DateTime.Now
            };

            int masterId = await wagesOTSetupMasterService.SaveoTSetupMaster(data);
            if (model.oTSlotTypeIdall != null)
            {
                await wagesOTSetupMasterService.DeleteOTSetupDetailByMasterId(masterId);
                if (model.oTSlotTypeIdall.Count() > 0)
                {

                    for (int i = 0; i < model.oTSlotTypeIdall.Count(); i++)
                    {
                        OTSetupDetail datadd = new OTSetupDetail
                        {
                            Id = 0,
                            oTSetupMasterId = masterId,
                            description = model.descriptionall[i],
                            basicPercent = model.basicPercentall[i],
                            oTSlotTypeId=model.oTSlotTypeIdall[i],
                            createdBy = HttpContext.User.Identity.Name,
                            createdAt = DateTime.Now
                        };

                        await wagesOTSetupMasterService.SaveoTSetupDetail(datadd);
                    }

                }
            }
          

            return RedirectToAction("Index");

           

        }

        [Route("global/api/OTSetup/GetSetupDetailbyMasterId/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetSetupDetailbyMasterId(int id)
        {
            return Json(await wagesOTSetupMasterService.GetOTSetupDetailByMasterId(id));
        }
    }
}
