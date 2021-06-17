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
    public class RcrtMembershipController : Controller
    {
        private readonly IRcrtMembershipService rcrtMembershipService;
        private readonly IMembershipLanguageService membershipLanguageService;

        public RcrtMembershipController(IRcrtMembershipService rcrtMembershipService, IMembershipLanguageService membershipLanguageService)
        {
            this.rcrtMembershipService = rcrtMembershipService;
            this.membershipLanguageService = membershipLanguageService;
        }

        public async Task<IActionResult> Index(int id)
        {
            ViewBag.candidateId = id.ToString();
            var model = new MembershipViewModel
            {
                candidateId = id.ToString(),
                memberships = await membershipLanguageService.GetMembershipInfo(),
                //employeeMemberships = await membershipService.GetMembershipInfoByEmpId(id),
                //employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id),
            };
            return View(model);
        }

        // POST: Membership/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] MembershipViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.candidateId = model.candidateId;
                model.memberships = await membershipLanguageService.GetMembershipInfo();
                //model.rcrtEmpMemberships = await rcrtMembershipService.GetMembershipInfoByCandidateId(Int32.Parse(model.candidateId));
                //model.employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(Int32.Parse(model.employeeID));
                return View(model);
            }

            RcrtEmpMembership data = new RcrtEmpMembership
            {
                Id = Int32.Parse(model.membershipId),
                candidateId = Int32.Parse(model.candidateId),
                nameOrganization = model.nameOrganization,
                membershipNo = model.membershipNo,
                membershipId = Int32.Parse(model.membershipType),
                remarks = model.remarks
            };

            await rcrtMembershipService.SaveRcrtMembershipInfo(data);
            //await personalInfoService.UpdateEmployeeinfoById(Int32.Parse(model.employeeID));
            return RedirectToAction("Index", "RcrtMembership", new
            {
                id = Int32.Parse(model.candidateId)
            });
        }
    }
}