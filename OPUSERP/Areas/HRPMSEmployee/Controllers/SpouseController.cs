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
using OPUSERP.HRPMS.Services.MasterData.Interfaces;

namespace OPUSERP.Areas.HRPMSEmployee.Controllers
{
    [Authorize]
    [Area("HRPMSEmployee")]
    public class SpouseController : Controller
    {
        private readonly LangGenerate<SpouseLn> _lang;
        private readonly ISpouseChildrenService spouseChildrenService;
        private readonly IPhotographService photographService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly IAddressService addressService;

        public SpouseController(IHostingEnvironment hostingEnvironment, IPhotographService photographService, IPersonalInfoService personalInfoService, ISpouseChildrenService spouseChildrenService, IAddressService addressService)
        {
            _lang = new LangGenerate<SpouseLn>(hostingEnvironment.ContentRootPath);
            this.spouseChildrenService = spouseChildrenService;
            this.personalInfoService = personalInfoService;
            this.addressService = addressService;
            this.photographService = photographService;
        }

        // GET: Spouse
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new SpouseViewModel
            {
                employeeID = id.ToString(),
                photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile"),
                employeeInfo = await personalInfoService.GetEmployeeInfoById(id),
                fLang = _lang.PerseLang("Employee/SpouseEN.json", "Employee/SpouseBN.json", Request.Cookies["lang"]),
                spouses = await spouseChildrenService.GetSpouseByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id),
                districts = await addressService.GetAllDistrict(),
            };
            if (model.spouse == null) model.spouse = new Spouse();
            return View(model);
        }

        // POST: Spouse/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] SpouseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.fLang = _lang.PerseLang("Employee/SpouseEN.json", "Employee/SpouseBN.json", Request.Cookies["lang"]);
                model.spouses = await spouseChildrenService.GetSpouseByEmpId(Int32.Parse(model.employeeID));
                model.employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(Int32.Parse(model.employeeID));
                model.districts = await addressService.GetAllDistrict();
                if (model.spouse == null) model.spouse = new Spouse();
                return View(model);
            }

            Spouse data = new Spouse
            {
                Id = Int32.Parse(model.spouseID),
                employeeId = Int32.Parse(model.employeeID),
                spouseName = model.spouseName,
                spouseNameBN = model.spouseNameBN,
                email = model.emailAddress,
                dateOfBirth = model.dateOfBirth,
                occupation = model.occupation,
                gender = model.gender,
                designation = model.designation,
                organization = model.organization,
                bin = model.bin,
                nid = model.nid,
                contact = model.contact,
                highestEducation = model.higherEducation,
                bloodGroup = model.bloodGroup,
                homeDistrict = model.homeDistrict
            };

            await spouseChildrenService.SaveSpouse(data);
            await personalInfoService.UpdateEmployeeinfoById(Int32.Parse(model.employeeID));
            return RedirectToAction(nameof(Index));
        }

        // Delete: Language
        public async Task<IActionResult> Delete(int id, int empId)
        {
            await spouseChildrenService.DeleteSpouseById(id);
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Index", "Spouse", new
            {
                id = empId
            });
        }

    }
}