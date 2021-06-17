using OPUSERP.CLUB.Services.Member.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Helpers;
using System;
using System.Threading.Tasks;
using OPUSERP.CLUB.Data.Member;
using OPUSERP.Areas.MemberInfo.Models.Lang;
using OPUSERP.Areas.MemberInfo.Models;

namespace OPUSERP.Areas.MemberInfo.Controllers
{
    [Authorize]
    [Area("MemberInfo")]
    public class AddressController : Controller
    {
        private readonly LangGenerate<Address> _lang;
        private readonly IMemberInfoService employeeInfoService;
        private readonly IPersonalInfoService personalInfoService;

        public AddressController(IHostingEnvironment hostingEnvironment, IPersonalInfoService personalInfoService, IMemberInfoService employeeInfoService)
        {
            _lang = new LangGenerate<Address>(hostingEnvironment.ContentRootPath);
            this.employeeInfoService = employeeInfoService;
            this.personalInfoService = personalInfoService;
        }
        
        // GET: Address
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new AddressViewModel
            {
                employeeID = id,
                fLang = _lang.PerseLang("Employee/AddressEN.json", "Employee/AddressBN.json", Request.Cookies["lang"]),
                permanent = await employeeInfoService.GetAddressByTypeAndEmpId(id, "permanent"),
                present = await employeeInfoService.GetAddressByTypeAndEmpId(id, "present"),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            if (model.permanent == null) model.permanent = new MemberAddress();
            if (model.present == null) model.present = new MemberAddress();
            model.present.divisionId = model.present.divisionId ?? 0;
            model.present.districtId = model.present.districtId ?? 0;

            model.permanent.divisionId = model.present.divisionId ?? 0;
            model.permanent.districtId = model.present.districtId ?? 0;

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
                if (model.permanent == null) model.permanent = new MemberAddress();
                if (model.present == null) model.present = new MemberAddress();

                model.present.divisionId = model.present.divisionId ?? 0;
                model.present.districtId = model.present.districtId ?? 0;
                model.permanent.divisionId = model.present.divisionId ?? 0;
                model.permanent.districtId = model.present.districtId ?? 0;

                return View(model);
            }

            MemberAddress presentdata = new MemberAddress
            {
                Id = model.presentAddressID,
                employeeId = model.employeeID,
                countryId =1,
                divisionId = model.presentDivision,
                districtId = model.presentDistrict,
                thanaId = model.presentUpazila,
                union = model.presentUnion,
                postOffice = model.presentPostOffice,
                postCode = model.presentPostCode,
                blockSector = model.presentBlockSector,
                houseVillage = model.presentHouseVillage,
                roadNumber = model.presentRoadNo,
                type = "present"

            };

            MemberAddress permanentdata = new MemberAddress
            {
                Id = model.permanentAddressID,
                employeeId = model.employeeID,
                countryId = 1,
                divisionId = model.permanentDivision,
                districtId = model.permanentDistrict,
                thanaId = model.permanentUpazila,
                union = model.permanentUnion,
                postOffice = model.permanentPostOffice,
                postCode = model.permanentPostCode,
                blockSector = model.permanentBlockSector,
                houseVillage = model.permanentHouseVillage,
                roadNumber = model.permanentRoadNo,
                type = "permanent"

            };

            await employeeInfoService.SaveAddress(presentdata);
            await employeeInfoService.SaveAddress(permanentdata);

            return RedirectToAction(nameof(Index));
        }

    }
}