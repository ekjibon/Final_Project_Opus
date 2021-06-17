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
using Newtonsoft.Json.Linq;
using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using OPUSERP.Accounting.Services.AccountingSettings.Interfaces;
using OPUSERP.Areas.Distribution.Models;
//using OPUSERP.Areas.OtherSales.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.Data.Entity.Master;
using OPUSERP.Distribution.Data.Entity.MasterData;
using OPUSERP.Distribution.Services.MasterData.Interfaces;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.OtherSales.Services.Sales.Interfaces;
using OPUSERP.SCM.Data.Entity.Requisition;
using OPUSERP.SCM.Data.Entity.Supplier;
using OPUSERP.SCM.Services.MasterData.Interfaces;

namespace OPUSERP.Areas.Distribution.Controllers
{
    [Area("Distribution")]
    public class DistributorController : Controller
    {
        //Customer
        private readonly ICustomerService customerService;
        private readonly IAddressService addressService;
        private readonly IItemsService itemsService;
        private readonly IAttachmentCommentService attachmentCommentService;
        private readonly IAddressCategoryService addressCategoryService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILedgerService ledgerService;
        private readonly IDistributorTypeService distributorTypeService;
        private readonly ICommunicationService communicationService;
        private readonly IAddressTypeService addressTypeService;
        private readonly ISalesTeamDeploymentService salesTeamDeploymentService;
        private readonly ISalesLevelService salesLevelService;
        private int Depth;
        private int DepthA;
        public DistributorController(IHostingEnvironment hostingEnvironment, ISalesLevelService salesLevelService, ISalesTeamDeploymentService salesTeamDeploymentService, IAddressTypeService addressTypeService, ICommunicationService communicationService, IDistributorTypeService distributorTypeService, ILedgerService ledgerService, ICustomerService customerService, IAddressService addressService, IItemsService itemsService, IAttachmentCommentService attachmentCommentService, IAddressCategoryService addressCategoryService)
        {
            this.customerService = customerService;
            this.addressService = addressService;
            this.itemsService = itemsService;
            this.attachmentCommentService = attachmentCommentService;
            this.addressCategoryService = addressCategoryService;
            this.ledgerService = ledgerService;
            this.distributorTypeService = distributorTypeService;
            this.communicationService = communicationService;
            this.addressTypeService = addressTypeService;
            this.salesTeamDeploymentService = salesTeamDeploymentService;
            this.salesLevelService = salesLevelService;
            this._hostingEnvironment = hostingEnvironment;
            Depth = 0;
            DepthA = 0;
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
          //  ViewBag.distributionTypeId = 0;
            int relSupplierCustomerResourseId = 0;
            if (viid != 0)
            {
                var relSupplierCustomerResourse = await customerService.GetRelSupplierCustomerResourseByResourceId(viid);
                ViewBag.distributionTypeId = relSupplierCustomerResourse.distributorTypeId;
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
                    //addressCategories = await addressCategoryService.GetAddressCategory(),
                    addressTypes = await addressTypeService.GetAllAddressType(),
                    distributorTypes = await distributorTypeService.GetAllDistributorType(),
                    areas = await communicationService.GetAllArea(),
                    relSupplierCustomerResourse = await customerService.GetRelSupplierCustomerResourseByResourceId(viid),
                    salesTeamDeployments = await salesTeamDeploymentService.GetAllSalesTeamDeployment(),
                    salesLevels = await salesLevelService.GetAllSalesLevel()


                };
                model.actionType = actionType;
                if (model.GetResourcebyId == null) model.GetResourcebyId = new Resource();
              //  if (model.addressTypes == null) model.addressTypes = new List<AddressType>();


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
            //return Json(model);

            try
            {
                string userId = HttpContext.User.Identity.Name;

                Resource resorucedata = new Resource
                {
                    Id = Convert.ToInt32(model.resourceId),
                    nameEnglish = model.nameEnglish,
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
                    RelSupplierCustomerResourse data2 = new RelSupplierCustomerResourse
                    {
                        Id = model.relSupplierCustomerResourseId,
                        resourceId = rid,
                        roleTypeId = 3,
                        distributorTypeId = model.distributorTypeId
                    };

                 int relid=   await customerService.SaveRelSupplierCustomerResourse(data2);
                   
                  
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
                            //Department = model.deptall[i],
                            //Designation = model.desall[i],
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
                            Id = 0,
                            resourceId = rid,
                            countryId = 1,
                            addressTypeId=(int)model.addressCategoryId[i],
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
                var relSupplierCustomerResourse = await customerService.GetRelSupplierCustomerResourseByResourceId(rid);
                if (model.areaidall != null)
                {
                    if (relSupplierCustomerResourse.Id != 0)
                    {
                        //await customerService.DeleteCustomerItemInfoByResourceId(rid);
                        await customerService.DeleteRelCustomerAreaByrelId(relSupplierCustomerResourse.Id);
                      
                    }
                    for (int i = 0; i < model.areaidall.Count(); i++)
                    {

                        RelCustomerArea area = new RelCustomerArea
                        {
                            Id = 0,

                            relSupplierCustomerResourseId = relSupplierCustomerResourse.Id,
                            areaId = model.areaidall[i],

                        };
                        int ssspecid = await customerService.SaveRelCustomerArea(area);
                    }
                }
                if (model.employeeInfoAll != null)
                {
                    if (relSupplierCustomerResourse.Id != 0)
                    {
                        //await customerService.DeleteCustomerItemInfoByResourceId(rid);
                        await customerService.DeleteRelCustomerSalesTeamDeploymentByrelId(relSupplierCustomerResourse.Id);

                    }
                    for (int i = 0; i < model.employeeInfoAll.Count(); i++)
                    {

                        RelCustomerSalesTeamDeployment team = new RelCustomerSalesTeamDeployment
                        {
                            Id = 0,

                            relSupplierCustomerResourseId = relSupplierCustomerResourse.Id,
                            //nsmsalesTeamDeploymentId = model.teamnsmidall[i],
                            //rsmsalesTeamDeploymentId = model.teamrsmidall[i],
                            //tsmsalesTeamDeploymentId = model.teamtsmidall[i],
                            employeeInfoId = model.employeeInfoAll[i],
                            areaId = model.areaIdAll[i],
                            salesLevelId = model.salesLevelIdAll[i],

                        };
                        int ssspecid = await customerService.SaverelCustomerSalesTeamDeployment(team);
                    }
                }
                //if (model.itemidall!=null)
                //{
                //    for (int i = 0; i < model.itemidall.Count(); i++)
                //    {
                //        CustomerItemInfo itemInfo = new CustomerItemInfo
                //        {
                //            Id = 0,
                //            resourceId = rid,
                //            itemId = model.itemidall[i]
                //        };
                //        int vwtId = await customerService.SaveCustomerItemInfo(itemInfo);
                //    }
                //}
                return RedirectToAction("CustomerList");
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
            var data = await customerService.GetAllRelSupplierCustomerResourseByRoleId(3);
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

        //[Route("global/api/GetAllItem")]
        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> GetAllItem()
        //{
        //    return Json(await itemsService.GetAllItem());
        //}

        [Route("global/api/Distributor/GetAddressListByResourceId/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAddressListByResourceId(int id)
        {
            return Json(await addressService.GetAddressListByResourceId(id));
        }

        [Route("global/api/Distributor/GetContacByResourceId/{Id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetContacByResourceId(int Id)
        {
            return Json(await customerService.GetcontactbyResourceid(Id));
        }
        [Route("global/api/Distributor/GetCustomerareaInfobyResourceId/{Id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCustomerareaInfobyResourceId(int Id)
        {
            return Json(await customerService.GetAllRelCustomerArearesourceId(Id));
        }
        [Route("global/api/Distributor/GetCustomerteamInfobyResourceId/{Id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCustomerteamInfobyResourceId(int Id)
        {
            return Json(await customerService.GetAllrelCustomerSalesTeamDeploymentresourceId(Id));
        }

        [Route("global/api/GetparentsalesdeployebyId/{Id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetparentsalesdeployebyId(int Id)
        {
            return Json(await salesTeamDeploymentService.GetSalesTeamDeploymentByparrentId(Id));
        }


        #endregion
        #region tree show
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetMenusJsonTeam()
        {
            Depth = 2;
            string s = "[{" + string.Format("\"data\":{0},\"name\":\"{1}\",\"nameEN\":\"{2}\",\"parent\":\"{3}\",\"type\":\"{4}\",\"children\":[{5}]", 0, "Start", "Start", "null", "parrent", await this.GenerateTree(0, "Start", 0)) + "}]";

            dynamic data = new JObject();
            data.menus = s;
            data.depth = Depth;
            return Json(data);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetMenusJsonTeamArea()
        {
            DepthA = 2;
            string s = "[{" + string.Format("\"data\":{0},\"name\":\"{1}\",\"nameEN\":\"{2}\",\"parent\":\"{3}\",\"type\":\"{4}\",\"children\":[{5}]", 0, "Start", "Start", "null", "parrent", await this.GenerateTreeArea(0, "Start", 0)) + "}]";

            dynamic data = new JObject();
            data.menus = s;
            data.depth = DepthA;
            return Json(data);
        }

        //Recursion For Retriving Tree 
        private async Task<string> GenerateTree(int parrentid, string parrentName, int level)
        {
            Depth = Math.Max(level, Depth);
            string data = "";
            IEnumerable<RelSupplierCustomerResourse> MenuData = await customerService.GetRelSupplierCustomerResourseByResourceshow();

            if (MenuData.Count() <= 0)
            {
                return data;
            }

            int last = MenuData.Last().Id;
            foreach (RelSupplierCustomerResourse menu in MenuData)
            {
                string type = "parrent";

                // if (menu.IsLast) { type = "last"; }

                string child = await GenerateTreeTeam(0, menu.resource.nameEnglish, level + 1,menu.Id);

                string S = "{" + string.Format("\"data\":{0},\"name\":\"{1}\",\"nameEN\":\"{2}\",\"parent\":\"{3}\",\"type\":\"{4}\",\"children\":[{5}]", menu.Id, menu.resource.nameEnglish, menu.resource.nameEnglish, parrentid, menu.Id, child) + "}";

                if (menu.Id != last)
                {
                    S += ",";
                }
                data += S;
            }

            return data;
        }
        private async Task<string> GenerateTreeArea(int parrentid, string parrentName, int level)
        {
            DepthA = Math.Max(level, DepthA);
            string data = "";
            IEnumerable<RelSupplierCustomerResourse> MenuData = await customerService.GetRelSupplierCustomerResourseByResourceshow();

            if (MenuData.Count() <= 0)
            {
                return data;
            }

            int last = MenuData.Last().Id;
            foreach (RelSupplierCustomerResourse menu in MenuData)
            {
               

                // if (menu.IsLast) { type = "last"; }

                string child = await GenerateTreeTeamArea(0, menu.resource.nameEnglish, level + 1, menu.Id);

                string S = "{" + string.Format("\"data\":{0},\"name\":\"{1}\",\"nameEN\":\"{2}\",\"parent\":\"{3}\",\"type\":\"{4}\",\"children\":[{5}]", menu.Id, menu.resource.nameEnglish, menu.resource.nameEnglish, parrentid, menu.Id, child) + "}";

                if (menu.Id != last)
                {
                    S += ",";
                }
                data += S;
            }

            return data;
        }

        private async Task<string> GenerateTreeTeam(int parrentid, string parrentName, int level,int disid)
        {
            Depth = Math.Max(level, Depth);
            string data = "";
           // IEnumerable<RelCustomerSalesTeamDeployment> lstteam = await customerService.GetAllrelCustomerSalesTeamDeploymentresourceId(disid);
            //List<int?>listsalelevel=lstteam.Select(x=>x.employeeInfoId).ToList();
            IEnumerable<SalesTeamDeployment> MenuData = await salesTeamDeploymentService.GetSalesTeamDeploymentByparrentrelId(parrentid,disid);

            if (MenuData.Count() <= 0)
            {
                return data;
            }

            int last = MenuData.Last().Id;
            foreach (SalesTeamDeployment menu in MenuData)
            {
               

                // if (menu.IsLast) { type = "last"; }

                string child = await GenerateTreeTeam(menu.Id, menu.employeeInfo.nameEnglish, level + 1,disid);
                //int empid = lstteam.Where(x => x.employeeInfoId == menu.employeeInfoId).Count();
                //if (empid > 0)
                //{
                //    string S = "{" + string.Format("\"data\":{0},\"name\":\"{1}\",\"nameEN\":\"{2}\",\"parent\":\"{3}\",\"type\":\"{4}\",\"children\":[{5}]", menu.Id, menu.employeeInfo.nameEnglish, menu.employeeInfo.nameEnglish, parrentid, menu.salesLevelId, child) + "}";

                //    if (menu.Id != last)
                //    {
                //        S += ",";
                //    }

                //    data += S;
                //}
                //else
                //{

                //        return data;

                //}
                string S = "{" + string.Format("\"data\":{0},\"name\":\"{1}\",\"nameEN\":\"{2}\",\"parent\":\"{3}\",\"type\":\"{4}\",\"children\":[{5}]", menu.Id, menu.employeeInfo.nameEnglish, menu.employeeInfo.nameEnglish, parrentid, menu.salesLevelId, child) + "}";

                if (menu.Id != last)
                {
                    S += ",";
                }

                data += S;

            }

            return data;
        }

        private async Task<string> GenerateTreeTeamArea(int parrentid, string parrentName, int level, int disid)
        {
            DepthA = Math.Max(level, DepthA);
            string data = "";
            IEnumerable<Area> MenuData = await communicationService.GetMenusByParrentdistId(parrentid,disid);

            if (MenuData.Count() <= 0)
            {
                return data;
            }

            int last = MenuData.Last().Id;
            foreach (Area menu in MenuData)
            {
               

                // if (menu.IsLast) { type = "last"; }

                string child = await GenerateTreeTeam(menu.Id, menu.areaName, level + 1, disid);

                string S = "{" + string.Format("\"data\":{0},\"name\":\"{1}\",\"nameEN\":\"{2}\",\"parent\":\"{3}\",\"type\":\"{4}\",\"children\":[{5}]", menu.Id, menu.areaName, menu.areaName, parrentid, menu.salesLevelId, child) + "}";

                if (menu.Id != last)
                {
                    S += ",";
                }
                data += S;
            }

            return data;
        }
        public ActionResult TeamShow()
        {
            return View();
        }
        #endregion
    }
}