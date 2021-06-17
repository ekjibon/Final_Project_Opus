using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Master;
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
    public class AddressController : Controller
    {
        private readonly LangGenerate<Address> _lang;
        private readonly IEmployeeInfoService employeeInfoService;
        private readonly IWagesEmployeeInfoService wagesEmployeeInfoService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly IPhotographService photographService;
        private readonly IWagesPersonalInfoService wagesPersonalInfoService;

        public AddressController(IHostingEnvironment hostingEnvironment, IPhotographService photographService, IWagesEmployeeInfoService wagesEmployeeInfoService, IPersonalInfoService personalInfoService, IEmployeeInfoService employeeInfoService, IWagesPersonalInfoService wagesPersonalInfoService)
        {
            _lang = new LangGenerate<Address>(hostingEnvironment.ContentRootPath);
            this.employeeInfoService = employeeInfoService;
            this.personalInfoService = personalInfoService;
            this.photographService = photographService;
            this.wagesEmployeeInfoService = wagesEmployeeInfoService;
            this.wagesPersonalInfoService = wagesPersonalInfoService;
        }
        
        // GET: Address
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new AddressViewModel
            {
                employeeID = id,
                photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile"),
                employeeInfo = await personalInfoService.GetEmployeeInfoById(id),
                fLang = _lang.PerseLang("Employee/AddressEN.json", "Employee/AddressBN.json", Request.Cookies["lang"]),
                permanent = await employeeInfoService.GetAddressByTypeAndEmpId(id, "permanent"),
                present = await employeeInfoService.GetAddressByTypeAndEmpId(id, "present"),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            if (model.permanent == null) model.permanent = new HRPMS.Data.Entity.Employee.Address();
            if (model.present == null) model.present = new HRPMS.Data.Entity.Employee.Address();
            model.present.divisionId = model.present.divisionId ?? 0;
            model.present.districtId = model.present.districtId ?? 0;

            model.permanent.divisionId = model.permanent.divisionId ?? 0;
            model.permanent.districtId = model.permanent.districtId ?? 0;

            //model.permanent.division = new Division();
            //model.permanent.district = new District();

            //model.permanent.division.Id = model.present.division.Id ?? 0;
            //model.permanent.district.Id = model.present.district.Id ?? 0;

            return View(model);
        }

        // GET: Address
        public async Task<IActionResult> WagesIndex(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new AddressViewModel
            {
                employeeID = id,
                fLang = _lang.PerseLang("Employee/AddressEN.json", "Employee/AddressBN.json", Request.Cookies["lang"]),
                wagesPermanent = await wagesEmployeeInfoService.GetAddressByTypeAndEmpId(id, "permanent"),
                wagesPresent = await wagesEmployeeInfoService.GetAddressByTypeAndEmpId(id, "present"),
                employeeNameCode = await wagesPersonalInfoService.GetEmployeeNameCodeById(id)
            };
            if (model.wagesPermanent == null) model.wagesPermanent = new HRPMS.Data.Entity.Employee.WagesAddress();
            if (model.wagesPresent == null) model.wagesPresent = new HRPMS.Data.Entity.Employee.WagesAddress();
            model.wagesPresent.divisionId = model.wagesPresent.divisionId ?? 0;
            model.wagesPresent.districtId = model.wagesPresent.districtId ?? 0;

            model.wagesPermanent.divisionId = model.wagesPermanent.divisionId ?? 0;
            model.wagesPermanent.districtId = model.wagesPermanent.districtId ?? 0;

            return View(model);
        }

        // POST: Address/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] AddressViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.present = await employeeInfoService.GetAddressByTypeAndEmpId(model.employeeID, "present");
                model.permanent = await employeeInfoService.GetAddressByTypeAndEmpId(model.employeeID, "permanent");
                model.fLang = _lang.PerseLang("Employee/AddressEN.json", "Employee/AddressBN.json", Request.Cookies["lang"]);
                if (model.permanent == null) model.permanent = new HRPMS.Data.Entity.Employee.Address();
                if (model.present == null) model.present = new HRPMS.Data.Entity.Employee.Address();

                model.present.divisionId = model.present.divisionId ?? 0;
                model.present.districtId = model.present.districtId ?? 0;
                model.permanent.divisionId = model.permanent.divisionId ?? 0;
                model.permanent.districtId = model.permanent.districtId ?? 0;

                return View(model);
            }

            HRPMS.Data.Entity.Employee.Address presentdata = new HRPMS.Data.Entity.Employee.Address
            {
                Id = model.presentAddressID,
                employeeId = model.employeeID,
                countryId =1,
                divisionId = Int32.Parse(model.presentDivision),
                districtId = Int32.Parse(model.presentDistrict),
                thanaId = Int32.Parse(model.presentUpazila),
                union = model.presentUnion,
                postOffice = model.presentPostOffice,
                postCode = model.presentPostCode,
                blockSector = model.presentBlockSector,
                houseVillage = model.presentHouseVillage,
                roadNumber = model.presentRoadNo,
                type = "present"
            };
            await employeeInfoService.SaveAddress(presentdata);

            HRPMS.Data.Entity.Employee.Address permanentdata = new HRPMS.Data.Entity.Employee.Address
            {
                Id = model.permanentAddressID,
                employeeId = model.employeeID,
                countryId = 1,
                divisionId = Int32.Parse(model.permanentDivision),
                districtId = Int32.Parse(model.permanentDistrict),
                thanaId = Int32.Parse(model.permanentUpazila),
                union = model.permanentUnion,
                postOffice = model.permanentPostOffice,
                postCode = model.permanentPostCode,
                blockSector = model.permanentBlockSector,
                houseVillage = model.permanentHouseVillage,
                roadNumber = model.permanentRoadNo,
                type = "permanent"
            };
            await employeeInfoService.SaveAddress(permanentdata);
            await personalInfoService.UpdateEmployeeinfoById(model.employeeID);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WagesIndex([FromForm] AddressViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.wagesPresent = await wagesEmployeeInfoService.GetAddressByTypeAndEmpId(model.employeeID, "present");
                model.wagesPermanent = await wagesEmployeeInfoService.GetAddressByTypeAndEmpId(model.employeeID, "permanent");
                model.fLang = _lang.PerseLang("Employee/AddressEN.json", "Employee/AddressBN.json", Request.Cookies["lang"]);
                if (model.wagesPermanent == null) model.wagesPermanent = new HRPMS.Data.Entity.Employee.WagesAddress();
                if (model.wagesPresent == null) model.wagesPresent = new HRPMS.Data.Entity.Employee.WagesAddress();
                model.wagesPresent.divisionId = model.wagesPresent.divisionId ?? 0;
                model.wagesPresent.districtId = model.wagesPresent.districtId ?? 0;

                model.wagesPermanent.divisionId = model.wagesPermanent.divisionId ?? 0;
                model.wagesPermanent.districtId = model.wagesPermanent.districtId ?? 0;

                return View(model);
            }

            HRPMS.Data.Entity.Employee.WagesAddress presentdata = new HRPMS.Data.Entity.Employee.WagesAddress
            {
                Id = model.presentAddressID,
                employeeId = model.employeeID,
                countryId = 1,
                divisionId = Int32.Parse(model.presentDivision),
                districtId = Int32.Parse(model.presentDistrict),
                thanaId = Int32.Parse(model.presentUpazila),
                union = model.presentUnion,
                postOffice = model.presentPostOffice,
                postCode = model.presentPostCode,
                blockSector = model.presentBlockSector,
                houseVillage = model.presentHouseVillage,
                roadNumber = model.presentRoadNo,
                type = "present"
            };
            await wagesEmployeeInfoService.SaveAddress(presentdata);

            HRPMS.Data.Entity.Employee.WagesAddress permanentdata = new HRPMS.Data.Entity.Employee.WagesAddress
            {
                Id = model.permanentAddressID,
                employeeId = model.employeeID,
                countryId = 1,
                divisionId = Int32.Parse(model.permanentDivision),
                districtId = Int32.Parse(model.permanentDistrict),
                thanaId = Int32.Parse(model.permanentUpazila),
                union = model.permanentUnion,
                postOffice = model.permanentPostOffice,
                postCode = model.permanentPostCode,
                blockSector = model.permanentBlockSector,
                houseVillage = model.permanentHouseVillage,
                roadNumber = model.permanentRoadNo,
                type = "permanent"
            };
            await wagesEmployeeInfoService.SaveAddress(permanentdata);
            await wagesPersonalInfoService.UpdateEmployeeinfoById(model.employeeID);
            return RedirectToAction(nameof(WagesIndex));
        }

    }
}