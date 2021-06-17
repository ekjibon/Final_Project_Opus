using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.Areas.SCMMasterData.Models;
using OPUSERP.Areas.SCMRequisition.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Services.Lead.Interfaces;
using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Auth;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.OtherSales.Services.Sales.Interfaces;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Services.MasterData.Interfaces;
using OPUSERP.SCM.Services.PurchaseOrder.Interface;
using OPUSERP.SCM.Services.Requisition.Interfaces;

namespace OPUSERP.Areas.SCMRequisition.Controllers
{
	[Area("SCMRequisition")]
    public class RequisitionNewController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IOSalesInvoiceMasterService salesInvoiceMasterService;
        private readonly IItemsService itemsService;
        public readonly IRequisitionService _requisitionService;
        public readonly IUserInfoes userInfoes;
        private readonly IERPCompanyService iERPCompanyService;
        private readonly IItemPriceFixingService itemPriceFixingService;
        private readonly IPurchaseOrderService purchaseOrderService;
        private readonly IOSalesInvoiceDetailService salesInvoiceDetailService;
        private readonly IOSalesCollectionService salesCollectionService;
        private readonly IOSalesCollectionDetailsService salesCollectionDetailsService;
        private readonly ILeadsService leadsService;
        private readonly IResourceService resourceService;
        private readonly ICustomerService customerService;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;
        private readonly Microsoft.AspNetCore.Identity.SignInManager<ApplicationUser> _signInManager;
        private readonly Microsoft.AspNetCore.Identity.RoleManager<ApplicationRole> _roleManager;

        private readonly string rootPath;
        private readonly MyPDF myPDF;
        public string FileName;

        public RequisitionNewController(IHostingEnvironment hostingEnvironment, IItemsService itemsService, IOSalesInvoiceMasterService salesInvoiceMasterService, IUserInfoes userInfoes, IRequisitionService requisitionService, IConverter converter, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager, IERPCompanyService iERPCompanyService, IItemPriceFixingService itemPriceFixingService, IPurchaseOrderService purchaseOrderService, IOSalesCollectionService salesCollectionService, IOSalesInvoiceDetailService salesInvoiceDetailService, IOSalesCollectionDetailsService salesCollectionDetailsService,
            Microsoft.AspNetCore.Identity.SignInManager<ApplicationUser> signInManager,
            Microsoft.AspNetCore.Identity.RoleManager<ApplicationRole> roleManager)
        {
            this._hostingEnvironment = hostingEnvironment;
            this.salesInvoiceMasterService = salesInvoiceMasterService;
            this.userInfoes = userInfoes;
            this.itemsService = itemsService;
            this.iERPCompanyService = iERPCompanyService;
            this.itemPriceFixingService = itemPriceFixingService;
            this.purchaseOrderService = purchaseOrderService;
            this.salesInvoiceDetailService = salesInvoiceDetailService;
            this.salesCollectionService = salesCollectionService;
            this.salesCollectionDetailsService = salesCollectionDetailsService;
            _requisitionService = requisitionService;

            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;

            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        public async Task<IActionResult> Index()
        {
            ItemSpecificationDepartmentModel model = new ItemSpecificationDepartmentModel
            {
                itemSpecificationDepartmentModels = await itemsService.GetItemByDepartmentWise(),
                MostRecent = await _requisitionService.GetMostRecentReq(),
                TopTen = await _requisitionService.GetTopTenReq(),
                Featured = await _requisitionService.GetFeaturedReq()
            };
            return View(model);
        }

    
        public async Task<IActionResult> CreateRequisition()
        {
            string userName = HttpContext.User.Identity.Name;
            var User = await userInfoes.GetUserBasicInfoes(userName);
            var role = await _userManager.GetRolesAsync(User);
            IEnumerable<SectionWiseDesignationModel> sectionWiseDesignations = new List<SectionWiseDesignationModel>();
            LogUserPersonInformation logUsers = new LogUserPersonInformation();

            var sale3 = await salesInvoiceMasterService.GetAllSalesInvoiceMaster();
            int Cpurchase3 = 0;
            Cpurchase3 = sale3.Where(x => Convert.ToDateTime(x.invoiceDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            string idate3 = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo3 = "Pos" + '-' + idate3 + '-' + (Cpurchase3 + 1);
            ViewBag.SaleNo = issueNo3;

            if (User.org == "Person" && role.Contains("Log User"))
            {
                //logUsers = await _employee.GetPersonUserInfoByUser(userName);
                //sectionWiseDesignations = new List<SectionWiseDesignationModel>();
            }
            else if (User.org == "Person" && role.Contains("Department Approver"))
            {
                //logUsers = await _employee.GetPersonUserInfoByDeptApprover(userName);
                //sectionWiseDesignations = new List<SectionWiseDesignationModel>();
            }
            else if (User.org == "Section" && role.Contains("Log User"))
            {
                //sectionWiseDesignations = await personalInfoService.GetSectionWiseDesignation(userName);
            }
            else
            {
                //logUsers = new LogUserPersonInformation();
                //sectionWiseDesignations = new List<SectionWiseDesignationModel>();
            }
            var empInfo = await userInfoes.GetEmployeeInfobyUser(userName);

            ItemSpecificationDepartmentModel model = new ItemSpecificationDepartmentModel
            {
                itemSpecificationDepartmentModels = await itemsService.GetItemByDepartmentWise(),
                //MostRecent = await _requisitionService.GetMostRecentReq(1),
                //TopTen = await _requisitionService.GetTopTenReq(1),
                Featured = await _requisitionService.GetFeaturedReq(),
                //employeeBasic = empInfo,
                //logUsers = logUsers,
                sectionWiseDesignations = sectionWiseDesignations
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SavePosSales([FromForm] SalesInvoicePressViewModel model)
        {
            try
            {
                model.masterId = 0;
                var company = await iERPCompanyService.GetAllCompany();
                //if (!ModelState.IsValid || model.itemPriceFixingId == null || model.itemPriceFixingId.Length <= 0)
                if (!ModelState.IsValid)
                {
                    var sale3 = await salesInvoiceMasterService.GetAllSalesInvoiceMaster();
                    int Cpurchase3 = 0;
                    Cpurchase3 = sale3.Where(x => Convert.ToDateTime(x.invoiceDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
                    string idate3 = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
                    string issueNo3 = "Pos" + '-' + idate3 + '-' + (Cpurchase3 + 1);
                    ViewBag.SaleNo = issueNo3;
                    model.salesInvoiceMaster = new SalesInvoiceMaster();
                    model.itemPriceFixings = await itemPriceFixingService.GetActiveItemPriceFixing();
                    if (model.itemPriceFixingId == null || model.itemPriceFixingId.Length <= 0)
                    {
                        ModelState.AddModelError(string.Empty, "Please Select Some Item to Sold");
                    }

                    return View(model);
                }

                string user = HttpContext.User.Identity.Name;
                var userinfo = await userInfoes.GetUserInfoByUser(user);

            
                SalesInvoiceMaster data = new SalesInvoiceMaster
                {
                    Id = Convert.ToInt32(model.masterId),
                    relSupplierCustomerResourseId = model.customerId,
                    invoiceDate = model.invoiceDate,
                    paymentDate = model.invoiceDate,
                    invoiceNumber = "123",//model.salesInvoiceNo,
                    totalAmount = model.grossTotal,
                    VATOnTotal = model.grossVAT,
                    DiscountOnTotal = model.grossDiscount,
                    NetTotal = model.netTotal,
                    givenAmount = model.given,
                    change = model.change,
                    salesType = 1,
                    isClose = 1,
                    isStockClose = 1,
                    alternateMobile = model.baleboy
                };
                var masterId = await salesInvoiceMasterService.SaveSalesInvoiceMaster(data);
                //if (model.reqMasterId > 0)
                //{
                //    await requisitionService.DeleteRequisitionDetailByreqId(masterId);
                //}
                SalesInvoiceDetail detailEntity = new SalesInvoiceDetail();
                foreach (var data1 in model.Details)
                {
                    detailEntity.Id = 0;
                    //detailEntity.itemSpecicationId = Convert.ToInt32(masterId);

                    detailEntity.itemSpecicationId = Convert.ToInt32(data1.itemSpecificationId);
                    detailEntity.salesInvoiceMasterId = masterId;
                    detailEntity.quantity = data1.reqQty;
                    detailEntity.rate = data1.reqRate;

                    //var detailId = await requisitionService.SaveRequisitionDetail(detailEntity);
                    var detailId = await salesInvoiceDetailService.SaveSalesInvoiceDetail(detailEntity);

                    detailEntity = new SalesInvoiceDetail();
                }
                //for (int i = 0; i < model.itemPriceFixingId.Length; i++)
                //{
                //    SalesInvoiceDetail data1 = new SalesInvoiceDetail
                //    {
                //        Id = 0,
                //        itemSpecicationId = model.itemPriceFixingId[i],
                //        quantity = model.tblQuantity[i],
                //        rate = model.priceList[i],
                //        salesInvoiceMasterId = masterId
                //    };
                //    await salesInvoiceDetailService.SaveSalesInvoiceDetail(data1);
                //}

                var purchase = await salesCollectionService.GetAllSalesCollection();
                int Cpurchase1 = 0;
                Cpurchase1 = purchase.Where(x => Convert.ToDateTime(x.collectionDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
                string idate1 = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
                string issueNo1 = "Pos-Collection-No-" + '-' + idate1 + '-' + (Cpurchase1 + 1);

                int saleCollectionId = 0;

                SalesCollection posCollectionMaster = new SalesCollection
                {
                    relSupplierCustomerResourseId = model.customerId,
                    collectionNumber = issueNo1,
                    collectionAmount = model.netTotal,
                    collectionDate = model.invoiceDate,
                    companyId = company.OrderByDescending(a => a.Id).FirstOrDefault().Id,
                    remarks = "Pos Payment",
                    paymentModeId = model.paymentModeId,
                   // cashAmount = cashAmount,

                };
                saleCollectionId = await salesCollectionService.SaveSalesCollection(posCollectionMaster);
                SalesCollectionDetail posCollectionDetail = new SalesCollectionDetail
                {
                    salesCollectionId = saleCollectionId,
                    Amount = model.netTotal,
                    collectionDate = model.invoiceDate,
                    remarks = "Pos Payment"
                };
                await salesCollectionDetailsService.SaveSalesCollectionDetail(posCollectionDetail);


                #region pdf
                string scheme = Request.Scheme;
                var host = Request.Host;

                string url = scheme + "://" + host + "/OtherSales/SalesInvoice/POSInvoicePDF?id=" + masterId;

                string fileName;
                string status = myPDF.GeneratePOSReceiptPDF(out fileName, url);

                //string status = myPDF.GeneratePDF(out fileName, url);

                FileName = fileName;
                if (status != "done")
                {
                    return Content("<h1>" + status + "</h1>");
                }

                // var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
                var stream = rootPath + "/wwwroot/pdf/" + fileName;
                #endregion


                return Json(fileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //public async Task<IActionResult> SavePosSales([FromForm] SalesInvoicePressViewModel model)
        //{
        //    model.masterId = 0;
        //    var company = await iERPCompanyService.GetAllCompany();
        //    if (!ModelState.IsValid || model.itemPriceFixingId == null || model.itemPriceFixingId.Length <= 0)
        //    {
        //        var sale3 = await salesInvoiceMasterService.GetAllSalesInvoiceMaster();
        //        int Cpurchase3 = 0;
        //        Cpurchase3 = sale3.Where(x => Convert.ToDateTime(x.invoiceDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
        //        string idate3 = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
        //        string issueNo3 = "Pos" + '-' + idate3 + '-' + (Cpurchase3 + 1);
        //        ViewBag.SaleNo = issueNo3;
        //        model.salesInvoiceMaster = new SalesInvoiceMaster();
        //        model.itemPriceFixings = await itemPriceFixingService.GetActiveItemPriceFixing();
        //        if (model.itemPriceFixingId == null || model.itemPriceFixingId.Length <= 0)
        //        {
        //            ModelState.AddModelError(string.Empty, "Please Select Some Item to Sold");
        //        }

        //        return View(model);
        //    }

        //    string user = HttpContext.User.Identity.Name;
        //    var userinfo = await userInfoes.GetUserInfoByUser(user);

        //    if (model.netTotal != model.cashAmount)
        //    {
        //        var sale3 = await salesInvoiceMasterService.GetAllSalesInvoiceMaster();
        //        int Cpurchase3 = 0;
        //        Cpurchase3 = sale3.Where(x => Convert.ToDateTime(x.invoiceDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
        //        string idate3 = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
        //        string issueNo3 = "Pos" + '-' + idate3 + '-' + (Cpurchase3 + 1);
        //        ViewBag.SaleNo = issueNo3;
        //        model.salesInvoiceMaster = new SalesInvoiceMaster();
        //        model.itemPriceFixings = await itemPriceFixingService.GetActiveItemPriceFixing();

        //        model.paymentModes = await purchaseOrderService.GetPaymentMode();
        //        ModelState.AddModelError(string.Empty, "Total & Cash amount not same.");
        //        return View(model);
        //    }
        //    decimal cashAmount = 0;
        //    cashAmount = (decimal)model.cashAmount;

        //    var sale = await salesInvoiceMasterService.GetAllSalesInvoiceMaster();
        //    int Cpurchase = 0;
        //    Cpurchase = sale.Where(x => Convert.ToDateTime(x.invoiceDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
        //    string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
        //    string issueNo = "Pos" + '-' + idate + '-' + (Cpurchase + 1);

        //    if (model.masterId > 0)
        //    {
        //        issueNo = model.salesInvoiceNo;
        //    }
        //    if (model.masterId > 0)
        //    {
        //        try
        //        {
        //            await salesInvoiceDetailService.DeleteSalesInvoiceDetailByMasterId(Convert.ToInt32(model.masterId));
        //        }
        //        catch
        //        {
        //            //return RedirectToAction(nameof(InvoiceDetails), new { id = model.masterId });
        //            return RedirectToAction("OtherSales/SalesInvoice/InvoiceDetails", new { id = model.masterId });
        //        }
        //    }
        //    int? isMember = 0;
        //    if (model.isMember == 1)
        //    {
        //        isMember = 1;
        //    }

        //    if (model.customerId == null || model.customerId == 0)
        //    {
        //        Leads lead = new Leads
        //        {
        //            leadOwner = HttpContext.User.Identity.Name,
        //            leadName = model.customerName,
        //            leadShortName = model.customerName,
        //            isPersonal = 0,
        //            isClient = isMember, //For membership
        //            leadNumber = model.cardNo  //For membership Number
        //        };

        //        var leadid = await leadsService.SaveLeads(lead);

        //        Resource resource = new Resource
        //        {
        //            resourceName = model.customerName,
        //            mobile = model.customerMobile,
        //            organizationName = model.address

        //        };
        //        int resourceId = await resourceService.SaveResourceReturnId(resource);

        //        var roleData = await customerService.GetRoleTypeIdByName("Leads");
        //        int roleId = roleData.Id;

        //        if (Convert.ToInt32(model.customerId) == 0)
        //        {
        //            RelSupplierCustomerResourse data2 = new RelSupplierCustomerResourse
        //            {
        //                Id = 0,
        //                LeadsId = leadid,
        //                resourceId = resourceId,
        //                roleTypeId = roleId
        //            };
        //            model.customerId = await customerService.SaveRelSupplierCustomerResourse(data2);
        //        }

        //    }

        //    if (model.grossDiscount == null)
        //    {
        //        model.grossDiscount = 0;
        //    }
        //    if (model.grossVAT == null)
        //    {
        //        model.grossVAT = 0;
        //    }

        //    SalesInvoiceMaster data = new SalesInvoiceMaster
        //    {
        //        Id = Convert.ToInt32(model.masterId),
        //        relSupplierCustomerResourseId = model.customerId,
        //        invoiceDate = model.invoiceDate,
        //        paymentDate = model.invoiceDate,
        //        invoiceNumber = issueNo,//model.salesInvoiceNo,
        //        totalAmount = model.grossTotal,
        //        VATOnTotal = model.grossVAT,
        //        DiscountOnTotal = model.grossDiscount,
        //        NetTotal = model.netTotal,
        //        givenAmount = model.given,
        //        change = model.change,
        //        salesType = 1,
        //        isClose = 1,
        //        isStockClose = 1,
        //        alternateMobile = model.baleboy
        //    };
        //    var masterId = await salesInvoiceMasterService.SaveSalesInvoiceMaster(data);

        //    for (int i = 0; i < model.itemPriceFixingId.Length; i++)
        //    {
        //        SalesInvoiceDetail data1 = new SalesInvoiceDetail
        //        {
        //            Id = 0,
        //            itemSpecicationId = model.itemPriceFixingId[i],
        //            quantity = model.tblQuantity[i],
        //            rate = model.priceList[i],
        //            salesInvoiceMasterId = masterId
        //        };
        //        await salesInvoiceDetailService.SaveSalesInvoiceDetail(data1);
        //    }

        //    var purchase = await salesCollectionService.GetAllSalesCollection();
        //    int Cpurchase1 = 0;
        //    Cpurchase1 = purchase.Where(x => Convert.ToDateTime(x.collectionDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
        //    string idate1 = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
        //    string issueNo1 = "Pos-Collection-No-" + '-' + idate1 + '-' + (Cpurchase1 + 1);

        //    int saleCollectionId = 0;

        //    SalesCollection posCollectionMaster = new SalesCollection
        //    {
        //        relSupplierCustomerResourseId = model.customerId,
        //        collectionNumber = issueNo1,
        //        collectionAmount = model.netTotal,
        //        collectionDate = model.invoiceDate,
        //        companyId = company.OrderByDescending(a => a.Id).FirstOrDefault().Id,
        //        remarks = "Pos Payment",
        //        paymentModeId = model.paymentModeId,
        //        cashAmount = cashAmount,

        //    };
        //    saleCollectionId = await salesCollectionService.SaveSalesCollection(posCollectionMaster);
        //    SalesCollectionDetail posCollectionDetail = new SalesCollectionDetail
        //    {
        //        salesCollectionId = saleCollectionId,
        //        Amount = model.netTotal,
        //        collectionDate = model.invoiceDate,
        //        remarks = "Pos Payment"
        //    };
        //    await salesCollectionDetailsService.SaveSalesCollectionDetail(posCollectionDetail);


        //    #region pdf
        //    string scheme = Request.Scheme;
        //    var host = Request.Host;

        //    string url = scheme + "://" + host + "/OtherSales/SalesInvoice/POSInvoicePDF?id=" + masterId;

        //    string fileName;
        //    string status = myPDF.GeneratePOSReceiptPDF(out fileName, url);

        //    //string status = myPDF.GeneratePDF(out fileName, url);

        //    FileName = fileName;
        //    if (status != "done")
        //    {
        //        return Content("<h1>" + status + "</h1>");
        //    }

        //    // var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
        //    var stream = rootPath + "/wwwroot/pdf/" + fileName;
        //    #endregion


        //    return Json(fileName);


        //}


        [HttpPost]
        public async Task<IActionResult> SaveFeaturedItems(int id)
        {
            string userName = HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);

            var isExist = await itemsService.GetFeatureItemByUserAndSpecId(user.userId, id) != null ? true : false;
            if (!isExist)
            {
                var spec = await itemsService.itemSpecificationById(id);
                var featured = new FeatureItem
                {
                    Id = 0,
                    itemSpecificationId = id,
                    statusId = 1,
                    
                    date = DateTime.Now,
                    userId = user.userId.ToString()
                };
                await itemsService.SaveFeatureItem(featured);
                return Json("Success");
            }
            else
            {
                return Json("Exists");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetRecentItems(int id)
        {
            string userName = HttpContext.User.Identity.Name;
            //var data = await _requisitionService.GetMostRecentReq(id);
            var data = await _requisitionService.GetMostRecentRequisition(id, userName);
            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetTopTen(int id)
        {
            var data = await _requisitionService.GetTopTenReq(1);
            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetFeatured(int id)
        {
            string username = HttpContext.User.Identity.Name;
            var data = await _requisitionService.GetFeaturedReq(username, id);
            return Json(data);
        }

        public async Task<IActionResult> DeleteFeaturedItems(int id)
        {
            try
            {
                string userName = HttpContext.User.Identity.Name;
                var user = await _userManager.FindByNameAsync(userName);

                var featured = await itemsService.GetFeatureItemByUserAndSpecId(user.userId, id);
                await itemsService.DeleteFeatureItemByID(featured.Id);

                return RedirectToAction("CreateRequisition");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
