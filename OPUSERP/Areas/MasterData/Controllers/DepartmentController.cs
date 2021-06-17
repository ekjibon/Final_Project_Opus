using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.MasterData.Models;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.MasterData.Controllers
{
    [Area("MasterData")]
    public class DepartmentController : Controller
    {
        //private readonly LangGenerate<DepartmentInfoLn> _lang;
        // GET: Department
        private readonly IDesignationDepartmentService designationDepartmentService;
        private readonly IUserTypeService userTypeService;

        public DepartmentController(IHostingEnvironment hostingEnvironment, IUserTypeService userTypeService, IDesignationDepartmentService designationDepartmentService)
        {
            //_lang = new LangGenerate<DepartmentInfoLn>(hostingEnvironment.ContentRootPath);
            this.designationDepartmentService = designationDepartmentService;
            this.userTypeService = userTypeService;
        }

        // GET: Department
        public async Task<IActionResult> Index()
        {
            var country = await designationDepartmentService.GetDepartment();
            int count = country.Count();
            var autonumber = await userTypeService.GetAutonumberingInfoById("Department");
            var total = 0;
            string code = "";
            if (autonumber.NumType == 1)
            {
                total = count + (int)autonumber.defaultValue;
                code = userTypeService.GetNumberCode(total.ToString(), (int)autonumber.NumValue);
            }
            else
            {
                total = count + (int)autonumber.startValue;
                code = userTypeService.GetNumberCode(total.ToString(), (int)autonumber.NumValue);
                code = autonumber.prefix + code;
            }

            DepartmentViewModel model = new DepartmentViewModel
            {
                deptCode = code,
                departments = await designationDepartmentService.GetDepartment()
            };
            return View(model);
        }

        // POST: Department/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] DepartmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //model.fLang = _lang.PerseLang("MasterData/DepartmentEN.json", "MasterData/DepartmentBN.json", Request.Cookies["lang"]);
                model.departments = await designationDepartmentService.GetDepartment();
                return View(model);
            }

            Department data = new Department
            {
                Id = Int32.Parse(model.departmentId),
                deptCode = model.deptCode,
                deptName = model.deptName,
                deptNameBn = model.deptNameBn,
                shortName = model.shortName
            };

            await designationDepartmentService.SaveDepartment(data);

            return RedirectToAction(nameof(Index));
        }
    }
}