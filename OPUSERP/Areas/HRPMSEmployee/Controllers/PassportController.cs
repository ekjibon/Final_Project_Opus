using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Controllers
{
    [Authorize]
    [Area("HRPMSEmployee")]
    public class PassportController : Controller
    {
        private readonly IPassportInfoService passportInfoService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly IPhotographService photographService;
        private readonly LangGenerate<Passport> _lang;

        public PassportController(IHostingEnvironment hostingEnvironment, IPhotographService photographService, IPersonalInfoService personalInfoService, IPassportInfoService passportInfoService)
        {
            this.passportInfoService = passportInfoService;
            this.personalInfoService = personalInfoService;
            this.photographService = photographService;
            _lang = new LangGenerate<Passport>(hostingEnvironment.ContentRootPath);
        }

        // GET: Passport
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new PassportViewModel
            {
                employeeID = id.ToString(),
                photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile"),
                employeeInfo = await personalInfoService.GetEmployeeInfoById(id),
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

            PassportDetails data = new PassportDetails
            {
                Id = Int32.Parse(model.passportId),
                employeeId = Int32.Parse(model.employeeID),
                passportNumber = model.passPortNumber,
                placeOfIssue = model.place,
                dateOfIssue = model.dateOfIssue,
                dateOfExpair = model.dateOfExpair
            };

            await passportInfoService.SavePassportInfo(data);

            return RedirectToAction("Index", "Passport", new
            {
                id = Int32.Parse(model.employeeID)
            });
        }

        // Delete: Language
        public async Task<IActionResult> Delete(int id, int empId)
        {
            await passportInfoService.DeletePassportInfoById(id);
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Index", "Passport", new
            {
                id = empId
            });
        }

    }
}