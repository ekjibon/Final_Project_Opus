using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSMasterData.Models;
using OPUSERP.Areas.HRPMSMasterData.Models.Lang;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSMasterData.Controllers
{
    [Authorize]
    [Area("HRPMSMasterData")]
    public class ShiftGroupAssignController : Controller
    {
        private readonly LangGenerate<ShiftGroupLn> _lang;
        private readonly IShiftGroupMasterService shiftGroupMasterService;
        private readonly ISpecialBranchUnitService specialBranchUnitService;
        private readonly IDesignationDepartmentService designationDepartmentService;
        private readonly IPersonalInfoService personalInfoService;

        public ShiftGroupAssignController(IHostingEnvironment hostingEnvironment, IShiftGroupMasterService shiftGroupMasterService, ISpecialBranchUnitService specialBranchUnitService, IDesignationDepartmentService designationDepartmentService, IPersonalInfoService personalInfoService)
        {
            _lang = new LangGenerate<ShiftGroupLn>(hostingEnvironment.ContentRootPath);
            this.shiftGroupMasterService = shiftGroupMasterService;
            this.specialBranchUnitService = specialBranchUnitService;
            this.designationDepartmentService = designationDepartmentService;
            this.personalInfoService = personalInfoService;
        }

        // GET: ShiftGroupMaster
        public async Task<ActionResult> Index()
        {
            ShiftGroupAssignViewModel model = new ShiftGroupAssignViewModel
            {
                fLang = _lang.PerseLang("MasterData/ShiftGroupMasterEN.json", "MasterData/ShiftGroupMasterBN.json", Request.Cookies["lang"]),
                shiftGroupMasterlist = await shiftGroupMasterService.GetAllShiftGroupMaster(),
                specialBranchUnits = await specialBranchUnitService.GetSpecialBranchUnit(),
                departments = await designationDepartmentService.GetDepartment(),
                visualEmpCodeName = await personalInfoService.GetEmpCodeNameVisualData()
            };
            return View(model);
        }

        // GET: ShiftGroupMaster
        public async Task<ActionResult> WagesIndex()
        {
            ShiftGroupAssignViewModel model = new ShiftGroupAssignViewModel
            {
                fLang = _lang.PerseLang("MasterData/ShiftGroupMasterEN.json", "MasterData/ShiftGroupMasterBN.json", Request.Cookies["lang"]),
                shiftGroupMasterlist = await shiftGroupMasterService.GetAllShiftGroupMaster(),
                specialBranchUnits = await specialBranchUnitService.GetSpecialBranchUnit(),
                departments = await designationDepartmentService.GetDepartment(),
            };
            return View(model);
        }


        // POST: ShiftGroupMaster/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ShiftGroupAssignViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("MasterData/ShiftGroupMasterEN.json", "MasterData/ShiftGroupMasterBN.json", Request.Cookies["lang"]);
                model.shiftGroupMasterlist = await shiftGroupMasterService.GetAllShiftGroupMaster();
                return View(model);
            }

            await shiftGroupMasterService.UpdateShiftGroupId(model.ShiftType, model.sbu, model.department, model.employeeInfoId, model.shiftGroup);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WagesIndex([FromForm] ShiftGroupAssignViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("MasterData/ShiftGroupMasterEN.json", "MasterData/ShiftGroupMasterBN.json", Request.Cookies["lang"]);
                model.shiftGroupMasterlist = await shiftGroupMasterService.GetAllShiftGroupMaster();
                return View(model);
            }

            await shiftGroupMasterService.UpdateShiftGroupIdForWages(model.ShiftType, model.sbu, model.department, model.employeeInfoId, model.shiftGroup);

            return RedirectToAction(nameof(Index));
        }


    }
}