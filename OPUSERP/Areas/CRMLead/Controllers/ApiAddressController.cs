using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.CRMLead.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Services.Lead.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;

namespace OPUSERP.Areas.CRMLead.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ApiAddressController : Controller
    {
        private readonly IAddressesService addressesService;
        private readonly IAddressService addressService;
        private readonly IAddressTypeService addressTypeService;
        private readonly ILeadsService leadsService;

        public ApiAddressController(IAddressesService addressesService, IAddressService addressService, IAddressTypeService addressTypeService, ILeadsService leadsService)
        {
            this.addressesService = addressesService;
            this.addressService = addressService;
            this.addressTypeService = addressTypeService;
            this.leadsService = leadsService;
        }
        
        // GET: Address
        public async Task<IActionResult> Index(int? id, string leadName, int addressTypeId)
        {
           
            int LID = Convert.ToInt32(id);
            var model = new AddressViewModel
            {
                districts = await addressService.GetAllDistrict(),
                divisions = await addressService.GetAllDivision(),
                Addresses = await addressesService.GetAddressesByLeadId(LID),
                addressTypes = await addressTypeService.GetAllAddressType(),
                //company = await addressService.GetAddressByType(LID),
            };
            //if (model.company == null) model.company = new Data.Entity.Lead.Addresses();
            //if (model.factory == null) model.factory = new Data.Entity.Lead.Addresses();
            if (model.company == null) model.company = new Address();
            model.company.divisionId = model.company.divisionId == null ? 0 : model.company.divisionId;
            model.company.districtId = model.company.districtId == null ? 0 : model.company.districtId;
            ViewBag.leadName = leadName;
            ViewBag.leadId = id;
            ViewBag.addressTypeId = addressTypeId;
            return View(model);
        }
        

        // POST: Address/Create
        [HttpPost]
        public async Task<IActionResult> Index([FromBody] AddressViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.districts = await addressService.GetAllDistrict();
                model.divisions = await addressService.GetAllDivision();
                model.thanas = await addressService.GetAllThana();
                model.addressTypes = await addressTypeService.GetAllAddressType();
                //model.company = await addressService.GetAddressByType( model.leadId);
                model.Addresses = await addressesService.GetAddressesByLeadId(model.leadId);

                if (model.company == null) model.company = new Address();

                ViewBag.leadName = model.leadName;
                ViewBag.leadId = Convert.ToInt32(model.leadId);
                ViewBag.addressTypeId = model.addressTypeId;
                return View(model);
            }

            Address companydata = new Address
            {
                Id = Convert.ToInt32(model.Id),
                addressTypeId = Convert.ToInt32(model.addressTypeId),
                leadsId = Convert.ToInt32(model.leadId),
                maillingAddress = model.mailingAddress,
                divisionId = model.divisionId,
                districtId = model.districtId,
                thanaId = Convert.ToInt32(model.thanaId),
                postOfficeName=model.postOfficeName,
                email = model.email,
                mobile = model.mobile,
                website = model.website,
                phone = model.phone,
                fax = model.fax
            };

            await addressesService.SaveAddresses(companydata);

            #region Save History
            var addressTypeList = await addressTypeService.GetAddressTypeById(Convert.ToInt32(model.addressTypeId));
            string actDetailss = string.Empty;
            if (model.Id == 0)
            {
                actDetailss = ""+ addressTypeList.typeName+ " Address is Created.";
            }
            else
            {
                actDetailss = "" + addressTypeList.typeName + " Address is Updated.";
            }

            LeadsHistory leadsData = new LeadsHistory
            {
                Id = 0,
                leadsId = Convert.ToInt32(model.leadId),
                actionName = "Address",
                actionDetails = actDetailss
            };
            await leadsService.SaveLeadsHistory(leadsData);
            #endregion

            return new OkObjectResult("Saved Successfully!!");
        }
        // GET:Client Address
        public async Task<IActionResult> ClientAddress(int? id, string leadName, int addressTypeId)
        {

            int LID = Convert.ToInt32(id);
            var model = new AddressViewModel
            {
                districts = await addressService.GetAllDistrict(),
                divisions = await addressService.GetAllDivision(),
                Addresses = await addressesService.GetAddressesByLeadId(LID),
                addressTypes = await addressTypeService.GetAllAddressType(),
                //company = await addressService.GetAddressByType(LID),
            };
            //if (model.company == null) model.company = new Data.Entity.Lead.Addresses();
            //if (model.factory == null) model.factory = new Data.Entity.Lead.Addresses();
            if (model.company == null) model.company = new Address();
            model.company.divisionId = model.company.divisionId == null ? 0 : model.company.divisionId;
            model.company.districtId = model.company.districtId == null ? 0 : model.company.districtId;
            ViewBag.leadName = leadName;
            ViewBag.leadId = id;
            ViewBag.addressTypeId = addressTypeId;
            return View(model);
        }
        // POST: Address/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClientAddress([FromForm] AddressViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.districts = await addressService.GetAllDistrict();
                model.divisions = await addressService.GetAllDivision();
                model.thanas = await addressService.GetAllThana();
                model.addressTypes = await addressTypeService.GetAllAddressType();
                //model.company = await addressService.GetAddressByType( model.leadId);
                model.Addresses = await addressesService.GetAddressesByLeadId(model.leadId);

                if (model.company == null) model.company = new Address();

                ViewBag.leadName = model.leadName;
                ViewBag.leadId = Convert.ToInt32(model.leadId);
                ViewBag.addressTypeId = model.addressTypeId;
                return View(model);
            }

            Address companydata = new Address
            {
                Id = Convert.ToInt32(model.Id),
                addressTypeId = Convert.ToInt32(model.addressTypeId),
                leadsId = Convert.ToInt32(model.leadId),
                maillingAddress = model.mailingAddress,
                divisionId = model.divisionId,
                districtId = model.districtId,
                thanaId = Convert.ToInt32(model.thanaId),
                email = model.email,
                mobile = model.mobile,
                website = model.website,
                phone = model.phone,
                fax = model.fax
            };

            await addressesService.SaveAddresses(companydata);
            return RedirectToAction(nameof(ClientAddress), new
            {
                id = Convert.ToInt32(model.leadId),
                leadName = model.leadName
            });
        }
    }
}