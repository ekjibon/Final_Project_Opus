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
    public class EmployeeTypeController : Controller
    {
        private readonly LangGenerate<EmployeeTypeLn> _lang;
        private readonly ITypeService typeService;

        public EmployeeTypeController(IHostingEnvironment hostingEnvironment, ITypeService typeService)
        {
            _lang = new LangGenerate<EmployeeTypeLn>(hostingEnvironment.ContentRootPath);
            this.typeService = typeService;
        }

        // GET: EmployeeType
        public async Task<IActionResult> Index()
        {
            EmployeeTypeViewModel model = new EmployeeTypeViewModel
            {
                fLang = _lang.PerseLang("MasterData/EmployeeTypeEN.json", "MasterData/EmployeeTypeBN.json", Request.Cookies["lang"]),
                employeeTypes = await typeService.GetAllEmployeeType()
            };

            return View(model);
        }

       
        // POST: EmployeeType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] EmployeeTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("MasterData/EmployeeTypeEN.json", "MasterData/EmployeeTypeBN.json", Request.Cookies["lang"]);
                model.employeeTypes = await typeService.GetAllEmployeeType();
                return View(model);
            }

            EmployeeType data = new EmployeeType
            {
                Id = model.empTypeId,
                empType = model.empType,
                empTypeBn = model.empTypeBn,
                shortName = model.shortName
            };

            await typeService.SaveEmployeeType(data);

            return RedirectToAction(nameof(Index));
        }
    }
}