using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Recruitment.Models;
using OPUSERP.Recruitment.Data.Entity;
using OPUSERP.Recruitment.Services.Interfaces;

namespace OPUSERP.Areas.Recruitment.Controllers
{
    [Authorize]
    [Area("Recruitment")]
    public class RcrtReferenceController : Controller
    {
        private readonly IRcrtReferenceService rcrtReferenceService;

        public RcrtReferenceController(IRcrtReferenceService rcrtReferenceService)
        {
            this.rcrtReferenceService = rcrtReferenceService;
        }

        public IActionResult Index(int id)
        {
            ViewBag.candidateId = id.ToString();
            var model = new ReferenceViewModel
            {
                candidateId = id,
                //rcrtReferences = await rcrtReferenceService.GetReferenceByEmpId(id)
                //employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ReferenceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.candidateId = model.candidateId;
                //model.references = await referenceService.GetReferenceByEmpId((int)model.employeeID);
                //model.employeeNameCode = await personalInfoService.GetEmployeeNameCodeById((int)model.employeeID);
                return View(model);
            }

            RcrtReference data = new RcrtReference
            {
                Id = model.refID ?? 0,
                candidateId = (int)model.candidateId,
                name = model.refName,
                organization = model.refOrganization,
                designation = model.refDesignation,
                email = model.refEmail,
                contact = model.refContact
            };

            await rcrtReferenceService.SaveRcrtReference(data);
            //await personalInfoService.UpdateEmployeeinfoById((int)model.employeeID);
            return RedirectToAction("Index", "RcrtReference", new
            {
                id = (int)model.candidateId
            });
        }
    }
}