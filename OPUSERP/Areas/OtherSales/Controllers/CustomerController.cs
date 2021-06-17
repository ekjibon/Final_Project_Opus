using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using OPUSERP.Accounting.Services.AccountingSettings.Interfaces;
using OPUSERP.Areas.OtherSales.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Services.Lead.Interfaces;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.Data.Entity.Master;
using OPUSERP.Distribution.Services.MasterData.Interfaces;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.OtherSales.Services.Sales.Interfaces;
using OPUSERP.Rental.Services.Sales.Interfaces;
using OPUSERP.SCM.Data.Entity.Requisition;
using OPUSERP.SCM.Data.Entity.Supplier;
using OPUSERP.SCM.Services.MasterData.Interfaces;

namespace OPUSERP.Areas.OtherSales.Controllers
{
    [Area("OtherSales")]
    public class CustomerController : Controller
    {
        //Customer
        private readonly ICustomerService customerService;
        private readonly IAddressService addressService;
        private readonly IItemsService itemsService;
        private readonly IAttachmentCommentService attachmentCommentService;
        private readonly IAddressCategoryService addressCategoryService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILedgerService ledgerService;
        private readonly IContactsService contactsService;
        private readonly IResourceService resourceService;
        private readonly ILeadsService leadsService;
        private readonly IERPModuleService eRPModuleService;
        private readonly IDistributorTypeService distributorTypeService;
        private readonly IOSalesInvoiceMasterService salesInvoiceMasterService;
        private readonly IRentInvoiceMasterService rentInvoiceMasterService;
        private readonly IRentInvoiceDetailService rentInvoiceDetailService;


        public CustomerController(IHostingEnvironment hostingEnvironment, IRentInvoiceMasterService rentInvoiceMasterService, IRentInvoiceDetailService rentInvoiceDetailService, IOSalesInvoiceMasterService salesInvoiceMasterService, IDistributorTypeService distributorTypeService, IERPModuleService eRPModuleService, ILeadsService leadsService, IContactsService contactsService, IResourceService resourceService, ILedgerService ledgerService, ICustomerService customerService, IAddressService addressService, IItemsService itemsService, IAttachmentCommentService attachmentCommentService, IAddressCategoryService addressCategoryService)
        {
            this.customerService = customerService;
            this.addressService = addressService;
            this.itemsService = itemsService;
            this.attachmentCommentService = attachmentCommentService;
            this.addressCategoryService = addressCategoryService;
            this.ledgerService = ledgerService;
            this.contactsService = contactsService;
            this.resourceService = resourceService;
            this.leadsService = leadsService;
            this.salesInvoiceMasterService = salesInvoiceMasterService;
            this.eRPModuleService = eRPModuleService;
            this.rentInvoiceMasterService = rentInvoiceMasterService;
            this.rentInvoiceDetailService = rentInvoiceDetailService;
            this.distributorTypeService = distributorTypeService;
            this._hostingEnvironment = hostingEnvironment;
        }

        #region Customer
        
        [HttpGet]
        public async Task<IActionResult> Index(int? id, string actionType)
        {
            if (actionType == string.Empty || actionType == null)
            {
                actionType = "defaultOpen";
            }
            int viid = Convert.ToInt32(id);
            int relSupplierCustomerResourseId = 0;
            if (viid != 0)
            {
                var relSupplierCustomerResourse = await customerService.GetRelSupplierCustomerResourseByResourceId(viid);
                relSupplierCustomerResourseId = relSupplierCustomerResourse.Id;
            }
            try
            {
                var orgAddress = await addressService.GetAddressByResourceId(viid);
                if (orgAddress == null)
                {
                    orgAddress = new CRM.Data.Entity.Lead.Address();
                    orgAddress.division = new Division();
                    orgAddress.district = new District();
                    orgAddress.thana = new Thana();
                }
                ViewBag.addcount = orgAddress;
                CustomerViewModel model = new CustomerViewModel
                {
                    organizations = await customerService.GetAllOrganization(),
                    items = await itemsService.GetAllItem(),
                    GetResourcebyId = await customerService.GetResourceById(viid),
                    GetAddress = orgAddress,
                    relSupplierCustomerResourseId = relSupplierCustomerResourseId,
                    addressCategories=await addressCategoryService.GetAddressCategory()
                };
                model.actionType = actionType;
                if (model.GetResourcebyId == null) model.GetResourcebyId = new Resource();


                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] CustomerViewModel model, IFormFile formFile)
        {
            //var Org = model.orgDivision;
            //var Contactcount = model.contactall;
            //var itemcount = model.itemId;
            var relresource = model.relSupplierCustomerResourseId;

            var modulename = await eRPModuleService.GetERPModuleById((int)model.moduleId);

            var distributortype = await distributorTypeService.GetAllDistributorType();

            int distributortypeId = 0;
            if(distributortype!=null)
            {
                distributortypeId = distributortype.Where(x => x.name == modulename.moduleName).FirstOrDefault().Id;
            }
            
            //return Json(model);

            try
            {
                string userId = HttpContext.User.Identity.Name;

                Resource resorucedata = new Resource
                {
                    Id = Convert.ToInt32(model.resourceId),
                    nameEnglish = model.nameEnglish,
                    resourceName=model.nameEnglish,
                    LicenseNumber = model.LicenseNumber,
                    eTinNumber = model.eTinNumber,
                    binNumber = model.binNumber,
                    VATNumber = model.VATNumber,
                    email = model.email,
                    alternativeEmail = model.alternativeEmail,
                    mobile = model.mobile,
                    phone = model.phone,
                    //departmentsId = 17,
                    //designationsId = 1
                };

                int rid = await customerService.SaveResource(resorucedata);

               
                if (relresource == 0)
                {
                    var leadAutoNumbers = await leadsService.GetLeadAutoNumberBySp();
                    string leadNumber = leadAutoNumbers.leadNumber;
                    Leads lead = new Leads
                    {
                        Id = 0,
                        leadOwner = HttpContext.User.Identity.Name,
                        leadName = model.nameEnglish,
                        leadShortName = model.nameEnglish,
                        leadNumber = leadNumber,
                        isPersonal = 1,
                        companyName = model.nameEnglish,
                        createdBy = HttpContext.User.Identity.Name,
                        createdAt = DateTime.Now
                    };

                    var masterId = await leadsService.SaveLeads(lead);

                    RelSupplierCustomerResourse data2 = new RelSupplierCustomerResourse
                    {
                        Id = model.relSupplierCustomerResourseId,
                        resourceId = rid,
                        roleTypeId = 2,
                        LeadsId= masterId,
                        distributorTypeId=distributortypeId

                    };

                  int relid=  await customerService.SaveRelSupplierCustomerResourse(data2);
                    
                }

                if (formFile != null)
                {
                    string userName = User.Identity.Name;
                    string documentType = "Document";
                    var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                    var fileName = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName;
                    string fileType = Path.GetExtension(fileName);
                    fileName = fileName.Contains("\\")
                        ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                        : fileName.Trim('"');

                    string NewFileName = rid + "_" + documentType + "_" + fileName;
                    string fileLocation = "/Upload/Photo/" + NewFileName;
                    var fullFilePath = Path.Combine(filesPath, NewFileName);

                    using (var stream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    DocumentPhotoAttachment data = new DocumentPhotoAttachment
                    {
                        Id = 0,
                        actionType = "Customer",
                        actionTypeId = rid,
                        subject = "Document",
                        fileName = NewFileName,
                        filePath = fileLocation,
                        fileType = fileType,
                        description = "default Document description",
                        documentType = documentType,
                        createdBy = userName
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);
                }

                if (model.resourceId != 0)
                {
                    //await customerService.DeleteCustomerItemInfoByResourceId(rid);
                    await customerService.DeleteAddressByResourceId(rid);
                    await customerService.DeleteContactByResourceId(rid);
                }
                if (model.phoneall != null)
                {
                    for (int i = 0; i < model.phoneall.Count(); i++)
                    {
                        Contact contact = new Contact
                        {
                            Id = 0,
                            resourceId = rid,
                            personName = model.contactall[i],
                            Department = model.deptall[i],
                            Designation = model.desall[i],
                            phoneNumber = model.phoneall[i],
                            mobileNumber = model.mobileall[i]
                        };
                        int specid = await customerService.Savecontact(contact);
                    }
                }
                if (model.addressCategoryId != null)
                {
                    for (int i = 0; i < model.orgUpazila.Length; i++)
                    {
                        CRM.Data.Entity.Lead.Address address = new CRM.Data.Entity.Lead.Address
                        {
                            Id = Convert.ToInt32(model.orgAddressID[i]),
                            resourceId = rid,
                            countryId = 1,
                            addressCategoryId=model.addressCategoryId[i],
                            divisionId = Convert.ToInt32(model.orgDivision[i]),
                            districtId = Convert.ToInt32(model.orgDistrict[i]),
                            thanaId = Convert.ToInt32(model.orgUpazila[i]),
                            union = model.orgUnion[i],
                            //postOffice = model.orgPostOffice[i],
                            postCode = model.orgPostCode[i],
                            blockSector = model.orgBlockSector[i],
                            houseVillage = model.orgHouseVillage[i],

                            type = "Customer"
                        };
                        int vetId = await addressService.SaveAddress(address);
                    }
                    
                }
                //if (model.itemidall!=null)
                //{
                //    for (int i = 0; i < model.itemidall.Count(); i++)
                //    {
                //     CustomerItemInfo itemInfo = new CustomerItemInfo
                //        {
                //            Id = 0,
                //            resourceId = rid,
                //            itemId = model.itemidall[i]
                //        };
                //        int vwtId = await customerService.SaveCustomerItemInfo(itemInfo);
                //    }
                //}
                return RedirectToAction("Custom" +
                    "erList");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //[HttpGet]
        //public async Task<IActionResult> GetCustomerItemInfobyResourceId(int Id)
        //{
        //    var contactlist = await customerService.GetCustomerItemInfobyResourceId(Id);
        //    return Json(contactlist);
        //}
        [HttpGet]
        public async Task<IActionResult> GetcontactbyResourceid(int Id)
        {
            var contactlist = await customerService.GetcontactbyResourceid(Id);
            return Json(contactlist);
        }

        #endregion

        #region Customer List
        public async Task<IActionResult> CustomerList()
        {
            var data = await customerService.GetAllRelSupplierCustomerResourse();
            if (data == null)
            {
                data = new List<RelSupplierCustomerResourse>();
            }
           
            var dataaddress = await addressService.GetAddress();
            List<int?> relids = data.Select(x => x.resourceId).ToList();
            List<int?> leadsid = data.Select(x => x.LeadsId).ToList();
            dataaddress = dataaddress.Where(x => leadsid.Contains(x.leadsId));
            var contacts = await contactsService.GetAllContacts();
            contacts = contacts.Where(x => relids.Contains(x.resourceId));
            var resources = await resourceService.GetAllResource();
            resources = resources.Where(x => relids.Contains(x.Id));
            var model = new CustomerViewModel()
            {
                relSupplierCustomerResourses = data.Where(x=>x.LeadsId!=null),
                GetAddresses = dataaddress,
                Contacts = contacts,
                Resources = resources
            };

            return View(model);
        }

        public async Task<IActionResult> CustomerListNew()
        {
            var data = await customerService.GetAllRelSupplierCustomerResourse();
            if (data == null)
            {
                data = new List<RelSupplierCustomerResourse>();
            }

            var dataaddress = await addressService.GetAddress();
            List<int?> relids = data.Select(x => x.resourceId).ToList();
            List<int?> leadsid = data.Select(x => x.LeadsId).ToList();
            dataaddress = dataaddress.Where(x => leadsid.Contains(x.leadsId));
            var contacts = await contactsService.GetAllContacts();
            contacts = contacts.Where(x => relids.Contains(x.resourceId));
            var resources = await resourceService.GetAllResource();
            resources = resources.Where(x => relids.Contains(x.Id));
            var model = new CustomerViewModel()
            {
                relSupplierCustomerResourses = data.Where(x => x.LeadsId != null),
                GetAddresses = dataaddress,
                Contacts = contacts,
                Resources = resources
            };

            return View(model);
        }

        public async Task<IActionResult> CustomerListForLedger()
        {
            var data = await customerService.GetAllRelSupplierCustomerResourseByRoleId(2);
            if (data == null)
            {
                data = new List<RelSupplierCustomerResourse>();
            }
            var model = new CustomerViewModel()
            {
                relSupplierCustomerResourses = data
            };

            return View(model);
        }
        public async Task<ActionResult> CreateSubLedger(string name, int Id)
        {
            var ledger = await ledgerService.GetLedgerCheckbyName(name);
            if (ledger == 0)
            {
                var subLedger = await ledgerService.GetLedgerWithBigSubLedgerCustomer();
                var accTemp = "";
                string vl = "0000";
                if (subLedger == null)
                {
                    accTemp = "C0000";
                }
                else
                {
                    accTemp = subLedger.accountCode;
                }
                // var accTemp = subLedger.accountCode;
                var accWithOutS = accTemp.Replace('C', '0');
                int newAcc = Int32.Parse(accWithOutS) + 1;
                var newCode = "C" + vl.Substring(vl.Length - 4) + newAcc;
               // var newCode = "C" + newAcc;
                Ledger data = new Ledger
                {
                    Id = 0,
                    accountName = name,
                    accountCode = newCode,
                    aliasName = "",
                    companyId = 1,
                    isActive = 1,
                    currencyId = 1,
                    haveSubLedger = 2
                };

                int ledgerId = await ledgerService.Saveledger(data);
                //LedgerRelation data1 = new LedgerRelation
                //{
                //    ledgerId = 100,
                //    subLedgerId = ledgerId,
                //};
                //await ledgerService.SaveLedgerRelation(data1);
                List<int> ids = new List<int> {49,91,103,11646, 11647 };
                for (var i = 0; i < ids.Count(); i++)
                {
                    LedgerRelation data1 = new LedgerRelation
                    {
                        ledgerId =ids[i],
                        subLedgerId = ledgerId,
                    };
                    await ledgerService.SaveLedgerRelation(data1);
                }
                await customerService.UpdateCustomerLedgerId(Id, ledgerId);
            }
            return RedirectToAction("CustomerListForLedger");

        }
        #endregion



        #region API Section

        //[Route("global/api/divisions/{id}")]
        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> Divisions(int Id)
        //{
        //    return Json(await addressService.GetDivisionsByCountryId(Id));
        //}

        //[Route("global/api/districts/{id}")]
        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> Districts(int Id)
        //{
        //    return Json(await addressService.GetDistrictsByDivisonId(Id));
        //}

        //[Route("global/api/thanas/{id}")]
        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> Thanas(int Id)
        //{
        //    return Json(await addressService.GetThanasByDistrictId(Id));
        //}

        [Route("global/api/GetAllItem")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllItem()
        {
            return Json(await itemsService.GetAllItem());
        }

        [Route("global/api/customer/GetAddressListByResourceId/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAddressListByResourceId(int id)
        {
            return Json(await addressService.GetAddressListByResourceId(id));
        }

        [Route("global/api/customer/CustomerListdaterange/{fromdate}/{todate}/{localstorage}")]
        [HttpGet]
       
        public async Task<IActionResult> CustomerListdaterange(DateTime? fromdate,DateTime?todate,int? localstorage)
        {
            var modulename = await eRPModuleService.GetERPModuleById((int)localstorage);

            var distributortype = await distributorTypeService.GetAllDistributorType();
            List<int?> rellids = new List<int?>();
            var reldata = new List<RelSupplierCustomerResourse>();
            if (modulename.moduleName == "Rental")
            {
                var x = await rentInvoiceMasterService.GetAllSalesInvoiceMaster();
                rellids = x.Select(y => y.relSupplierCustomerResourseId).ToList();
                reldata = x.Select(y => y.relSupplierCustomerResourse).ToList();
            }
            else if (modulename.moduleName == "Sales")
            {
                var x = await salesInvoiceMasterService.GetAllSalesInvoiceMaster();
                rellids = x.Where(y=>y.invoiceNumber.Contains("SALE")).Select(y => y.relSupplierCustomerResourseId).ToList();
                reldata = x.Where(y=>y.invoiceNumber.Contains("SALE")).Select(y => y.relSupplierCustomerResourse).ToList();
            }
            else
            {
                var x = await salesInvoiceMasterService.GetAllSalesInvoiceMaster();
                rellids = x.Where(y => y.invoiceNumber.Contains("Service")).Select(y => y.relSupplierCustomerResourseId).ToList();
                reldata = x.Where(y => y.invoiceNumber.Contains("Service")).Select(y => y.relSupplierCustomerResourse).ToList();
            }


            int distributortypeId = 0;
            if (distributortype != null)
            {
                distributortypeId = distributortype.Where(x => x.name == modulename.moduleName).FirstOrDefault().Id;
            }
            var data = reldata;
            if (data == null)
            {
                data = new List<RelSupplierCustomerResourse>();
            }
           
            var dataaddress = await addressService.GetAddress();
           // List<int?> relids = data.Select(x => reldata.Select(y=>(int)y.resourceId).ToList().Contains(x.resourceId)).ToList();
            List<int?> leadsid = data.Select(x => x.LeadsId).ToList();
            dataaddress = dataaddress.Where(x => leadsid.Contains(x.leadsId));
            var contacts = await contactsService.GetAllContacts();
            contacts = contacts.Where(x => reldata.Select(y => (int)y.LeadsId).ToList().Contains((int)x.leadsId));
            var resources = await resourceService.GetAllResource();
            resources = resources.Where(x => contacts.Select(s=>s.resourceId).ToList().Contains(x.Id));
            var model = new CustomerViewModel()
            {
                relSupplierCustomerResourses = data,
                GetAddresses = dataaddress,
                Contacts=contacts,
                Resources=resources
            };

            return Json(model);
        }

        #endregion
    }
}