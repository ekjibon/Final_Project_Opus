using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.CRMMasterData.Models;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Services.MasterData.Interfaces;

using System;
using System.Threading.Tasks;

namespace OPUSERP.Areas.CRMMasterData.Controllers
{
    [Area("CRMMasterData")]
    public class DepartmentController : Controller
    {
        //private readonly LangGenerate<DepartmentInfoLn> _lang;
        // GET: Department
        private readonly ICRMDesignationDepartmentService designationDepartmentService;

        public DepartmentController(IHostingEnvironment hostingEnvironment, ICRMDesignationDepartmentService designationDepartmentService)
        {
            //_lang = new LangGenerate<DepartmentInfoLn>(hostingEnvironment.ContentRootPath);
            this.designationDepartmentService = designationDepartmentService;
        }

        // GET: Department
        public async Task<IActionResult> Index()
        {
            DepartmentViewModel model = new DepartmentViewModel
            {
                //fLang = _lang.PerseLang("MasterData/DepartmentEN.json", "MasterData/DepartmentBN.json", Request.Cookies["lang"]),
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

            CRMDepartment data = new CRMDepartment
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