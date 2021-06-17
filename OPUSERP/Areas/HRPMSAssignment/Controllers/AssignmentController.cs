using OPUSERP.Areas.HRPMSAssignment.Helpers;
using OPUSERP.Areas.HRPMSAssignment.Models;
using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using OPUSERP.HRPMS.Services.Assignment.Interfaces;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSAssignment.Controllers
{
    [Area("HRPMSAssignment")]
    public class AssignmentController : Controller
    {
        //private readonly LangGenerate<Models.Lang.Assignment> _lang;
        private readonly LangGenerate<EmployeeInfoLn> _lang;
        private readonly IPersonalInfoService personalInfoService;
        private readonly IAssignmentService assignmentService;
        private readonly IDesignationDepartmentService designationDepartmentService;

        public AssignmentController(IHostingEnvironment hostingEnvironment, IPersonalInfoService personalInfoService, IAssignmentService assignmentService,IDesignationDepartmentService designationDepartmentService)
        {
            _lang = new LangGenerate<EmployeeInfoLn>(hostingEnvironment.ContentRootPath);
            this.personalInfoService = personalInfoService;
            this.assignmentService = assignmentService;
            this.designationDepartmentService = designationDepartmentService;
        }

        // GET: Assignment/AssignEmployeeList
        public async Task<IActionResult> AssignEmployeeList()
        {
            var model = new EmployeeWithDesignationVM
            {
                //fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json"),
                employeeWithDesignations = await personalInfoService.GetEmployeeInfoDetailsList(0),
                visualEmpCodeName = await personalInfoService.GetEmpCodeNameVisualData()
            };
            return View(model);
        }

        // GET: ACR
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            //ViewBag.employeeName = employeeName;
            var model = new AssignmentViewModel
            {
                //fLang = _lang.PerseLang("Assignment/AssignmentEN.json"), 
                assignments = await assignmentService.GetAssignmentByTypeAndEmpId(id,1),
                designations=await designationDepartmentService.GetDesignations(),
                departments=await designationDepartmentService.GetDepartment()
            };
            return View(model);
        }

        // POST: Info/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] AssignmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeId.ToString();
                ViewBag.employeeName = model.employeeName;

                //fLang = _lang.PerseLang("Assignment/AssignmentEN.json"), 
                model.assignments = await assignmentService.GetAssignmentByTypeAndEmpId(Convert.ToInt32(model.employeeId), 1);
                model.designations = await designationDepartmentService.GetDesignations();
                model.departments = await designationDepartmentService.GetDepartment();

                return View(model);
            }

            HRPMS.Data.Entity.Assignment.Assignment data = new HRPMS.Data.Entity.Assignment.Assignment
            {
                Id = Convert.ToInt32(model.ID),
                employeeId =Convert.ToInt32(model.employeeId),
                assignmentTypeId =1,
                EntryNo=model.EntryNo,
                StartDate=model.StartDate,
                EndDate=model.EndDate,
                designationId=model.designationId,
                departmentId=model.departmentId,
                Remarks=model.Remarks
            };

            int lstId = await assignmentService.SaveAssignment(data);

            return RedirectToAction(nameof(Index), new { @id = model.employeeId });
        }

        // GET: ACR/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ACR/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ACR/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ACR/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ACR/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ACR/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ACR/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int employeeID)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        #region API Section
        [Route("global/api/assignment/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAssignment(int Id)
        {
            return Json(await assignmentService.DeleteAssignmentById(Id));
        }
        #endregion

    }
}