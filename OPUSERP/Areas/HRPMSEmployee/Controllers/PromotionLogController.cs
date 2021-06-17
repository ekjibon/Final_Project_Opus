using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using OPUSERP.Areas.HRPMSAssignment.Helpers;
using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OPUSERP.Areas.HRPMSEmployee.Controllers
{
    [Authorize]
    [Area("HRPMSEmployee")]
    public class PromotionLogController : Controller
    {
        private readonly LangGenerate<PromotionLogLn> _lang;
        private readonly IPhotographService photographService;
        private readonly IPromotionLogService promotionLogService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly ISalaryGradeService salaryGradeService;
        private readonly IDesignationDepartmentService designationDepartmentService;

        public PromotionLogController(IHostingEnvironment hostingEnvironment, IPhotographService photographService, ISalaryGradeService salaryGradeService, IPersonalInfoService personalInfoService, IPromotionLogService promotionLogService, IDesignationDepartmentService designationDepartmentService)
        {
            _lang = new LangGenerate<PromotionLogLn>(hostingEnvironment.ContentRootPath);
            this.promotionLogService = promotionLogService;
            this.photographService = photographService;
            this.personalInfoService = personalInfoService;
            this.salaryGradeService = salaryGradeService;
            this.designationDepartmentService = designationDepartmentService;
        }

        // GET: PromotionLog
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var data = await personalInfoService.GetEmployeeInfoById(id);
            string desigName = data.designation;
            PromotionLogViewModel model = new PromotionLogViewModel
            {
                employeeID = id.ToString(),
                photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile"),
                employeeInfo = await personalInfoService.GetEmployeeInfoById(id),
                fLang = _lang.PerseLang("Employee/PromotionLogEN.json", "Employee/PromotionLogBN.json", Request.Cookies["lang"]),
                promotionLogs = await promotionLogService.GetPromotionLogByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id),
                salaryGrades = await salaryGradeService.GetAllSalaryGrade(),
                designations = await designationDepartmentService.GetDesignations(),
                designationName = await designationDepartmentService.GetDesignationIdByName(desigName),
            };

            return View(model);
        }

        // POST: PromotionLog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] PromotionLogViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("Employee/PromotionLogEN.json", "Employee/PromotionLogBN.json", Request.Cookies["lang"]);
                model.designations = await designationDepartmentService.GetDesignations();
                model.promotionLogs = await promotionLogService.GetPromotionLogByEmpId(Int32.Parse(model.employeeID));
                model.employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(Int32.Parse(model.employeeID));
                model.salaryGrades = await salaryGradeService.GetAllSalaryGrade();
                return View(model);
            }

            PromotionLog data = new PromotionLog
            {
                Id = Int32.Parse(model.promotionId),
                employeeId = Int32.Parse(model.employeeID),
                date = model.date,
                designationNewId = model.designationNewId,
                designationOldId = model.designationOldId,
                remark = model.remark,
                goNumber = model.goNumber,
                goDate = model.goDate,
                payScaleId = Int32.Parse(model.payScale)
                //nature = model.nature,
                //basic = model.basic,
                //rank = model.rank,

            };
            await promotionLogService.SavePromotionLog(data);

            return RedirectToAction("Index", "PromotionLog", new
            {
                id = Int32.Parse(model.employeeID)
            });

        }

        // Delete: Language
        public async Task<IActionResult> Delete(int id, int empId)
        {
            await promotionLogService.DeletePromotionLogById(id);
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Index", "PromotionLog", new
            {
                id = empId
            });
        }

        //xxxxxxxxxxxxxxxxxxxxxx
    }
}