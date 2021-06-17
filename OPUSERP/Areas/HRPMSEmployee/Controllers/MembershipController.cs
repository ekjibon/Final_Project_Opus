using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Controllers
{
    [Authorize]
    [Area("HRPMSEmployee")]
    public class MembershipController : Controller
    {
        private readonly IMembershipService membershipService;
        private readonly IPhotographService photographService;
        private readonly IMembershipLanguageService membershipLanguageService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly LangGenerate<Membership> _lang;

        public MembershipController(IMembershipService membershipService, IPhotographService photographService, IPersonalInfoService personalInfoService, IHostingEnvironment hostingEnvironment, IMembershipLanguageService membershipLanguageService)
        {
            this.membershipService = membershipService;
            this.photographService = photographService;
            this.personalInfoService = personalInfoService;
            this.membershipLanguageService = membershipLanguageService;
            _lang = new LangGenerate<Membership>(hostingEnvironment.ContentRootPath);
        }

        // GET: Membership
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new MembershipViewModel
            {
                employeeID = id.ToString(),
                photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile"),
                employeeInfo = await personalInfoService.GetEmployeeInfoById(id),
                memberships = await membershipLanguageService.GetMembershipInfo(),
                employeeMemberships = await membershipService.GetMembershipInfoByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id),
                fLang = _lang.PerseLang("Employee/MembershipEN.json")
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
                ViewBag.employeeID = model.employeeID;
                model.memberships = await membershipLanguageService.GetMembershipInfo();
                model.employeeMemberships = await membershipService.GetMembershipInfoByEmpId(Int32.Parse(model.employeeID));
                model.employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(Int32.Parse(model.employeeID));
                model.fLang = _lang.PerseLang("Employee/MembershipEN.json");
                return View(model);
            }

            EmployeeMembership data = new EmployeeMembership
            {
                Id = Int32.Parse(model.membershipId),
                employeeId = Int32.Parse(model.employeeID),
                nameOrganization = model.nameOrganization,
                membershipNo = model.membershipNo,
                membershipId = Int32.Parse(model.membershipType),
                remarks = model.remarks

            };

            await membershipService.SaveMembershipInfo(data);
            await personalInfoService.UpdateEmployeeinfoById(Int32.Parse(model.employeeID));
            return RedirectToAction("Index", "Membership", new
            {
                id = Int32.Parse(model.employeeID)
            });
        }

        public async Task<IActionResult> Delete(int id, int empId)
        {
            await membershipService.DeleteMembershipInfoById(id);
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Index", "Membership", new
            {
                id = empId
            });
        }


    }
}