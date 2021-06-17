using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Recruitment.Models;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using OPUSERP.Recruitment.Data.Entity;
using OPUSERP.Recruitment.Services.Interfaces;

namespace OPUSERP.Areas.Recruitment.Controllers
{
    [Authorize]
    [Area("Recruitment")]
    public class RcrtQualificationsController : Controller
    {
        //private readonly IPersonalInfoService personalInfoService;
        private readonly IResultService resultService;
        private readonly IRcrtQualificationsService rcrtQualificationsService;
        private readonly IOtherQualificationHeadService otherQualificationHeadService;
        private readonly IQualificationHeadService qualificationHeadService;

        public RcrtQualificationsController(IResultService resultService, IOtherQualificationHeadService otherQualificationHeadService, IQualificationHeadService qualificationHeadService, IRcrtQualificationsService rcrtQualificationsService)
        {
            this.resultService = resultService;
            this.rcrtQualificationsService = rcrtQualificationsService;
            this.otherQualificationHeadService = otherQualificationHeadService;
            this.qualificationHeadService = qualificationHeadService;
        }

        public async Task<IActionResult> OtherQualificationInfo(int id)
        {
            ViewBag.candidateId = id.ToString();
            var model = new OtherQualificationsViewModel
            {
                candidateId = id,
                otherQualificationHeads = await otherQualificationHeadService.GetOtherQualificationHead(),
                results = await resultService.GetAllResult(),
                //otherQualifications = await rcrtQualificationsService.GetOtherQualificationByEmpId(id),
                //employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OtherQualificationInfo([FromForm] OtherQualificationsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.candidateId = model.candidateId;
                model.otherQualificationHeads = await otherQualificationHeadService.GetOtherQualificationHead();
                model.results = await resultService.GetAllResult();
                //model.otherQualifications = await otherQualificationService.GetOtherQualificationByEmpId((int)model.employeeID);
                //model.employeeNameCode = await personalInfoService.GetEmployeeNameCodeById((int)model.employeeID);
                return View(model);
            }

            RCRTOtherQualification data = new RCRTOtherQualification
            {
                Id = model.qualificationID ?? 0,
                candidateId = (int)model.candidateId,
                otherQualificationHeadId = model.qualificationHeadId,
                subject = model.subject,
                resultId = model.result,
                instituteName = model.instituteName,
                passingYear = model.passingYear,
                markGrade = model.markGrade,
            };

            await rcrtQualificationsService.SaveRcrtOtherQualification(data);
            //await personalInfoService.UpdateEmployeeinfoById((int)model.employeeID);
            return RedirectToAction("OtherQualificationInfo", "RcrtQualifications", new
            {
                id = (int)model.candidateId
            });
        }

        public async Task<IActionResult> ProfessionalQualificationInfo(int id)
        {
            ViewBag.candidateId = id.ToString();
            var model = new ProfessionalQualificationsViewModel
            {
                candidateId = id,
                qualificationHeads = await qualificationHeadService.GetQualificationHead(),
                results = await resultService.GetAllResult(),
                //professionalQualifications = await professionalQualificationsService.GetProfessionalQualificationsByEmpId(id),
                //employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProfessionalQualificationInfo([FromForm] ProfessionalQualificationsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.candidateId = model.candidateId;
                model.qualificationHeads = await qualificationHeadService.GetQualificationHead();
                model.results = await resultService.GetAllResult();
                //model.professionalQualifications = await professionalQualificationsService.GetProfessionalQualificationsByEmpId((int)model.employeeID);
                //model.employeeNameCode = await personalInfoService.GetEmployeeNameCodeById((int)model.employeeID);
                return View(model);
            }

            RCRTProfessionalQualification data = new RCRTProfessionalQualification
            {
                Id = model.qualificationID ?? 0,
                candidateId = (int)model.candidateId,
                qualificationHeadId = model.qualificationHeadId,
                subject = model.subject,
                resultId = model.result,
                instituteName = model.instituteName,
                passingYear = model.passingYear,
                markGrade = model.markGrade,
            };

            await rcrtQualificationsService.SaveRcrtProfessionalQualification(data);
            //await personalInfoService.UpdateEmployeeinfoById((int)model.employeeID);
            return RedirectToAction("ProfessionalQualificationInfo", "RcrtQualifications", new
            {
                id = (int)model.candidateId
            });
        }
    }
}