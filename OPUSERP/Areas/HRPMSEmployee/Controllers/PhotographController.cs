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
    public class PhotographController : Controller
    {
        private readonly LangGenerate<EmployeeInfoLn> _lang;
        private readonly IPhotographService photographService;
        private readonly IWagesPhotographService wagesPhotographService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly IWagesPersonalInfoService wagesPersonalInfoService;

        public PhotographController(IHostingEnvironment hostingEnvironment, IWagesPhotographService wagesPhotographService, IPhotographService photographService, IPersonalInfoService personalInfoService, IWagesPersonalInfoService wagesPersonalInfoService)
        {
            _lang = new LangGenerate<EmployeeInfoLn>(hostingEnvironment.ContentRootPath);
            this.photographService = photographService;
            this.personalInfoService = personalInfoService;
            this.wagesPhotographService = wagesPhotographService;
            this.wagesPersonalInfoService = wagesPersonalInfoService;
        }

        // GET: Photograph
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            PhotographViewModel model = new PhotographViewModel
            {
                employeeInfo = await personalInfoService.GetEmployeeInfoById(id),
                fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]),
                photograph = await photographService.GetPhotographByEmpIdAndType(id,"profile"),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            if (model.photograph == null) model.photograph = new Photograph();
            return View(model);
        }

        // GET: Photograph
        public async Task<IActionResult> Signature(int id)
        {
            ViewBag.employeeID = id.ToString();
            PhotographViewModel model = new PhotographViewModel
            {
                photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile"),
                employeeInfo = await personalInfoService.GetEmployeeInfoById(id),
                fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]),
                employeeSignature = await photographService.GetEmployeeSignatureByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            if (model.employeeSignature == null) model.employeeSignature = new EmployeeSignature();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Signature([FromForm] PhotographViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.employeeSignature = await photographService.GetEmployeeSignatureByEmpId(model.employeeID);
                model.employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(model.employeeID);
                if (model.employeeSignature == null) model.employeeSignature = new EmployeeSignature();
                return View(model);
            }

            string fileName;
            string message = FileSave.SaveImage(out fileName, model.empPhoto);

            if (message == "success")
            {
                EmployeeSignature data = new EmployeeSignature
                {
                    Id = model.photographID,
                    employeeId = model.employeeID,
                    url = fileName,
                };
                await photographService.SaveEmployeeSignature(data);
            }

            //return RedirectToAction(nameof(Signature));
            return RedirectToAction("Signature", "Photograph", new
            {
                id = model.employeeID
            });
        }

        // GET: Photograph
        public async Task<IActionResult> WagesIndex(int id)
        {
            ViewBag.employeeID = id.ToString();
            PhotographViewModel model = new PhotographViewModel
            {
                fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]),
                wagesPhotograph = await wagesPhotographService.GetPhotographByEmpIdAndType(id, "profile"),
                employeeNameCode = await wagesPersonalInfoService.GetEmployeeNameCodeById(id)
            };
            if (model.wagesPhotograph == null) model.wagesPhotograph = new WagesPhotograph();
            return View(model);
        }

        public async Task<IActionResult> EditGrid(int id)
        {
            ViewBag.employeeID = id.ToString();
            PhotographViewModel model = new PhotographViewModel
            {
                photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile"),
                employeeInfo = await personalInfoService.GetEmployeeInfoById(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id),
                visualEmpCodeName = await personalInfoService.GetEmpCodeNameVisualData()
            };
            if (model.photograph == null) model.photograph = new Photograph();
            return View(model);
        }

        public async Task<IActionResult> WagesEditGrid(int id)
        {
            ViewBag.employeeID = id.ToString();
            PhotographViewModel model = new PhotographViewModel
            {
                wagesPhotograph = await wagesPhotographService.GetPhotographByEmpIdAndType(id, "profile"),
                wagesEmployeeInfo = await wagesPersonalInfoService.GetEmployeeInfoById(id),
                employeeNameCode = await wagesPersonalInfoService.GetEmployeeNameCodeById(id)
            };
            if (model.wagesPhotograph == null) model.wagesPhotograph = new WagesPhotograph();
            return View(model);
        }

        // POST: Photograph/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([FromForm] PhotographViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.photograph = await photographService.GetPhotographByEmpIdAndType(model.employeeID, "profile");
                model.employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(model.employeeID);
                if (model.photograph == null) model.photograph = new Photograph();
                return View(model);
            }

            string fileName;
            string message = FileSave.SaveImage(out fileName, model.empPhoto);
            
            if(message == "success")
            {  
                Photograph data = new Photograph
                {
                    Id = model.photographID,
                    employeeId = model.employeeID,
                    url = fileName,
                    type = "profile"
                };
                await photographService.SavePhotograph(data);
            }

            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Index", "Photograph", new
            {
                id = model.employeeID
            });
        }

        // POST: Photograph/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> WagesIndex([FromForm] PhotographViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.wagesPhotograph = await wagesPhotographService.GetPhotographByEmpIdAndType(model.employeeID, "profile");
                model.employeeNameCode = await wagesPersonalInfoService.GetEmployeeNameCodeById(model.employeeID);
                if (model.wagesPhotograph == null) model.wagesPhotograph = new WagesPhotograph();
                return View(model);
            }

            string fileName;
            string message = FileSave.SaveImage(out fileName, model.empPhoto);

            if (message == "success")
            {
                WagesPhotograph data = new WagesPhotograph
                {
                    Id = model.photographID,
                    employeeId = model.employeeID,
                    url = fileName,
                    type = "profile"
                };
                await wagesPhotographService.SavePhotograph(data);
            }

            return RedirectToAction(nameof(WagesIndex));
        }

    }
}