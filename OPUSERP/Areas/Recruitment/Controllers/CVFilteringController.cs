using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSReport.Models;
using OPUSERP.Areas.Recruitment.Models;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using OPUSERP.Recruitment.Services.Interfaces;

namespace OPUSERP.Areas.Recruitment.Controllers
{
    [Area("Recruitment")]
    public class CVFilteringController : Controller
    {
        private readonly ICandidateInfoService candidateInfoService;
        private readonly ITypeService typeService;
        private readonly IAddressService addressService;
        private readonly IReligionMunicipalityService religionMunicipalityService;
        private readonly IDesignationDepartmentService designationDepartmentService;
        private readonly IDegreeService degreeService;
        private readonly ISubjectService subjectService;
        private readonly IOrganizationService organizationService;
        private readonly IMembershipLanguageService membershipLanguageService;

        public CVFilteringController(ICandidateInfoService candidateInfoService, IDegreeService degreeService, IMembershipLanguageService membershipLanguageService, ISubjectService subjectService, IOrganizationService organizationService, ITypeService typeService, IAddressService addressService, IReligionMunicipalityService religionMunicipalityService, IDesignationDepartmentService designationDepartmentService)
        {
            this.candidateInfoService = candidateInfoService;
            this.typeService = typeService;
            this.addressService = addressService;
            this.religionMunicipalityService = religionMunicipalityService;
            this.designationDepartmentService = designationDepartmentService;
            this.degreeService = degreeService;
            this.subjectService = subjectService;
            this.organizationService = organizationService;
            this.membershipLanguageService = membershipLanguageService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new ReportingViewModel
            {
                countries = await addressService.GetAllContry(),
                districts = await addressService.GetAllDistrict(),
                divisions = await addressService.GetAllDivision(),
                thanas = await addressService.GetAllThana(),
                religions = await religionMunicipalityService.GetReligions(),
                employeeTypes = await typeService.GetAllEmployeeType(),
                designations = await designationDepartmentService.GetDesignations(),
                degrees = await degreeService.GetAllDegree(),
                subjects = await subjectService.GetAllSubject(),
                organizations = await organizationService.GetAllOrganization(),
                languages = await membershipLanguageService.GetLanguageInfo(),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Action([FromForm] CandidateInfoViewModel model)
        {
            ViewBag.candidateId = model.candidateId;
            string userName = HttpContext.User.Identity.Name;
            int status = 0;
            string updateBy = userName;
            if (model.status == "Approve")
            {
                status = 1;
            }
            else if (model.status == "Return")
            {
                status = 2;
            }
            else if (model.status == "Reject")
            {
                status = 3;
            }

            if (status == 1)
            {
                await candidateInfoService.UpdateCandidateInfo((int)model.cvId, status, updateBy);
                return RedirectToAction(nameof(Index));
            }

            if (status == 2)
            {
                await candidateInfoService.UpdateCandidateInfo((int)model.cvId, status, updateBy);
                return RedirectToAction(nameof(Index));
            }

            if (status == 3)
            {
                await candidateInfoService.UpdateCandidateInfo((int)model.cvId, status, updateBy);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }


        [AllowAnonymous]
        [Route("global/api/GetCVInfoAsQueryAble/{queryString}")]
        [HttpGet]
        public async Task<IActionResult> GetCVInfoAsQueryAble(string queryString)
        {
            return Json(await candidateInfoService.GetCVInfoAsQueryAble(queryString));
        }

        [AllowAnonymous]
        [Route("global/api/GetCandidateInfoById/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetCandidateInfoById(int id)
        {
            return Json(await candidateInfoService.GetCandidateInfoById(id));
        }
    }
}