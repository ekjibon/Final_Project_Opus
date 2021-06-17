using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;

namespace OPUSERP.Areas.HRPMSEmployee.Controllers
{
    [Authorize]
    [Area("HRPMSEmployee")]
    public class ProfessionalQualificationsController : Controller
    {
        private readonly IPersonalInfoService personalInfoService;
        private readonly IResultService resultService;
        private readonly IPhotographService photographService;
        private readonly IQualificationHeadService qualificationHeadService;
        private readonly IProfessionalQualificationsService professionalQualificationsService;

        public ProfessionalQualificationsController(IPersonalInfoService personalInfoService, IPhotographService photographService, IResultService resultService, IQualificationHeadService qualificationHeadService, IProfessionalQualificationsService professionalQualificationsService)
        {
            this.personalInfoService = personalInfoService;
            this.resultService = resultService;
            this.photographService = photographService;
            this.qualificationHeadService = qualificationHeadService;
            this.professionalQualificationsService = professionalQualificationsService;
        }

        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new ProfessionalQualificationsViewModel
            {
                employeeID = id,
                photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile"),
                employeeInfo = await personalInfoService.GetEmployeeInfoById(id),
                qualificationHeads = await qualificationHeadService.GetQualificationHead(),
                results = await resultService.GetAllResult(),
                professionalQualifications = await professionalQualificationsService.GetProfessionalQualificationsByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ProfessionalQualificationsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.qualificationHeads = await qualificationHeadService.GetQualificationHead();
                model.results = await resultService.GetAllResult();
                model.professionalQualifications = await professionalQualificationsService.GetProfessionalQualificationsByEmpId((int)model.employeeID);
                model.employeeNameCode = await personalInfoService.GetEmployeeNameCodeById((int)model.employeeID);
                return View(model);
            }

            ProfessionalQualifications data = new ProfessionalQualifications
            {
                Id = model.qualificationID ?? 0,
                employeeID = (int)model.employeeID,
                qualificationHeadId = model.qualificationHeadId,
                subject = model.subject,
                resultId = model.result,
                instituteName = model.instituteName,
                passingYear = model.passingYear,
                markGrade = model.markGrade,
            };

            await professionalQualificationsService.SaveProfessionalQualifications(data);
            await personalInfoService.UpdateEmployeeinfoById((int)model.employeeID);
            return RedirectToAction("Index", "ProfessionalQualifications", new
            {
                id = (int)model.employeeID
            });
        }

        public async Task<IActionResult> Delete(int id, int empId)
        {
            await professionalQualificationsService.DeleteProfessionalQualificationsById(id);
            return RedirectToAction("Index", "ProfessionalQualifications", new
            {
                id = empId
            });
        }


    }
}