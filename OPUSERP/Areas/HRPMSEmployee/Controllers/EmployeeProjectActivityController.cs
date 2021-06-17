using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Accounting.Services.MasterData.Interfaces;
using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using OPUSERP.Payroll.Services.Salary.Interfaces;

namespace OPUSERP.Areas.HRPMSEmployee.Controllers
{
    [Authorize]
    [Area("HRPMSEmployee")]
    public class EmployeeProjectActivityController : Controller
    {
        private readonly IEmployeeProjectActivityService employeeProjectActivityService;
        private readonly IHRDonerSevice hRDonerSevice;
        private readonly IPhotographService photographService;
        private readonly IHRProjectService hRProjectService;
        private readonly IHRActivityService hRActivityService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly ICostCentreService costCentreService;
        private readonly ISalaryService salaryService;

        public EmployeeProjectActivityController(IPersonalInfoService personalInfoService, IPhotographService photographService, IEmployeeProjectActivityService employeeProjectActivityService, IHRDonerSevice hRDonerSevice, IHRProjectService hRProjectService, IHRActivityService hRActivityService, ICostCentreService costCentreService, ISalaryService salaryService)
        {
            this.employeeProjectActivityService = employeeProjectActivityService;
            this.hRDonerSevice = hRDonerSevice;
            this.photographService = photographService;
            this.hRProjectService = hRProjectService;
            this.hRActivityService = hRActivityService;
            this.personalInfoService = personalInfoService;
            this.costCentreService = costCentreService;
            this.salaryService = salaryService;
        }

        #region Employee Project Activity
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id;
            EmployeeProjectActivityViewModel model = new EmployeeProjectActivityViewModel
            {
                employeeId = id,
                photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile"),
                employeeInfo = await personalInfoService.GetEmployeeInfoById(id),
                hRDoners = await hRDonerSevice.GetHRDoner(),
                hRActivities = await hRActivityService.GetHRActivity(),
                hRProjects = await hRProjectService.GetHRProject(),
                employeeProjectActivities = await employeeProjectActivityService.GetEmployeeProjectActivityByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([FromForm] EmployeeProjectActivityViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeId;
                model.hRDoners = await hRDonerSevice.GetHRDoner();
                model.hRActivities = await hRActivityService.GetHRActivity();
                model.hRProjects = await hRProjectService.GetHRProject();
                model.employeeProjectActivities = await employeeProjectActivityService.GetEmployeeProjectActivityByEmpId((int)model.employeeId);
                model.employeeNameCode = await personalInfoService.GetEmployeeNameCodeById((int)model.employeeId);
                return View(model);
            }

            EmployeeProjectActivity data = new EmployeeProjectActivity
            {
                Id = (int)model.id,
                employeeId = model.employeeId,
                hRDonerId = model.hRDonerId,
                hRProjectId = model.hRProjectId,
                hRActivityId = model.hRActivityId,
                isActive = model.isActive
            };
            await employeeProjectActivityService.SaveEmployeeProjectActivity(data);

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id, int empId)
        {
            await employeeProjectActivityService.DeleteEmployeeProjectActivityById(id);
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("ProjectAssign", "EmployeeProjectActivity", new
            {
                id = empId
            });
        }

        #endregion

        #region  Project Assign

        public async Task<IActionResult> ProjectAssign(int id)
        {
            ViewBag.employeeID = id;
            EmployeeProjectActivityViewModel model = new EmployeeProjectActivityViewModel
            {
                employeeId = id,
                photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile"),
                employeeInfo = await personalInfoService.GetEmployeeInfoById(id),
                projects = await employeeProjectActivityService.GetAllRunningProject(),
                employeeProjectAssigns = await employeeProjectActivityService.GetEmployeeProjectAssignByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ProjectAssign([FromForm] EmployeeProjectActivityViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeId;
                
                model.projects = await employeeProjectActivityService.GetAllRunningProject();
                model.employeeProjectAssigns = await employeeProjectActivityService.GetEmployeeProjectAssignByEmpId((int)model.employeeId);
                model.employeeNameCode = await personalInfoService.GetEmployeeNameCodeById((int)model.employeeId);
                return View(model);
            }

            EmployeeProjectAssign data = new EmployeeProjectAssign
            {
                Id = (int)model.id,
                employeeId = model.employeeId,
                projectId = model.projectId,
                isActive = model.isActive
            };
            await employeeProjectActivityService.SaveEmployeeProjectAssign(data);

            return RedirectToAction(nameof(ProjectAssign));
        }

        public async Task<IActionResult> DeleteProjectAssign(int id, int empId)
        {
            await employeeProjectActivityService.DeleteEmployeeProjectAssignById(id);
            return RedirectToAction("ProjectAssign", "EmployeeProjectActivity", new
            {
                id = empId
            });
        }
        #endregion

        #region Employee Other Info

        public async Task<IActionResult> EmployeeOtherInfo(int id)
        {
            ViewBag.employeeID = id;
            EmployeeOtherInfoViewModel model = new EmployeeOtherInfoViewModel
            {
                employeeId = id,
                photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile"),
                employeeInfo = await personalInfoService.GetEmployeeInfoById(id),
                hRFacilities = await hRActivityService.GetHRFacility(),               
                employeeOtherInfos = await employeeProjectActivityService.GetEmployeeOtherInfoByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EmployeeOtherInfo([FromForm] EmployeeOtherInfoViewModel model)
        {

            EmployeeOtherInfo data = new EmployeeOtherInfo
            {
                Id = model.id,
                employeeInfoId = model.employeeId,
                hRFacilityId = model.hRFacilityId,
                simsId = model.simsId,
                busArea = model.busArea,
                type = model.type
            };
            await employeeProjectActivityService.SaveEmployeeOtherInfo(data);

            return RedirectToAction(nameof(EmployeeOtherInfo));
        }


        public async Task<IActionResult> DeleteEmployeeOtherInfoById(int id, int empId)
        {
            await employeeProjectActivityService.DeleteEmployeeOtherInfoById(id);
            return RedirectToAction("EmployeeOtherInfo", "EmployeeProjectActivity", new
            {
                id = empId
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetHRFacilityById(int id)
        {
            return Json(await hRActivityService.GetHRFacilityById(id));
        }

        #endregion

        #region Employee Cost Centre

        public async Task<IActionResult> EmployeeCostCentre(int id)
        {
            ViewBag.employeeID = id;
            EmployeeCostCentreViewModel model = new EmployeeCostCentreViewModel
            {
                employeeId = id,
                photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile"),
                employeeInfo = await personalInfoService.GetEmployeeInfoById(id),
                costCentres = await costCentreService.GetCostCentres(),
                employeeCostCentres = await employeeProjectActivityService.GetEmployeeCostCentreByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EmployeeCostCentre([FromForm] EmployeeCostCentreViewModel model)
        {

            EmployeeCostCentre data = new EmployeeCostCentre
            {
                Id = model.id,
                employeeInfoId = model.employeeId,
                costCentreId = model.costCentreId,                
                costCentreDate = model.costCentreDate
            };
            await employeeProjectActivityService.SaveEmployeeCostCentre(data);
            return RedirectToAction(nameof(EmployeeCostCentre));
        }


        public async Task<IActionResult> DeleteEmployeeCostCentreById(int id, int empId)
        {
            await employeeProjectActivityService.DeleteEmployeeCostCentreById(id);
            return RedirectToAction("EmployeeCostCentre", "EmployeeProjectActivity", new
            {
                id = empId
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetcostCentreById(int id)
        {
            return Json(await costCentreService.GetcostCentreById(id));
        }

        #endregion

        #region Employee Grade

        public async Task<IActionResult> EmployeeGrade(int id)
        {
            ViewBag.employeeID = id;
            EmployeeGradeViewModel model = new EmployeeGradeViewModel
            {
                employeeId = id,
                photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile"),
                employeeInfo = await personalInfoService.GetEmployeeInfoById(id),
                salaryGrades = await salaryService.GetAllSalaryGrade(),
                employeeGrades = await employeeProjectActivityService.GetEmployeeGradeByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EmployeeGrade([FromForm] EmployeeGradeViewModel model)
        {
            EmployeeGrade data = new EmployeeGrade
            {
                Id = model.id,
                employeeInfoId = model.employeeId,
                salaryGradeId = model.salaryGradeId,
                effectiveDate = model.effectiveDate
            };
            await employeeProjectActivityService.SaveEmployeeGrade(data);
            return RedirectToAction(nameof(EmployeeGrade));
        }

        public async Task<IActionResult> DeleteEmployeeGradeById(int id, int empId)
        {
            await employeeProjectActivityService.DeleteEmployeeGradeById(id);
            return RedirectToAction("EmployeeGrade", "EmployeeProjectActivity", new
            {
                id = empId
            });
        }        

        #endregion

    }
}