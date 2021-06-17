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
    public class ChildrenController : Controller
    {
        private readonly LangGenerate<ChildrenLn> _lang;
        private readonly ISpouseChildrenService spouseChildrenService;
        private readonly IPhotographService photographService;
        private readonly IPersonalInfoService personalInfoService;

        public ChildrenController(IHostingEnvironment hostingEnvironment, IPhotographService photographService, IPersonalInfoService personalInfoService, ISpouseChildrenService spouseChildrenService)
        {
            _lang = new LangGenerate<ChildrenLn>(hostingEnvironment.ContentRootPath);
            this.spouseChildrenService = spouseChildrenService;
            this.personalInfoService = personalInfoService;
            this.photographService = photographService;
        }

        // GET: Children
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new ChildrenViewModel
            {
                employeeID = id.ToString(),
                photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile"),
                employeeInfo = await personalInfoService.GetEmployeeInfoById(id),
                fLang = _lang.PerseLang("Employee/ChildrenEN.json", "Employee/ChildrenBN.json", Request.Cookies["lang"]),
                children = await spouseChildrenService.GetChildrenByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            return View(model);
        }

        // POST: Children/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ChildrenViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.children = await spouseChildrenService.GetChildrenByEmpId(Int32.Parse(model.childrenID));
                model.fLang = _lang.PerseLang("Employee/ChildrenEN.json", "Employee/ChildrenBN.json", Request.Cookies["lang"]);
                return View(model);
            }      

            Children data = new Children
            {
                Id = Int32.Parse(model.childrenID),
                employeeId = Int32.Parse(model.employeeID),
                childName = model.childName,
                childNameBN = model.childNameBN,
                dateOfBirth = model.dateOfBirth,
                education = model.education,
                occupation = model.occupation,
                gender = model.gender,
                designation = model.designation,
                organization = model.organization,
                bin = model.bin,
                nid = model.nid,
                bloodGroup = model.bloodGroup, 
                disability=model.disability,
                disablityType=model.disablityType
            };

            await spouseChildrenService.SaveChildren(data);
            await personalInfoService.UpdateEmployeeinfoById(Int32.Parse(model.employeeID));
            return RedirectToAction(nameof(Index));
        }

        // Delete: Language
        public async Task<IActionResult> Delete(int id, int empId)
        {
            await spouseChildrenService.DeleteChildrenById(id);
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Index", "Children", new
            {
                id = empId
            });
        }

    }
}