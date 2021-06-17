using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.SCMSupplier.Models;
using OPUSERP.Data.Entity.Master;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.SCM.Data.Entity.Supplier;
using OPUSERP.SCM.Services.MasterData.Interfaces;
using OPUSERP.SCM.Services.Supplier.Interface;

namespace OPUSERP.Areas.SCMSupplier.Controllers
{
    [Area("SCMSupplier")]
    public class SupplierController : Controller
    {
        private readonly SCM.Services.Supplier.Interface.IAddressService addressService;
        private readonly IItemsService itemsService;
        private readonly IContactService contactService;
        private readonly IOrganizationService organizationService;
        private readonly ISupplierItemInfoService supplierItemInfoService;
        private readonly IAddressTypeService addressTypeService;
        private readonly ERPServices.MasterData.Interfaces.IAddressService countryService;

        public SupplierController(SCM.Services.Supplier.Interface.IAddressService addressService, IContactService contactService, IOrganizationService organizationService, ISupplierItemInfoService supplierItemInfoService, IItemsService itemsService, IAddressTypeService addressTypeService, ERPServices.MasterData.Interfaces.IAddressService countryService)
        {
            this.addressService = addressService;
            this.contactService = contactService;
            this.organizationService = organizationService;
            this.supplierItemInfoService = supplierItemInfoService;
            this.itemsService = itemsService;
            this.addressTypeService = addressTypeService;
            this.countryService = countryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? id, string actionType)
        {
            var countrydata = await countryService.GetAllContry();
            int bangladeshcountryId = countrydata.Where(a => a.countryName == "Bangladesh").FirstOrDefault().Id;

            if (actionType == string.Empty || actionType == null)
            {
                actionType = "defaultOpen";
            }

            int viid = Convert.ToInt32(id);
            int relSupplierCustomerResourseId = 0;
            var suppCode = "";
            if (viid != 0)
            {
                var relSupplierCustomerResourse = await organizationService.GetOrganizationById(viid);
                relSupplierCustomerResourseId = relSupplierCustomerResourse.Id;
                suppCode = relSupplierCustomerResourse.orgCode;
            }
            else
            {
                suppCode = await organizationService.GetAutoOrganizationCode();
            }
            ViewBag.OrgCode = suppCode;
            try
            {
                var orgAddress = await addressService.GetAddressByOrganizationId(viid);
                if (orgAddress == null)
                {
                    orgAddress = new Address();
                    orgAddress.division = new Division();
                    orgAddress.district = new District();
                    orgAddress.thana = new Thana();
                }
                ViewBag.addcount = orgAddress;
                SupplierViewModel model = new SupplierViewModel
                {
                    //orgCode = await organizationService.GetAutoOrganizationCode(),
                    items = await itemsService.GetAllItem(),
                    GetOrganizationbyId = await organizationService.GetOrganizationById(viid),
                    GetAddress = orgAddress,
                    relSupplierCustomerResourseId = relSupplierCustomerResourseId,
                    addressTypes = await addressTypeService.GetAllAddressType()
                };
                model.actionType = actionType;
                if (model.GetOrganizationbyId == null) model.GetOrganizationbyId = new Organization();
                ViewBag.bangladeshcountryId = bangladeshcountryId;

                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] SupplierViewModel model)
        {
            var countrydata = await countryService.GetAllContry();
            int bangladeshcountryId = countrydata.Where(a => a.countryName == "Bangladesh").FirstOrDefault().Id;

            if (model.relSupplierCustomerResourseId <= 0)
            {
                var Sdata = await organizationService.GetorganizationBysupplierName(model.nameEnglish);

                if (Sdata != null)
                {
                    var orgAddress = await addressService.GetAddressByOrganizationId((int)model.organizationId);
                    if (orgAddress == null)
                    {
                        orgAddress = new Address();
                        orgAddress.division = new Division();
                        orgAddress.district = new District();
                        orgAddress.thana = new Thana();
                    }
                    ViewBag.addcount = orgAddress;
                    model.GetOrganizationbyId = await organizationService.GetOrganizationById((int)model.organizationId);
                    if (model.GetOrganizationbyId == null) model.GetOrganizationbyId = new Organization();
                    model.actionType = "defaultOpen";
                    model.addressTypes = await addressTypeService.GetAllAddressType();
                    ModelState.AddModelError(string.Empty, "This Supplier is already exist");
                    return View(model);
                    //return RedirectToAction("Index", "SupplierInfo", new { id =0, actionType=string.Empty, Area = "MasterData" });
                }
            }

            var relresource = model.relSupplierCustomerResourseId;

            try
            {
                string userId = HttpContext.User.Identity.Name;
                string orgCode = "";
                if (model.organizationId == 0)
                {
                    orgCode = await organizationService.GetAutoOrganizationCode();
                }
                else
                {
                    orgCode = model.orgCode;
                }
                Organization orgData = new Organization
                {
                    Id = (int)model.organizationId,
                    organizationName = model.nameEnglish,
                    orgCode = orgCode,
                    LicenseNumber = model.LicenseNumber,
                    eTinNumber = model.eTinNumber,
                    VATNumber = model.VATNumber,
                    email = model.email,
                    alternativeEmail = model.alternativeEmail,
                    mobile = model.mobile,
                    phone = model.phone
                };

                int rid = await organizationService.SaveOrganization(orgData);

                if (model.organizationId != 0)
                {
                    await supplierItemInfoService.DeleteSupplierItemInfoByOrganizationId(rid);
                    await addressService.DeleteAddressByOrganizationId(rid);
                    await contactService.DeleteContactByOrganizationId(rid);
                }
                if (model.phoneall != null)
                {
                    for (int i = 0; i < model.phoneall.Count(); i++)
                    {
                        Contact contact = new Contact
                        {
                            Id = 0,
                            organizationId = rid,
                            personName = model.contactall[i],
                            Department = model.deptall[i],
                            Designation = model.desall[i],
                            phoneNumber = model.phoneall[i],
                            mobileNumber = model.mobileall[i]
                        };
                        int specid = await contactService.SaveContact(contact);
                    }
                }
            if (model.addressCategoryId != null)
            {
                for (int j = 0; j < model.addressCategoryId.Length; j++)
                    {
                        Address address = new Address
                        {
                            Id = 0,
                            organizationId = rid,
                            countryId = bangladeshcountryId,
                            addressTypeId = model.addressCategoryId[j],
                            divisionId = Convert.ToInt32(model.orgDivision[j]),
                            districtId = Convert.ToInt32(model.orgDistrict[j]),
                            thanaId = Convert.ToInt32(model.orgUpazila[j]),
                            union = model.orgUnion[j],
                            postOffice = model.orgPostOffice[j],
                            postCode = model.orgPostCode[j],
                            blockSector = model.orgBlockSector[j],
                            houseVillage = model.orgHouseVillage[j],

                            type = "Supplier"
                        };
                        await addressService.SaveAddress(address);
                    }

            }


            if (model.itemidall != null)
                {
                    for (int i = 0; i < model.itemidall.Count(); i++)
                    {
                        SupplierItemInfo itemInfo = new SupplierItemInfo
                        {
                            Id = 0,
                            organizationId = rid,
                            itemId = model.itemidall[i]
                        };
                        int vwtId = await supplierItemInfoService.SaveSupplierItemInfo(itemInfo);
                    }
                }

                return RedirectToAction("SupplierList");
        }
            catch (Exception ex)
            {
                throw ex;
            }

}

        // GET: SupplierInfo
        public async Task<IActionResult> SupplierList()
        {
            SupplierViewModel model = new SupplierViewModel
            {
                organizations = await organizationService.GetAllOrganization()
            };
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> GetcontactbyOrganizationId(int Id)
        {
            var contactlist = await contactService.GetcontactbyOrganizationId(Id);
            return Json(contactlist);
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerItemInfobyOrganizationId(int Id)
        {
            var contactlist = await supplierItemInfoService.GetSupplierItemInfobyOrganizationId(Id);
            return Json(contactlist);
        }

        [Route("global/api/supplier/GetAddressListByOrganizationId/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAddressListByOrganizationId(int id)
        {
            return Json(await addressService.GetAddressListByOrganizationId(id));
        }

        [Route("global/api/supplier/GetAllSupplier/")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllOrganization()
        {
            return Json(await organizationService.GetAllOrganization());
        }
    }
}