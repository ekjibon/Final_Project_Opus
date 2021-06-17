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
    public class TransferLogController : Controller
    {
        //private readonly LangGenerate<TransferLogLn> _lang;
        private readonly LangGenerate<TransferLogLn> _lang;
        private readonly ISalaryGradeService salaryGradeService;
        private readonly IPhotographService photographService;
        private readonly IServiceHistoryService serviceHistoryService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly IDesignationDepartmentService designationDepartmentService;

        public TransferLogController(IHostingEnvironment hostingEnvironment, IPhotographService photographService, IDesignationDepartmentService designationDepartmentService, IPersonalInfoService personalInfoService, ISalaryGradeService salaryGradeService, IServiceHistoryService serviceHistoryService)
        {
            _lang = new LangGenerate<TransferLogLn>(hostingEnvironment.ContentRootPath);
            this.salaryGradeService = salaryGradeService;
            this.photographService = photographService;
            this.serviceHistoryService = serviceHistoryService;
            this.personalInfoService = personalInfoService;
            this.designationDepartmentService = designationDepartmentService;
        }

        // GET: Transfar
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new TransferLogViewModel
            {
                employeeID = id.ToString(),
                photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile"),
                employeeInfo = await personalInfoService.GetEmployeeInfoById(id),
                fLang = _lang.PerseLang("Employee/TransferLogEN.json", "Employee/TransferLogBN.json", Request.Cookies["lang"]),
                salaryGrade = await salaryGradeService.GetAllSalaryGrade(),
                transferLogs = await serviceHistoryService.GetServiceHistoryByEmpId(id),
                designations = await designationDepartmentService.GetDesignations(),
                departments = await designationDepartmentService.GetDepartment(),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            return View(model);
        }


        // POST: Transfar/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] TransferLogViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    ViewBag.employeeID = model.employeeID;
            //    model.fLang = _lang.PerseLang("Employee/TransferLogEN.json", "Employee/TransferLogBN.json", Request.Cookies["lang"]);
            //    model.designations = await designationDepartmentService.GetDesignations();
            //    model.departments = await designationDepartmentService.GetDepartment();
            //    model.salaryGrade = await salaryGradeService.GetAllSalaryGrade();
            //    model.transferLogs = await serviceHistoryService.GetServiceHistoryByEmpId(Convert.ToInt32(model.employeeID));


            //    return View(model);
            //}

            TransferLog data = new TransferLog
            {
                Id = Int32.Parse(model.transfarID),
                employeeId = Int32.Parse(model.employeeID),
                workStation = model.workStation,
                departmentId = model.departmentId,
                designatioId = model.designationId,
                from = model.fromDate,
                to = model.toDate,
                salaryGradeId = model.grade,
                designation = model.designation
            };

            await serviceHistoryService.SaveServiceHistory(data);
            await personalInfoService.UpdateEmployeeinfoById(Int32.Parse(model.employeeID));
            return RedirectToAction("Index", "TransferLog", new
            {
                id = Int32.Parse(model.employeeID)
            });
        }

        // Delete: Language
        public async Task<IActionResult> Delete(int id, int empId)
        {
            await serviceHistoryService.DeleteServiceHistoryById(id);
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Index", "TransferLog", new
            {
                id = empId
            });
        }

    }
}