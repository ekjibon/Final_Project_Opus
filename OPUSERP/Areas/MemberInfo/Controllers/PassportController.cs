using OPUSERP.CLUB.Data.Member;
using OPUSERP.CLUB.Services.Member.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Helpers;
using System;
using System.Threading.Tasks;
using OPUSERP.Areas.MemberInfo.Models;
using OPUSERP.Areas.MemberInfo.Models.Lang;

namespace OPUSERP.Areas.MemberInfo.Controllers
{
    [Authorize]
    [Area("MemberInfo")]
    public class PassportController : Controller
    {
        private readonly IPassportInfoService passportInfoService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly LangGenerate<Passport> _lang;

        public PassportController(IHostingEnvironment hostingEnvironment, IPersonalInfoService personalInfoService, IPassportInfoService passportInfoService)
        {
            this.passportInfoService = passportInfoService;
            this.personalInfoService = personalInfoService;
            _lang = new LangGenerate<Passport>(hostingEnvironment.ContentRootPath);
        }

        // GET: Passport
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new PassportViewModel
            {
                employeeID = id.ToString(),
               
                fLang = _lang.PerseLang("Employee/PassportEN.json", "Employee/PassportBN.json", Request.Cookies["lang"]),
                passportDetails = await passportInfoService.GetPassportInfoByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            return View(model);
        }

        // POST: Passport/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] PassportViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.passportDetails = await passportInfoService.GetPassportInfoByEmpId(Int32.Parse(model.employeeID));
               
                model.fLang = _lang.PerseLang("Employee/PassportEN.json", "Employee/PassportBN.json", Request.Cookies["lang"]);
                return View(model);
            }

            MemberPassportDetails data = new MemberPassportDetails
            {
                Id = Int32.Parse(model.passportId),
                employeeId = Int32.Parse(model.employeeID),
                passportNumber = model.passPortNumber,
                placeOfIssue = model.place,
                dateOfIssue = model.dateOfIssue,
                dateOfExpair = model.dateOfExpair
            };

            await passportInfoService.SavePassportInfo(data);

            return RedirectToAction(nameof(Index));
        }

    }
}