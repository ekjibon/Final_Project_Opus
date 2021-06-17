using OPUSERP.CLUB.Data.Member;
using OPUSERP.CLUB.Services.Member.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Helpers;
using System;
using System.Threading.Tasks;
using OPUSERP.Areas.MemberInfo.Models.Lang;
using OPUSERP.Areas.MemberInfo.Models;

namespace OPUSERP.Areas.MemberInfo.Controllers
{
    [Authorize]
    [Area("MemberInfo")]
    public class LicenseController : Controller
    {
        private readonly IDrivingLicenseService drivingLicenseService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly LangGenerate<License> _lang;

        public LicenseController(IHostingEnvironment hostingEnvironment, IPersonalInfoService personalInfoService, IDrivingLicenseService drivingLicenseService)
        {
            this.drivingLicenseService = drivingLicenseService;
            this.personalInfoService = personalInfoService;
            _lang = new LangGenerate<License>(hostingEnvironment.ContentRootPath);
        }

        // GET: License
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new LicenseViewModel
            {
                employeeID = id.ToString(),
                fLang = _lang.PerseLang("Employee/LicenseEN.json", "Employee/LicenseBN.json", Request.Cookies["lang"]),
                licenses = await drivingLicenseService.GetDrivingLicenseByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            return View(model);
        }

        // POST: License/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] LicenseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.licenses = await drivingLicenseService.GetDrivingLicenseByEmpId(Int32.Parse(model.employeeID));
                model.fLang = _lang.PerseLang("Employee/LicenseEN.json", "Employee/LicenseBN.json", Request.Cookies["lang"]);
                return View(model);
            }

            MemberDrivingLicense data = new MemberDrivingLicense
            {
                Id = Int32.Parse(model.licenseId),
                employeeId = Int32.Parse(model.employeeID),
                licenseNumber = model.licenseNumber,
                category = model.licenseCategory,
                placeOfIssue = model.place,
                dateOfIssue = model.dateOfIssue,
                dateOfExpair = model.dateOfExpair
            };

            await drivingLicenseService.SaveDrivingLicenseInfo(data);

            return RedirectToAction(nameof(Index));
        }
    }
}