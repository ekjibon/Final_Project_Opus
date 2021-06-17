using OPUSERP.CLUB.Data.Member;
using OPUSERP.CLUB.Services.Member.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using System;
using System.Threading.Tasks;
using OPUSERP.Areas.MemberInfo.Models;
using OPUSERP.CLUB.Services.Master.Interface;

namespace OPUSERP.Areas.MemberInfo.Controllers
{
    [Authorize]
    [Area("MemberInfo")]
    public class MembershipController : Controller
    {
        private readonly IMembershipService membershipService;
        private readonly IMembershipLanguageService membershipLanguageService;
        private readonly IMemberTypeService memberTypeService;
        private readonly LangGenerate<Models.Lang.MembershipLn> _lang;

        public MembershipController(IMembershipService membershipService, IMemberTypeService memberTypeService, IHostingEnvironment hostingEnvironment, IMembershipLanguageService membershipLanguageService)
        {
            this.membershipService = membershipService;
            this.memberTypeService = memberTypeService;
            this.membershipLanguageService = membershipLanguageService;
            _lang = new LangGenerate<Models.Lang.MembershipLn>(hostingEnvironment.ContentRootPath);
        }

        // GET: Membership
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new MembershipViewModel
            {
                employeeID = id.ToString(),
                memberTypes = await memberTypeService.GetAllMemberType(),
                employeeMemberships = await membershipService.GetMembershipInfoByEmpId(id),
                fLang = _lang.PerseLang("Employee/MembershipEN.json", "Employee/MembershipBN.json", Request.Cookies["lang"]),
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
                //model.memberships = await membershipLanguageService.GetMembershipInfo();
                model.employeeMemberships = await membershipService.GetMembershipInfoByEmpId(Int32.Parse(model.employeeID));
                model.fLang = _lang.PerseLang("Employee/MembershipEN.json", "Employee/MembershipBN.json", Request.Cookies["lang"]);
                return View(model);
            }

            OtherMembership data = new OtherMembership
            {
                Id = int.Parse(model.membershipId),
                employeeId = int.Parse(model.employeeID),
                nameOrganization = model.nameOrganization,
                membershipType = model.membershipType,
                remarks = model.remarks
            };

            await membershipService.SaveMembershipInfo(data);

            return RedirectToAction(nameof(Index));
        }
    }
}