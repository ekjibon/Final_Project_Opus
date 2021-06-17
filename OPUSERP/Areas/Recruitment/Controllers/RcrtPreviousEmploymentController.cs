using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Recruitment.Models;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using OPUSERP.Recruitment.Data.Entity;
using OPUSERP.Recruitment.Services.Interfaces;

namespace OPUSERP.Areas.Recruitment.Controllers
{
    [Authorize]
    [Area("Recruitment")]
    public class RcrtPreviousEmploymentController : Controller
    {
        private readonly IRcrtPrevEmploymentService rcrtPrevEmploymentService;
        private readonly IHRPMSOrganizationTypeService iHRPMSOrganizationTypeService;

        public RcrtPreviousEmploymentController(IRcrtPrevEmploymentService rcrtPrevEmploymentService, IHRPMSOrganizationTypeService iHRPMSOrganizationTypeService)
        {
            this.rcrtPrevEmploymentService = rcrtPrevEmploymentService;
            this.iHRPMSOrganizationTypeService = iHRPMSOrganizationTypeService;
        }

        public async Task<IActionResult> Index(int id)
        {
            ViewBag.candidateId = id.ToString();
            var model = new PreviousEmploymentViewModel
            {
                candidateId = id,
                rcrtPreviousEmployments = await rcrtPrevEmploymentService.GetPreviousEmploymentByCandidateId(id),
                hRPMSOrganizationTypes = await iHRPMSOrganizationTypeService.GetHRPMSOrganizationType()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] PreviousEmploymentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.candidateId;
                model.hRPMSOrganizationTypes = await iHRPMSOrganizationTypeService.GetHRPMSOrganizationType();
                model.rcrtPreviousEmployments = await rcrtPrevEmploymentService.GetPreviousEmploymentByCandidateId((int)model.candidateId);
                return View(model);
            }

            RcrtPreviousEmployment data = new RcrtPreviousEmployment
            {
                Id = model.previousEmploymentID ?? 0,
                candidateId = (int)model.candidateId,
                organizationTypeId = model.organizationType,
                companyName = model.companyName,
                position = model.position,
                department = model.department,
                companyBusiness = model.companyBusiness,
                startDate = model.startDate,
                endDate = model.endDate,
                companyLocation = model.companyLocation,
                responsibilities = model.responsibilities
            };

            await rcrtPrevEmploymentService.SaveRcrtPreviousEmployment(data);

            return RedirectToAction("Index", "RcrtPreviousEmployment", new
            {
                id = (int)model.candidateId
            });
        }
    }
}