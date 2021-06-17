using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Controllers
{
    [Authorize]
    [Area("HRPMSEmployee")]
    public class PromotionController : Controller
    {
        private readonly IPromotionService promotionService;
        private readonly LangGenerate<EmployeeInfoLn> _langEmp;
        private readonly LangGenerate<Promotion> _lang;
        private readonly IDesignationDepartmentService designationDepartmentService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly ISalaryGradeService salaryGradeService;
        

        public PromotionController(IHostingEnvironment hostingEnvironment, IPersonalInfoService personalInfoService, IDesignationDepartmentService designationDepartmentService,ISalaryGradeService salaryGradeService,IPromotionService promotionService)
        {
            _langEmp = new LangGenerate<EmployeeInfoLn>(hostingEnvironment.ContentRootPath);
            _lang = new LangGenerate<Promotion>(hostingEnvironment.ContentRootPath);
            this.personalInfoService = personalInfoService;
            this.designationDepartmentService = designationDepartmentService;
            this.salaryGradeService = salaryGradeService;
            this.promotionService = promotionService;
        }

        // GET: Employee/PromotionEmployeeList
        public async Task<IActionResult> PromotionEmployeeList()
        {
            var model = new EmployeeInfoViewModel
            {
                fLang = _langEmp.PerseLang("Promotion/PromotionEmpListEN.json", "Promotion/PromotionEmpListBN.json", Request.Cookies["lang"]),
                allEmployeeInfos = await personalInfoService.GetEmployeeInfo()
            };
            return View(model);
        }

        // GET: Promotion
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new PromotionViewModel
            {
                fLang = _lang.PerseLang("Employee/PromotionEmpListEN.json", "Employee/PromotionEmpListBN.json", Request.Cookies["lang"]),
                designations = await designationDepartmentService.GetDesignations(),
                //employeeInfos=await personalInfoService.GetEmployeeInfo(),
                salaryGrades=await salaryGradeService.GetAllSalaryGrade(),
                promotions=await promotionService.GetPromotionInfoByEmpId(id)
            };
            return View(model);
        }
 
        // POST: Promotion/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] PromotionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("Employee/PromotionEmpListEN.json", "Employee/PromotionEmpListBN.json", Request.Cookies["lang"]);
                model.designations = await designationDepartmentService.GetDesignations();
                model.employeeInfos = await personalInfoService.GetEmployeeInfo();
                model.salaryGrades = await salaryGradeService.GetAllSalaryGrade();
                return View(model);
            }

            HRPMS.Data.Entity.Employee.Promotion data = new HRPMS.Data.Entity.Employee.Promotion
            {
                Id = Convert.ToInt32(model.ID),
                employeeId = Convert.ToInt32(model.employeeId),
                designationId = model.designationId,
                salaryGradeId = model.salaryGradeId,
                promotionDate = model.promotionDate,
                promotionType = model.promotionType,
                Basic = model.Basic,
                Remarks = model.Remarks
            };

            int lstId = await promotionService.SavePromotion(data);

            return RedirectToAction(nameof(Index), new { @id = model.employeeId });
        }

    }
}