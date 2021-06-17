using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Accounting.Services.AccountingSettings.Interfaces;
using OPUSERP.Areas.MasterData.Models;
using OPUSERP.Areas.OtherSales.Models;
using OPUSERP.CRM.Data.Entity.Client;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Services.Client.Interfaces;
using OPUSERP.CRM.Services.Lead.Interfaces;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.OtherSales.Services.Sales.Interfaces;
using OPUSERP.Patient.Services.Interfaces;
using OPUSERP.Payroll.Services.Salary.Interfaces;
using OPUSERP.Rental.Services.Sales.Interfaces;
using OPUSERP.SCM.Services.PurchaseOrder.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OPUSERP.Areas.OtherSales.Controllers
{
    [Authorize]
    [Area("OtherSales")]
    public class SalesInvoiceController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly OPUSERP.OtherSales.Services.Sales.Interfaces.IItemPriceFixingService itemPriceFixingService;
        private readonly IOSalesInvoiceMasterService salesInvoiceMasterService;
        private readonly IOSalesInvoiceDetailService salesInvoiceDetailService;
        private readonly IAttachmentCommentService attachmentCommentService;
        private readonly ILedgerService ledgerService;
        private readonly IRentInvoiceMasterService rentInvoiceMasterService;
        private readonly IRentInvoiceDetailService rentInvoiceDetailService;
        //private readonly IStoreService storeService;
        private readonly IUserInfoes userInfoes;
        private readonly IHomeCareService homeCareService;
        private readonly ICustomerService customerService;
        private readonly IERPCompanyService iERPCompanyService;
        private readonly IAddressesService addressesService;
        private readonly ILeadsService leadsService;
        private readonly IClientService clientService;
        private readonly IAddressService AddressService;
        private readonly IPurchaseOrderService purchaseOrderService;
        private readonly IContactsService contactsService;
        private readonly IResourceService resourceService;
        private readonly IBankBranchService bankBranchService;
        private readonly ISalaryService salaryService;
        private readonly IOSalesCollectionService salesCollectionService;
        private readonly IOSalesCollectionDetailsService salesCollectionDetailsService;
        private readonly IPaymentModeService paymentModeService;

        private readonly string rootPath;
        private readonly MyPDF myPDF;
        public string FileName;


        public SalesInvoiceController(IHostingEnvironment hostingEnvironment,
            IAddressService AddressService, IContactsService contactsService,
            IResourceService resourceService, ILeadsService leadsService,
            IClientService clientService, IAddressesService addressesService,
            ICustomerService customerService, IHomeCareService homeCareService,
            IRentInvoiceMasterService rentInvoiceMasterService,
            IRentInvoiceDetailService rentInvoiceDetailService, IUserInfoes userInfoes,
            ILedgerService ledgerService, IAttachmentCommentService attachmentCommentService,
            OPUSERP.OtherSales.Services.Sales.Interfaces.IItemPriceFixingService itemPriceFixingService,
            IOSalesInvoiceMasterService salesInvoiceMasterService,
            IOSalesInvoiceDetailService salesInvoiceDetailService, IConverter converter,
            IERPCompanyService iERPCompanyService, IPurchaseOrderService purchaseOrderService,
            IBankBranchService bankBranchService, ISalaryService salaryService,
            IOSalesCollectionService salesCollectionService,
            IOSalesCollectionDetailsService salesCollectionDetailsService,
            IPaymentModeService paymentModeService)
        {
            this._hostingEnvironment = hostingEnvironment;
            this.attachmentCommentService = attachmentCommentService;
            this.itemPriceFixingService = itemPriceFixingService;
            this.salesInvoiceMasterService = salesInvoiceMasterService;
            this.salesInvoiceDetailService = salesInvoiceDetailService;
            this.ledgerService = ledgerService;
            this.userInfoes = userInfoes;
            this.clientService = clientService;
            this.leadsService = leadsService;
            this.rentInvoiceMasterService = rentInvoiceMasterService;
            this.rentInvoiceDetailService = rentInvoiceDetailService;
            this.homeCareService = homeCareService;
            this.customerService = customerService;
            this.iERPCompanyService = iERPCompanyService;
            this.addressesService = addressesService;
            this.AddressService = AddressService;
            this.resourceService = resourceService;
            this.contactsService = contactsService;
            this.purchaseOrderService = purchaseOrderService;
            this.bankBranchService = bankBranchService;
            this.salaryService = salaryService;
            this.salesCollectionService = salesCollectionService;
            this.salesCollectionDetailsService = salesCollectionDetailsService;
            this.paymentModeService = paymentModeService;

            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        #region SalesInvoice
        public async Task<IActionResult> Index(int id)
        {
            //var sale = await salesInvoiceMasterService.GetAllSalesInvoiceMaster();
            //int Cpurchase = 0;
            //Cpurchase = sale.Where(x => Convert.ToDateTime(x.invoiceDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            //string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            //string issueNo = "Sale" + idate.ToString() + (Cpurchase + 1).ToString();

            //ViewBag.SaleNo = issueNo;
            int masterId = Convert.ToInt32(id);
            var invoiceNumber = "";

            if (masterId != 0)
            {
                var masterdata = await salesInvoiceMasterService.GetSalesInvoiceMasterById(id);
                invoiceNumber = masterdata.invoiceNumber;
            }
            else
            {
                var invoiceAutoNumbers = await rentInvoiceMasterService.GetAutoSalesInvoiceNoBySp(Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd"));
                invoiceNumber = invoiceAutoNumbers.leadNumber;
            }


            ViewBag.SaleNo = invoiceNumber;


            var MasterInfo = await salesInvoiceMasterService.GetSalesInvoiceMasterById(id);
            if (MasterInfo == null)
            {
                MasterInfo = new SalesInvoiceMaster();
            }
            string user = HttpContext.User.Identity.Name;
            var userinfo = await userInfoes.GetUserInfoByUser(user);
            SalesInvoiceViewModel model = new SalesInvoiceViewModel
            {
                masterId = id,
                itemPriceFixings = await itemPriceFixingService.GetActiveItemPriceFixing(),
                salesInvoiceMaster = MasterInfo,
                termsAndConditions = await rentInvoiceMasterService.GetAllTermsAndConditions()
                //storeAssigns = await storeService.GetAllStoreAssignByUserId(userinfo.ID)
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] SalesInvoiceViewModel model)
        {
            var invoiceNumber = "";
            var sale = await salesInvoiceMasterService.GetAllSalesInvoiceMaster();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.customerId <= 0 || model.customerId == null)
            {
                var MasterInfo = await salesInvoiceMasterService.GetSalesInvoiceMasterById(0);
                if (MasterInfo == null)
                {
                    MasterInfo = new SalesInvoiceMaster();
                }

                model = new SalesInvoiceViewModel
                {
                    masterId = 0,
                    itemPriceFixings = await itemPriceFixingService.GetActiveItemPriceFixing(),
                    salesInvoiceMaster = MasterInfo,
                };
                ModelState.AddModelError(string.Empty, "Customer is not properly selected");
                return View(model);
            }

            if (model.masterId > 0)
            {
                invoiceNumber = model.salesInvoiceNo;
            }
            else
            {
                var invoiceAutoNumbers = await rentInvoiceMasterService.GetAutoSalesInvoiceNoBySp(Convert.ToDateTime(model.invoiceDate).ToString("yyyyMMdd"));
                invoiceNumber = invoiceAutoNumbers.leadNumber;
            }

            if (model.masterId > 0)
            {
                try
                {
                    await salesInvoiceDetailService.DeleteSalesInvoiceDetailByMasterId(Convert.ToInt32(model.masterId));
                    await salesInvoiceMasterService.DeleteTermsConditionsByMasterId(Convert.ToInt32(model.masterId));
                }
                catch
                {
                    return RedirectToAction(nameof(InvoiceDetails), new { id = model.masterId });
                }
            }

            SalesInvoiceMaster data = new SalesInvoiceMaster
            {
                Id = Convert.ToInt32(model.masterId),
                relSupplierCustomerResourseId = model.customerId,
                invoiceDate = model.invoiceDate,
                paymentDate = model.paymentDate,
                invoiceNumber = invoiceNumber,//model.salesInvoiceNo,
                totalAmount = model.grossTotal,
                VATOnTotal = model.grossVAT,
                TAXOnTotal = model.grossTAX,
                DiscountOnTotal = model.grossDiscount,
                vds = model.vds,
                //DiscountOnTotal = model.grossDiscount,
                NetTotal = model.netTotal,
                shippingAddress = model.shippingAddress,
                shippingCost = model.shippingCost,
                alternateMobile = model.alternateMobile,
                //storeId=model.storeId,
                isClose = 0,
                isStockClose = 0
            };
            var masterId = await salesInvoiceMasterService.SaveSalesInvoiceMaster(data);

            for (int i = 0; i < model.itemPriceFixingId.Length; i++)
            {
                SalesInvoiceDetail data1 = new SalesInvoiceDetail
                {
                    Id = 0,
                    //itemPriceFixingId = model.itemPriceFixingId[i],
                    itemSpecicationId = model.itemPriceFixingId[i],
                    quantity = model.tblQuantity[i],
                    salesInvoiceMasterId = masterId,
                    disAmount = model.distotal[i],
                    vatAmount = model.vattotal[i],
                    taxAmount = model.taxtotal[i],
                    lineTotal = model.lineTotal[i],
                    rate = model.lineprice[i]

                };
                await salesInvoiceDetailService.SaveSalesInvoiceDetail(data1);
            }
            var customer = await customerService.GetRelSupplierCustomerResourseById((int)model.customerId);
            if (customer != null)
            {
                if (customer.LeadsId != null)
                {
                    var client = await clientService.GetClientsByLeadId((int)customer.LeadsId);
                    if (client != null)
                    {
                        await clientService.DeletClientsleadsByleadId(client.Id);
                    }
                    Clients clients = new Clients
                    {
                        leadsId = (int)customer.LeadsId,
                        isactive = 1,
                        isconverted = 1
                    };
                    await clientService.SaveClients(clients);
                }
            }
            if (model.context != null)
            {
                for (int i = 0; i < model.context.Length; i++)
                {
                    SalesTermsConditions data2 = new SalesTermsConditions
                    {
                        Id = 0,
                        salesInvoiceMasterId = masterId,
                        termtext = model.context[i],
                    };
                    await salesInvoiceMasterService.SaveSalesTerms(data2);
                }
            }
            return RedirectToAction(nameof(InvoiceDetails), new { id = masterId });
        }

        public async Task<IActionResult> RentCustomerList(int id)
        {
            SalesInvoiceViewModel model = new SalesInvoiceViewModel
            {
                //storeAssigns = await storeService.GetAllStoreAssignByUserId(userinfo.ID)
            };
            return View(model);
        }

        public async Task<IActionResult> RentSalesDetails()
        {
            SalesInvoiceViewModel model = new SalesInvoiceViewModel
            {
                //storeAssigns = await storeService.GetAllStoreAssignByUserId(userinfo.ID)
            };
            return View(model);
        }

        public async Task<IActionResult> ServiceSalesDetails()
        {
            SalesInvoiceViewModel model = new SalesInvoiceViewModel
            {
                //storeAssigns = await storeService.GetAllStoreAssignByUserId(userinfo.ID)
            };
            return View(model);
        }

        public async Task<IActionResult> SaleSalesDetails()
        {
            SalesInvoiceViewModel model = new SalesInvoiceViewModel
            {
                //storeAssigns = await storeService.GetAllStoreAssignByUserId(userinfo.ID)
            };
            return View(model);
        }

        public async Task<IActionResult> SalesCustomerList(int id)
        {
            SalesInvoiceViewModel model = new SalesInvoiceViewModel
            {
                //storeAssigns = await storeService.GetAllStoreAssignByUserId(userinfo.ID)
            };
            return View(model);
        }

        public async Task<IActionResult> ServiceCustomerList(int id)
        {
            SalesInvoiceViewModel model = new SalesInvoiceViewModel
            {
                //storeAssigns = await storeService.GetAllStoreAssignByUserId(userinfo.ID)
            };
            return View(model);
        }

        #endregion

        #region POS Invoice

        public async Task<IActionResult> PosIndex(int id)
        {
            var sale = await salesInvoiceMasterService.GetAllSalesInvoiceMaster();
            int Cpurchase = 0;
            Cpurchase = sale.Where(x => Convert.ToDateTime(x.invoiceDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "Pos" + '-' + idate + '-' + (Cpurchase + 1);
            ViewBag.SaleNo = issueNo;

            var MasterInfo = await salesInvoiceMasterService.GetSalesInvoiceMasterById(id);
            if (MasterInfo == null)
            {
                MasterInfo = new SalesInvoiceMaster();
            }
            string user = HttpContext.User.Identity.Name;
            var userinfo = await userInfoes.GetUserInfoByUser(user);
            SalesInvoiceViewModel model = new SalesInvoiceViewModel
            {
                masterId = id,
                itemPriceFixings = await itemPriceFixingService.GetActiveItemPriceFixing(),
                salesInvoiceMaster = MasterInfo,
                paymentModes = await purchaseOrderService.GetPaymentMode(),
                cardTypes = await purchaseOrderService.GetAllCardType(),
                bankInfos = await bankBranchService.GetAllBank(),
                mobileBankingTypes = await salaryService.GetAllWalletType(),
                discountRate = await purchaseOrderService.GetDiscountRateForSales()
            };

            //if (HttpContext.Session.GetInt32("storeId") == null)
            //{
            //    ViewBag.storeId = model.storeAssigns.Where(x => x.isDefault == "Yes").FirstOrDefault().Id;
            //}
            //else
            //{
            //    ViewBag.storeId = HttpContext.Session.GetInt32("storeId");
            //}
            return View(model);
        }

        [HttpPost]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> PosIndex([FromForm] SalesInvoiceViewModel model)
        {
            model.masterId = 0;
            var company = await iERPCompanyService.GetAllCompany();
            if (!ModelState.IsValid || model.itemPriceFixingId == null || model.itemPriceFixingId.Length <= 0)
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
            //var storedata = await storeService.GetAllStoreAssignByUserId(userinfo.ID);
            //int storeid = 0;
            //if (HttpContext.Session.GetInt32("storeId") == null)
            //{
            //    storeid = storedata.Where(x => x.isDefault == "Yes").FirstOrDefault().Id;
            //}
            //else
            //{
            //    storeid = (int)HttpContext.Session.GetInt32("storeId");
            ////}

            //if (model.paymentModeId == 1)
            //{
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

            //}
            //else if (model.paymentModeId == 2)
            //{
            //    if (model.netTotal != model.bankAmount)
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
            //        ModelState.AddModelError(string.Empty, "Total & Bank amount not same.");
            //        return View(model);
            //    }

            //}
            //else if (model.paymentModeId == 4)
            //{
            //    if (model.netTotal != model.cardAmount)
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
            //        ModelState.AddModelError(string.Empty, "Total & card amount not same.");
            //        return View(model);
            //    }

            //}
            //else if (model.paymentModeId == 5)
            //{
            //    if (model.netTotal != model.mobileAmount)
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
            //        ModelState.AddModelError(string.Empty, "Total & mobile amount not same.");
            //        return View(model);
            //    }

            //}
            //else
            //{
            //    if (model.netTotal != model.bankAmount + model.cashAmount)
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
            //        ModelState.AddModelError(string.Empty, "Total and Bank & Cash  amount not same.");
            //        return View(model);
            //    }
            //}
            //decimal bankAmount = 0;
            //decimal cashAmount = 0;
            //decimal cardAmount = 0;
            //decimal mobileAmount = 0;
            //if (model.paymentModeId == 1)
            //{
            //    cashAmount = (decimal)model.cashAmount;

            //}
            //else if (model.paymentModeId == 2)
            //{
            //    bankAmount = (decimal)model.bankAmount;

            //}
            //else if (model.paymentModeId == 4)
            //{
            //    cardAmount = (decimal)model.cardAmount;


            //}
            //else if (model.paymentModeId == 5)
            //{
            //    mobileAmount = (decimal)model.mobileAmount;

            //}
            //else
            //{
            //    cashAmount = (decimal)model.cashAmount;
            //    bankAmount = (decimal)model.bankAmount;
            //}


            if (model.netTotal != model.cashAmount)
            {
                var sale3 = await salesInvoiceMasterService.GetAllSalesInvoiceMaster();
                int Cpurchase3 = 0;
                Cpurchase3 = sale3.Where(x => Convert.ToDateTime(x.invoiceDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
                string idate3 = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
                string issueNo3 = "Pos" + '-' + idate3 + '-' + (Cpurchase3 + 1);
                ViewBag.SaleNo = issueNo3;
                model.salesInvoiceMaster = new SalesInvoiceMaster();
                model.itemPriceFixings = await itemPriceFixingService.GetActiveItemPriceFixing();

                model.paymentModes = await purchaseOrderService.GetPaymentMode();
                ModelState.AddModelError(string.Empty, "Total & Cash amount not same.");
                return View(model);
            }
            decimal cashAmount = 0;
            cashAmount = (decimal)model.cashAmount;

            var sale = await salesInvoiceMasterService.GetAllSalesInvoiceMaster();
            int Cpurchase = 0;
            Cpurchase = sale.Where(x => Convert.ToDateTime(x.invoiceDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "Pos" + '-' + idate + '-' + (Cpurchase + 1);

            if (model.masterId > 0)
            {
                issueNo = model.salesInvoiceNo;
            }
            if (model.masterId > 0)
            {
                try
                {
                    await salesInvoiceDetailService.DeleteSalesInvoiceDetailByMasterId(Convert.ToInt32(model.masterId));
                }
                catch
                {
                    return RedirectToAction(nameof(InvoiceDetails), new { id = model.masterId });
                }
            }
            int? isMember = 0;
            if (model.isMember == 1)
            {
                isMember = 1;
            }

            if (model.customerId == null || model.customerId == 0)
            {
                Leads lead = new Leads
                {
                    leadOwner = HttpContext.User.Identity.Name,
                    leadName = model.customerName,
                    leadShortName = model.customerName,
                    isPersonal = 0,
                    isClient = isMember, //For membership
                    leadNumber = model.cardNo  //For membership Number
                };

                var leadid = await leadsService.SaveLeads(lead);

                Resource resource = new Resource
                {
                    resourceName = model.customerName,
                    mobile = model.customerMobile,
                    organizationName = model.address

                };
                int resourceId = await resourceService.SaveResourceReturnId(resource);

                var roleData = await customerService.GetRoleTypeIdByName("Leads");
                int roleId = roleData.Id;

                if (Convert.ToInt32(model.customerId) == 0)
                {
                    RelSupplierCustomerResourse data2 = new RelSupplierCustomerResourse
                    {
                        Id = 0,
                        LeadsId = leadid,
                        resourceId = resourceId,
                        roleTypeId = roleId
                    };
                    model.customerId = await customerService.SaveRelSupplierCustomerResourse(data2);
                }

            }

            if (model.grossDiscount == null)
            {
                model.grossDiscount = 0;
            }
            if (model.grossVAT == null)
            {
                model.grossVAT = 0;
            }

            SalesInvoiceMaster data = new SalesInvoiceMaster
            {
                Id = Convert.ToInt32(model.masterId),
                relSupplierCustomerResourseId = model.customerId,
                invoiceDate = model.invoiceDate,
                paymentDate = model.invoiceDate,
                invoiceNumber = issueNo,//model.salesInvoiceNo,
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

            //Stock start 
            //var stock = await stockService.GetAllStockMaster(2);
            //int Cpurchase2 = 0;
            //Cpurchase2 = stock.Where(x => Convert.ToDateTime(x.StockDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            //string idate2 = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            //string issueNo2 = "SRNO" + '-' + idate2 + '-' + (Cpurchase2 + 1);

            //StockMaster stockMaster = new StockMaster
            //{
            //    companyId = 1,
            //    remarks = "POS Stock",
            //    MRNO = issueNo2,//model.MRNO,
            //    StockDate = model.invoiceDate,
            //    stockStatusId = 2,
            //    storeId = storeid

            //};
            //int stockMasterId = await stockService.SaveStockMaster(stockMaster);


            for (int i = 0; i < model.itemPriceFixingId.Length; i++)
            {
                SalesInvoiceDetail data1 = new SalesInvoiceDetail
                {
                    Id = 0,
                    itemSpecicationId = model.itemPriceFixingId[i],
                    quantity = model.tblQuantity[i],
                    rate = model.priceList[i],
                    salesInvoiceMasterId = masterId
                };
                await salesInvoiceDetailService.SaveSalesInvoiceDetail(data1);
                //if (model.barcode[i].StartsWith("P"))
                //{
                //    var datalist = await offerService.GetOfferDetailsByspecId((int)model.itemPriceFixingId[i]);
                //    foreach (var l in datalist)
                //    {


                //        Stock stockData = new Stock
                //        {
                //            stockMasterId = stockMasterId,
                //            posInvoiceDetailId = detailsId,
                //            rate = l.itemPriceFixing.price,
                //            itemSpecificationId = l.itemPriceFixing.itemSpecificationId,
                //            qty = l.quantity * model.tblQuantity[i],

                //        };
                //        await stockService.SaveStock(stockData);
                //    }

                //}
                //else
                //{


                //    Stock stockData = new Stock
                //    {
                //        stockMasterId = stockMasterId,
                //        posInvoiceDetailId = detailsId,
                //        rate = model.priceList[i],
                //        itemSpecificationId = model.IdOriginal[i],
                //        qty = model.tblQuantity[i],

                //    };
                //    await stockService.SaveStock(stockData);
                //}

            }

            //Collection Part start
            var purchase = await salesCollectionService.GetAllSalesCollection();
            int Cpurchase1 = 0;
            Cpurchase1 = purchase.Where(x => Convert.ToDateTime(x.collectionDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            string idate1 = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo1 = "Pos-Collection-No-" + '-' + idate1 + '-' + (Cpurchase1 + 1);

            int saleCollectionId = 0;

            //if (model.paymentModeId == 1 || model.paymentModeId == 2 || model.paymentModeId == 3)
            //{
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
            //        bankAmount = bankAmount
            //    };
            //    saleCollectionId = await salesCollectionService.SaveSalesCollection(posCollectionMaster);
            //}
            //else if (model.paymentModeId == 4)
            //{
            //    SalesCollection posCollectionMaster = new SalesCollection
            //    {
            //        relSupplierCustomerResourseId = model.customerId,
            //        collectionNumber = issueNo1,
            //        collectionAmount = model.netTotal,
            //        collectionDate = model.invoiceDate,
            //        companyId = company.OrderByDescending(a => a.Id).FirstOrDefault().Id,
            //        remarks = "Pos Payment",
            //        paymentModeId = model.paymentModeId,
            //        cardAmount = model.cardAmount,
            //        cardTypeId = model.cardTypeId,
            //        bankInfoId = model.bankInfoId
            //    };
            //    saleCollectionId = await salesCollectionService.SaveSalesCollection(posCollectionMaster);
            //}
            //else if (model.paymentModeId == 5)
            //{
            //    SalesCollection posCollectionMaster = new SalesCollection
            //    {
            //        relSupplierCustomerResourseId = model.customerId,
            //        collectionNumber = issueNo1,
            //        collectionAmount = model.netTotal,
            //        collectionDate = model.invoiceDate,
            //        companyId = company.OrderByDescending(a => a.Id).FirstOrDefault().Id,
            //        remarks = "Pos Payment",
            //        paymentModeId = model.paymentModeId,
            //        mobileAmount = model.mobileAmount,
            //        walletTypeId = model.mobileBankingTypeId,
            //        trxNo = model.trxNumber
            //    };
            //    saleCollectionId = await salesCollectionService.SaveSalesCollection(posCollectionMaster);
            //}


            SalesCollection posCollectionMaster = new SalesCollection
            {
                relSupplierCustomerResourseId = model.customerId,
                collectionNumber = issueNo1,
                collectionAmount = model.netTotal,
                collectionDate = model.invoiceDate,
                companyId = company.OrderByDescending(a => a.Id).FirstOrDefault().Id,
                remarks = "Pos Payment",
                paymentModeId = model.paymentModeId,
                cashAmount = cashAmount,
                
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

        [AllowAnonymous]
        public IActionResult POSInvoicePDFData(int id)
        {
            string userName = HttpContext.User.Identity.Name;
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/OtherSales/SalesInvoice/POSInvoicePDF?id=" + id;

            string fileName;
            string status = myPDF.GeneratePOSReceiptPDF(out fileName, url);

            FileName = fileName;
            if (status != "done")
            {
                return Content("<h1>" + status + "</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> POSInvoicePDF(int id)
        {
            ViewBag.masterId = id;

            SalesInvoiceViewModel model = new SalesInvoiceViewModel
            {
                salesInvoiceMaster = await salesInvoiceMasterService.GetSalesInvoiceMasterById(id),
                salesInvoiceDetails = await salesInvoiceDetailService.GetAllSalesInvoiceDetailByMasterId(id),
                companies = await iERPCompanyService.GetAllCompany(),
            };
            ViewBag.InWord = AmountInWord.ConvertToWords(model.salesInvoiceMaster.NetTotal.ToString());

            return View(model);
        }

        public async Task<IActionResult> POSInvoiceList()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetPosInvoiceList(string FDate, string TDate)
        {
            return Json(await salesInvoiceDetailService.GetPosInvoiceList(FDate, TDate));
        }

        //[Route("global/api/GetPosInvoiceList/{fromDate}/{toDate}")]
        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> GetPosInvoiceList(string fromDate, string toDate)
        //{
        //    return Json(await salesInvoiceDetailService.GetPosInvoiceList(fromDate, toDate));
        //}

        #endregion

        #region POS New

        public async Task<IActionResult> PosIndexNew(int id)
        {
            var sale = await salesInvoiceMasterService.GetAllSalesInvoiceMaster();
            int Cpurchase = 0;
            Cpurchase = sale.Where(x => Convert.ToDateTime(x.invoiceDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "Pos" + '-' + idate + '-' + (Cpurchase + 1);
            ViewBag.SaleNo = issueNo;

            var MasterInfo = await salesInvoiceMasterService.GetSalesInvoiceMasterById(id);
            if (MasterInfo == null)
            {
                MasterInfo = new SalesInvoiceMaster();
            }
            string user = HttpContext.User.Identity.Name;
            var userinfo = await userInfoes.GetUserInfoByUser(user);
            SalesInvoiceViewModel model = new SalesInvoiceViewModel
            {
                masterId = id,
                itemPriceFixings = await itemPriceFixingService.GetActiveItemPriceFixing(),
                salesInvoiceMaster = MasterInfo,
                paymentModes = await purchaseOrderService.GetPaymentMode(),
                cardTypes = await purchaseOrderService.GetAllCardType(),
                bankInfos = await bankBranchService.GetAllBank(),
                mobileBankingTypes = await salaryService.GetAllWalletType(),
                discountRate = await purchaseOrderService.GetDiscountRateForSales()
            };

            return View(model);
        }

        #endregion

        #region POS MENU
        public async Task<IActionResult> PosMenu()
        {
            SalesInvoiceViewModel model = new SalesInvoiceViewModel
            {
                salesMenuCategories = await paymentModeService.GetAllSalesMenuCategory(),
                menuItemViewModels = await paymentModeService.GetAllMenuItemByCategory(2),
                paymentModes = await purchaseOrderService.GetPaymentMode(),
                cardTypes = await purchaseOrderService.GetAllCardType(),
                bankInfos = await bankBranchService.GetAllBank(),
                mobileBankingTypes = await salaryService.GetAllWalletType(),
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSalesMenuByCategory(int? catId)
        {
            var data = await paymentModeService.GetAllMenuItemByCategory(catId);
            return Json(data);
        }

        #endregion

        #region Rent Invoice Index 
        public async Task<IActionResult> RentIndex(int id, int? agreementId)
        {
            //var sale = await salesInvoiceMasterService.GetAllSalesInvoiceMaster();
            //int Cpurchase = 0;
            //Cpurchase = sale.Where(x => Convert.ToDateTime(x.invoiceDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            //string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            //string issueNo = "Sale" + '-' + idate + '-' + (Cpurchase + 1);

            int masterId = Convert.ToInt32(id);
            var invoiceNumber = "";
            if (masterId != 0)
            {
                var masterdata = await salesInvoiceMasterService.GetSalesInvoiceMasterById(id);
                invoiceNumber = masterdata.invoiceNumber;
            }
            else
            {
                var invoiceAutoNumbers = await salesInvoiceMasterService.GetAutoRentSaleInvoiceNoBySp(Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd"));
                invoiceNumber = invoiceAutoNumbers.leadNumber;
            }
            ViewBag.SaleNo = invoiceNumber;


            var MasterInfo = await salesInvoiceMasterService.GetSalesInvoiceMasterById(id);
            if (MasterInfo == null)
            {
                MasterInfo = new SalesInvoiceMaster();
            }
            string user = HttpContext.User.Identity.Name;
            var userinfo = await userInfoes.GetUserInfoByUser(user);
            SalesInvoiceViewModel model = new SalesInvoiceViewModel
            {
                masterId = id,
                itemPriceFixings = await itemPriceFixingService.GetActiveItemPriceFixing(),
                salesInvoiceMaster = MasterInfo,
                termsAndConditions = await rentInvoiceMasterService.GetAllTermsAndConditions()
                //storeAssigns = await storeService.GetAllStoreAssignByUserId(userinfo.ID)
            };
            if (agreementId == null)
            {
                agreementId = 0;
            }
            var agreementmaster = await rentInvoiceMasterService.GetSalesInvoiceMasterById((int)agreementId);
            if (agreementmaster != null)
            {
                var contacts = await contactsService.GetAllContactsbyLeadId((int)agreementmaster.relSupplierCustomerResourse.LeadsId);
                ViewBag.agreementId = agreementId;
                ViewBag.agreementName = agreementmaster.invoiceNumber;

                ViewBag.CustomerName = agreementmaster.relSupplierCustomerResourse.Leads.leadName;
                ViewBag.relId = agreementmaster.relSupplierCustomerResourseId;
                var dataaddress = await AddressService.GetAddress();

                dataaddress = dataaddress.Where(x => x.leadsId == agreementmaster.relSupplierCustomerResourse.LeadsId);
                ViewBag.present = dataaddress.FirstOrDefault().maillingAddress;

                var resources = await resourceService.GetResourceById((int)contacts.FirstOrDefault().resourceId);
                ViewBag.mobileNo = resources?.mobile;

            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RentIndex([FromForm] SalesInvoiceViewModel model)
        {
            var invoiceNumber = "";
            var sale = await salesInvoiceMasterService.GetAllSalesInvoiceMaster();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.customerId <= 0 || model.customerId == null)
            {
                var invoiceAutoNumbers = await salesInvoiceMasterService.GetAutoRentSaleInvoiceNoBySp(Convert.ToDateTime(model.invoiceDate).ToString("yyyyMMdd"));
                invoiceNumber = invoiceAutoNumbers.leadNumber;

                var MasterInfo = await salesInvoiceMasterService.GetSalesInvoiceMasterById(0);
                if (MasterInfo == null)
                {
                    MasterInfo = new SalesInvoiceMaster();
                }

                model = new SalesInvoiceViewModel
                {
                    masterId = 0,
                    itemPriceFixings = await itemPriceFixingService.GetActiveItemPriceFixing(),
                    salesInvoiceMaster = MasterInfo,
                };
                ModelState.AddModelError(string.Empty, "Customer is not properly selected");
                return View(model);
            }
            else
            {
                invoiceNumber = model.salesInvoiceNo;
            }
            if (model.masterId > 0)
            {
                try
                {
                    await salesInvoiceDetailService.DeleteSalesInvoiceDetailByMasterId(Convert.ToInt32(model.masterId));
                    await salesInvoiceMasterService.DeleteTermsConditionsByMasterId(Convert.ToInt32(model.masterId));
                }
                catch
                {
                    return RedirectToAction(nameof(InvoiceDetails), new { id = model.masterId });
                }
            }



            SalesInvoiceMaster data = new SalesInvoiceMaster
            {
                Id = Convert.ToInt32(model.masterId),
                relSupplierCustomerResourseId = model.customerId,
                invoiceDate = model.invoiceDate,
                paymentDate = model.paymentDate,
                invoiceNumber = invoiceNumber, // model.salesInvoiceNo, 
                totalAmount = model.grossTotal,
                VATOnTotal = model.grossVAT,
                TAXOnTotal = model.grossTAX,
                DiscountOnTotal = model.grossDiscount,
                vds = model.vds,
                shippingCost = model.shippingCost,
                alternateMobile = model.alternateMobile,
                //DiscountOnTotal = model.grossDiscount,
                NetTotal = model.netTotal,

                //storeId=model.storeId,
                isClose = 0,
                isStockClose = 0
            };
            var masterId = await salesInvoiceMasterService.SaveSalesInvoiceMaster(data);

            try
            {
                for (int i = 0; i < model.itemPriceFixingId.Length; i++)
                {
                    SalesInvoiceDetail data1 = new SalesInvoiceDetail
                    {
                        //Id = 0,
                        //itemPriceFixingId = model.itemPriceFixingId[i],
                        itemSpecicationId = model.itemPriceFixingId[i],
                        quantity = model.tblQuantity[i],
                        salesInvoiceMasterId = masterId,
                        disAmount = model.distotal[i],
                        vatAmount = model.vattotal[i],
                        taxAmount = model.taxtotal[i],
                        lineTotal = model.lineTotal[i],
                        rate = model.lineprice[i],
                        rentInvoiceDetailId = model.TrentdetailId

                    };
                    await salesInvoiceDetailService.SaveSalesInvoiceDetail(data1);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            var customer = await customerService.GetRelSupplierCustomerResourseById((int)model.customerId);
            if (customer != null)
            {
                if (customer.LeadsId != null)
                {
                    var client = await clientService.GetClientsByLeadId((int)customer.LeadsId);
                    if (client != null)
                    {
                        await clientService.DeletClientsleadsByleadId(client.Id);
                    }

                    Clients clients = new Clients
                    {
                        leadsId = (int)customer.LeadsId,
                        isactive = 1,
                        isconverted = 1
                    };
                    await clientService.SaveClients(clients);
                }


            }
            if (model.context != null)
            {
                for (int i = 0; i < model.context.Length; i++)
                {
                    SalesTermsConditions data2 = new SalesTermsConditions
                    {
                        Id = 0,
                        salesInvoiceMasterId = masterId,
                        termtext = model.context[i],
                    };
                    await salesInvoiceMasterService.SaveSalesTerms(data2);
                }
            }

            return RedirectToAction(nameof(RentInvoiceDetails), new { id = masterId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InsertCondition([FromForm] SalesInvoiceViewModel model)
        {
            await salesInvoiceMasterService.DeleteTermsConditionsByMasterId((int)model.masterId);
            if (model.context != null)
            {
                for (int i = 0; i < model.context.Length; i++)
                {
                    SalesTermsConditions data2 = new SalesTermsConditions
                    {
                        Id = 0,
                        salesInvoiceMasterId = model.masterId,
                        termtext = model.context[i],
                    };
                    await salesInvoiceMasterService.SaveSalesTerms(data2);
                }
            }

            return RedirectToAction(nameof(RentInvoiceDetails), new { id = model.masterId });
            // return RedirectToAction("InvoiceDetails", "RentInvoice", new { id = model.masterId, Area = "Rental" });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InsertConditionS([FromForm] SalesInvoiceViewModel model)
        {
            await salesInvoiceMasterService.DeleteTermsConditionsByMasterId((int)model.masterId);
            if (model.context != null)
            {
                for (int i = 0; i < model.context.Length; i++)
                {
                    SalesTermsConditions data2 = new SalesTermsConditions
                    {
                        Id = 0,

                        salesInvoiceMasterId = model.masterId,
                        termtext = model.context[i],
                    };
                    await salesInvoiceMasterService.SaveSalesTerms(data2);
                }
            }
            return RedirectToAction(nameof(InvoiceDetails), new { id = model.masterId });
            // return RedirectToAction("InvoiceDetails", "RentInvoice", new { id = model.masterId, Area = "Rental" });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InsertConditionP([FromForm] SalesInvoiceViewModel model)
        {
            await salesInvoiceMasterService.DeleteTermsConditionsByMasterId((int)model.masterId);
            if (model.context != null)
            {
                for (int i = 0; i < model.context.Length; i++)
                {
                    SalesTermsConditions data2 = new SalesTermsConditions
                    {
                        Id = 0,

                        salesInvoiceMasterId = model.masterId,
                        termtext = model.context[i],
                    };
                    await salesInvoiceMasterService.SaveSalesTerms(data2);
                }
            }


            return RedirectToAction(nameof(PatInvoiceDetails), new { id = model.masterId });
            // return RedirectToAction("InvoiceDetails", "RentInvoice", new { id = model.masterId, Area = "Rental" });

        }
        [HttpGet]
        public async Task<IActionResult> GetAutoRentSaleInvoiceNoBySp(string invoiceDate)
        {
            return Json(await salesInvoiceMasterService.GetAutoRentSaleInvoiceNoBySp(Convert.ToDateTime(invoiceDate).ToString("yyyyMMdd")));
        }

        #endregion

        #region PatIndex

        public async Task<IActionResult> PatIndex(int id, int? agreementId)
        {
            int masterId = Convert.ToInt32(id);
            var invoiceNumber = "";
            if (masterId != 0)
            {
                var masterdata = await salesInvoiceMasterService.GetSalesInvoiceMasterById(id);
                invoiceNumber = masterdata.invoiceNumber;
            }
            else
            {
                var invoiceAutoNumbers = await salesInvoiceMasterService.GetAutoRentServiceInvoiceNoBySp(Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd"));
                invoiceNumber = invoiceAutoNumbers.leadNumber;
            }

            ViewBag.SaleNo = invoiceNumber;


            var MasterInfo = await salesInvoiceMasterService.GetSalesInvoiceMasterById(id);
            if (MasterInfo == null)
            {
                MasterInfo = new SalesInvoiceMaster();
            }
            string user = HttpContext.User.Identity.Name;
            var userinfo = await userInfoes.GetUserInfoByUser(user);
            SalesInvoiceViewModel model = new SalesInvoiceViewModel
            {
                masterId = id,
                itemPriceFixings = await itemPriceFixingService.GetActiveItemPriceFixing(),
                salesInvoiceMaster = MasterInfo,
                termsAndConditions = await rentInvoiceMasterService.GetAllTermsAndConditions()
                //storeAssigns = await storeService.GetAllStoreAssignByUserId(userinfo.ID)
            };
            if (agreementId == null)
            {
                agreementId = 0;
            }
            var agreementmaster = new Leads();
            if (agreementId != 0)
            {
                agreementmaster = await leadsService.GetLeadsById((int)agreementId);
            }
            var contacts = await contactsService.GetAllContactsbyLeadId(agreementmaster.Id);

            var resource = new Resource();
            if (contacts.Count() != 0)
            {
                resource = await resourceService.GetResourceById((int)contacts.LastOrDefault().resourceId);
            }
            var reldata = await customerService.GetRelSupplierCustomerResourseByLeadId(agreementmaster.Id);
            if (agreementmaster.Id != 0)
            {
                ViewBag.agreementId = agreementId;
                ViewBag.relId = reldata.Id;
                var dataaddress = await AddressService.GetAddress();
                dataaddress = dataaddress.Where(x => x.leadsId == agreementmaster.Id);
                ViewBag.present = dataaddress.FirstOrDefault().maillingAddress;
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetResourceInformationById(int? agreementId)
        {
            var agreementmaster = await leadsService.GetLeadsById((int)agreementId);
            var contacts = await contactsService.GetAllContactsbyLeadId(agreementmaster.Id);
            var data = await resourceService.GetResourceById((int)contacts.LastOrDefault().resourceId);
            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetResourceInformationByLeadId(int? agreementId)
        {
            var agreementmaster = await leadsService.GetLeadsById((int)agreementId);
            var contacts = await contactsService.GetOwnContactsbyLeadId(agreementmaster.Id);
            var data = await resourceService.GetResourceById((int)contacts.LastOrDefault().resourceId);
            return Json(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PatIndex([FromForm] SalesInvoiceViewModel model)
        {
            var invoiceNumber = "";
            var sale = await salesInvoiceMasterService.GetAllSalesInvoiceMaster();

            if (model.customerId <= 0 || model.customerId == null)
            {
                var MasterInfo = await salesInvoiceMasterService.GetSalesInvoiceMasterById(0);
                if (MasterInfo == null)
                {
                    MasterInfo = new SalesInvoiceMaster();
                }

                model = new SalesInvoiceViewModel
                {
                    masterId = 0,
                    itemPriceFixings = await itemPriceFixingService.GetActiveItemPriceFixing(),
                    salesInvoiceMaster = MasterInfo,
                };
                ModelState.AddModelError(string.Empty, "Customer is not properly selected");
                return View(model);
            }
            if (model.masterId > 0)
            {
                invoiceNumber = model.salesInvoiceNo;
            }
            else
            {
                var invoiceAutoNumbers = await salesInvoiceMasterService.GetAutoRentServiceInvoiceNoBySp(Convert.ToDateTime(model.invoiceDate).ToString("yyyyMMdd"));
                invoiceNumber = invoiceAutoNumbers.leadNumber;
            }

            if (model.masterId > 0)
            {
                try
                {
                    await salesInvoiceDetailService.DeleteSalesInvoiceDetailByMasterId(Convert.ToInt32(model.masterId));
                    await salesInvoiceMasterService.DeleteTermsConditionsByMasterId(Convert.ToInt32(model.masterId));
                }
                catch
                {
                    return RedirectToAction(nameof(InvoiceDetails), new { id = model.masterId });
                }
            }

            SalesInvoiceMaster data = new SalesInvoiceMaster
            {
                Id = Convert.ToInt32(model.masterId),
                relSupplierCustomerResourseId = model.customerId,
                invoiceDate = model.invoiceDate,
                paymentDate = model.paymentDate,
                invoiceNumber = invoiceNumber,//model.salesInvoiceNo, //issueNo
                totalAmount = model.grossTotal,
                VATOnTotal = model.grossVAT,
                TAXOnTotal = model.grossTAX,
                DiscountOnTotal = model.grossDiscount,
                vds = model.vds,
                //DiscountOnTotal = model.grossDiscount,
                NetTotal = model.netTotal,
                shippingCost = model.shippingCost,
                alternateMobile = model.alternateMobile,
                //storeId=model.storeId,
                isClose = 0,
                isStockClose = 0
            };
            var masterId = await salesInvoiceMasterService.SaveSalesInvoiceMaster(data);

            for (int i = 0; i < model.itemPriceFixingId.Length; i++)
            {
                SalesInvoiceDetail data1 = new SalesInvoiceDetail
                {
                    Id = 0,
                    //itemPriceFixingId = model.itemPriceFixingId[i],
                    itemSpecicationId = model.itemPriceFixingId[i],
                    quantity = model.tblQuantity[i],
                    salesInvoiceMasterId = masterId,
                    disAmount = model.distotal[i],
                    vatAmount = model.vattotal[i],
                    taxAmount = model.taxtotal[i],
                    lineTotal = model.lineTotal[i],
                    rate = model.lineprice[i],
                    patientServiceDetailsId = model.rendetailId[i]

                };
                await salesInvoiceDetailService.SaveSalesInvoiceDetail(data1);
            }
            var customer = await customerService.GetRelSupplierCustomerResourseById((int)model.customerId);
            if (customer != null)
            {
                if (customer.LeadsId != null)
                {
                    var client = await clientService.GetClientsByLeadId((int)customer.LeadsId);
                    if (client != null)
                    {
                        await clientService.DeletClientsleadsByleadId(client.Id);
                    }
                    Clients clients = new Clients
                    {
                        leadsId = (int)customer.LeadsId,
                        isactive = 1,
                        isconverted = 1
                    };
                    await clientService.SaveClients(clients);
                }


            }
            if (model.context != null)
            {
                for (int i = 0; i < model.context.Length; i++)
                {
                    SalesTermsConditions data2 = new SalesTermsConditions
                    {
                        Id = 0,
                        salesInvoiceMasterId = masterId,
                        termtext = model.context[i],
                    };
                    await salesInvoiceMasterService.SaveSalesTerms(data2);
                }
            }
            return RedirectToAction(nameof(PatInvoiceDetails), new { id = masterId });
        }




        [HttpGet]
        public async Task<IActionResult> GetAutoSalesInvoiceNoBySp(string invoiceDate)
        {
            return Json(await salesInvoiceMasterService.GetAutoSalesInvoiceNoBySp(Convert.ToDateTime(invoiceDate).ToString("yyyyMMdd")));
        }

        [HttpGet]
        public async Task<IActionResult> GetAutoRentServiceInvoiceNoBySp(string invoiceDate)
        {
            return Json(await salesInvoiceMasterService.GetAutoRentServiceInvoiceNoBySp(Convert.ToDateTime(invoiceDate).ToString("yyyyMMdd")));
        }

        #endregion

        #region InvoiceList
        public async Task<IActionResult> InvoiceList()
        {
            var data = await salesInvoiceMasterService.GetAllSalesInvoiceMaster();


            //  var data = await salesInvoiceMasterService.GetAllRantalListwithDateViewModel();
            if (data == null)
            {
                data = new List<SalesInvoiceMaster>();
            }
            var dataaddress = await AddressService.GetAddress();
            List<int?> relids = data.Select(x => x.relSupplierCustomerResourse.resourceId).ToList();
            List<int?> leadsid = data.Select(x => x.relSupplierCustomerResourse.LeadsId).ToList();
            dataaddress = dataaddress.Where(x => leadsid.Contains(x.leadsId));
            var contacts = await contactsService.GetAllContacts();
            contacts = contacts.Where(x => relids.Contains(x.resourceId));
            var resources = await resourceService.GetAllResource();
            resources = resources.Where(x => relids.Contains(x.Id));

            SalesInvoiceViewModel model = new SalesInvoiceViewModel
            {
                salesInvoiceMasters = data,// await salesInvoiceMasterService.GetAllSalesInvoiceMaster(),
                GetAddresses = dataaddress,
                Contacts = contacts,
                Resources = resources
            };
            return View(model);
        }
        public async Task<IActionResult> RentInvoiceList()
        {
            var data = await salesInvoiceMasterService.GetAllSalesInvoiceMaster();


            //  var data = await salesInvoiceMasterService.GetAllRantalListwithDateViewModel();
            if (data == null)
            {
                data = new List<SalesInvoiceMaster>();
            }
            var dataaddress = await AddressService.GetAddress();
            List<int?> relids = data.Select(x => x.relSupplierCustomerResourse.LeadsId).ToList();
            List<int?> leadsid = data.Select(x => x.relSupplierCustomerResourse.LeadsId).ToList();
            dataaddress = dataaddress.Where(x => leadsid.Contains(x.leadsId));
            var contacts = await contactsService.GetAllContacts();
            contacts = contacts.Where(x => relids.Contains(x.leadsId));
            List<int?> resourseids = contacts.Select(x => x.resourceId).ToList();
            var resources = await resourceService.GetAllResource();
            resources = resources.Where(x => resourseids.Contains(x.Id));

            SalesInvoiceViewModel model = new SalesInvoiceViewModel
            {
                salesInvoiceMasters = data,// await salesInvoiceMasterService.GetAllSalesInvoiceMaster(),
                GetAddresses = dataaddress,
                Contacts = contacts,
                Resources = resources
            };
            return View(model);
        }
        public async Task<IActionResult> PatInvoiceList()
        {
            var data = await salesInvoiceMasterService.GetAllSalesInvoiceMaster();


            //  var data = await salesInvoiceMasterService.GetAllRantalListwithDateViewModel();
            if (data == null)
            {
                data = new List<SalesInvoiceMaster>();
            }
            var dataaddress = await AddressService.GetAddress();
            List<int?> relids = data.Select(x => x.relSupplierCustomerResourse.resourceId).ToList();
            List<int?> leadsid = data.Select(x => x.relSupplierCustomerResourse.LeadsId).ToList();
            dataaddress = dataaddress.Where(x => leadsid.Contains(x.leadsId));
            var contacts = await contactsService.GetAllContacts();
            contacts = contacts.Where(x => relids.Contains(x.resourceId));
            var resources = await resourceService.GetAllResource();
            resources = resources.Where(x => relids.Contains(x.Id));

            SalesInvoiceViewModel model = new SalesInvoiceViewModel
            {
                salesInvoiceMasters = data,// await salesInvoiceMasterService.GetAllSalesInvoiceMaster(),
                GetAddresses = dataaddress,
                Contacts = contacts,
                Resources = resources
            };
            return View(model);
        }
        public async Task<IActionResult> ReturnInvoiceList()
        {
            SalesInvoiceViewModel model = new SalesInvoiceViewModel
            {
                salesReturnInvoiceMasters = await salesInvoiceMasterService.GetAllSalesReturnInvoiceMaster(),
            };
            return View(model);
        }

        #endregion

        #region Details

        public async Task<IActionResult> InvoiceDetails(int id)
        {
            ViewBag.masterId = id;
            var CommentInfo = await attachmentCommentService.GetCommentByActionTypeId(id, "SalesInvoice", 4);
            if (CommentInfo == null)
            {
                CommentInfo = new List<Comment>();
            }
            var photoInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "SalesInvoice", "photo", 4);
            if (photoInfo == null)
            {
                photoInfo = new List<DocumentPhotoAttachment>();
            }
            var docInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "SalesInvoice", "Document", 4);
            if (docInfo == null)
            {
                docInfo = new List<DocumentPhotoAttachment>();
            }
            var invoicedata = await salesInvoiceMasterService.GetSalesInvoiceMasterById(id);
            var data = await addressesService.GetAddressesByLeadId((int)invoicedata.relSupplierCustomerResourse.LeadsId);
            int? relids = invoicedata.relSupplierCustomerResourseId;
            var cusdata = await customerService.GetRelSupplierCustomerResourseByRoleId(0);
            cusdata = cusdata.Where(x => x.relId == relids);
            ViewBag.mobile = cusdata.FirstOrDefault().mobile;

            SalesInvoiceViewModel model = new SalesInvoiceViewModel
            {
                salesInvoiceMaster = invoicedata,//await salesInvoiceMasterService.GetSalesInvoiceMasterById(id),
                salesInvoiceDetails = await salesInvoiceDetailService.GetAllSalesInvoiceDetailByMasterId(id),
                comments = CommentInfo,
                photoes = photoInfo,
                documents = docInfo,
                termsAndConditions = await rentInvoiceMasterService.GetAllTermsAndConditions()
            };
            ViewBag.InWord = AmountInWord.ConvertToWords(model.salesInvoiceMaster.totalAmount.ToString());

            return View(model);
        }

        public async Task<IActionResult> RentInvoiceDetails(int id)
        {
            ViewBag.masterId = id;
            var CommentInfo = await attachmentCommentService.GetCommentByActionTypeId(id, "SalesInvoice", 4);
            if (CommentInfo == null)
            {
                CommentInfo = new List<Comment>();
            }
            var photoInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "SalesInvoice", "photo", 4);
            if (photoInfo == null)
            {
                photoInfo = new List<DocumentPhotoAttachment>();
            }
            var docInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "SalesInvoice", "Document", 4);
            if (docInfo == null)
            {
                docInfo = new List<DocumentPhotoAttachment>();
            }
            var invoicedata = await salesInvoiceMasterService.GetSalesInvoiceMasterById(id);
            var data = await addressesService.GetAddressesByLeadId((int)invoicedata.relSupplierCustomerResourse.LeadsId);
            int? relids = invoicedata.relSupplierCustomerResourseId;
            var cusdata = await customerService.GetRelSupplierCustomerResourseByRoleId(0);
            cusdata = cusdata.Where(x => x.relId == relids);
            ViewBag.mobile = cusdata.FirstOrDefault().mobile;

            SalesInvoiceViewModel model = new SalesInvoiceViewModel
            {
                salesInvoiceMaster = invoicedata,//await salesInvoiceMasterService.GetSalesInvoiceMasterById(id),
                salesInvoiceDetails = await salesInvoiceDetailService.GetAllSalesInvoiceDetailByMasterId(id),
                comments = CommentInfo,
                photoes = photoInfo,
                documents = docInfo,
                termsAndConditions = await rentInvoiceMasterService.GetAllTermsAndConditions()
            };
            ViewBag.InWord = AmountInWord.ConvertToWords(model.salesInvoiceMaster.totalAmount.ToString());

            return View(model);
        }

        public async Task<IActionResult> PatInvoiceDetails(int id)
        {
            ViewBag.masterId = id;
            var CommentInfo = await attachmentCommentService.GetCommentByActionTypeId(id, "SalesInvoice", 4);
            if (CommentInfo == null)
            {
                CommentInfo = new List<Comment>();
            }
            var photoInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "SalesInvoice", "photo", 4);
            if (photoInfo == null)
            {
                photoInfo = new List<DocumentPhotoAttachment>();
            }
            var docInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "SalesInvoice", "Document", 4);
            if (docInfo == null)
            {
                docInfo = new List<DocumentPhotoAttachment>();
            }

            var invoicedata = await salesInvoiceMasterService.GetSalesInvoiceMasterById(id);
            var data = await addressesService.GetAddressesByLeadId((int)invoicedata.relSupplierCustomerResourse.LeadsId);
            int? relids = invoicedata.relSupplierCustomerResourseId;
            var cusdata = await customerService.GetRelSupplierCustomerResourseByRoleId(0);
            var patientList = await paymentModeService.GetAllPatientServiceById(id);

            patientList = patientList.Where(x => x.Id == id);
            cusdata = cusdata.Where(x => x.relId == relids);
            ViewBag.mobile = cusdata.FirstOrDefault().mobile;
            //if (patientList.FirstOrDefault().billingType !=null)
            //{
            //    ViewBag.billType = patientList.FirstOrDefault().billingType.ToString();
            //}
            
            //ViewBag.fromDate = patientList.FirstOrDefault().serviceFromDate;
            //ViewBag.toDate = patientList.FirstOrDefault().serviceToDate;


            SalesInvoiceViewModel model = new SalesInvoiceViewModel
            {
                salesInvoiceMaster = invoicedata,// await salesInvoiceMasterService.GetSalesInvoiceMasterById(id),
                salesInvoiceDetails = await salesInvoiceDetailService.GetAllSalesInvoiceDetailByMasterId(id),
                PatientRepresentatives = await homeCareService.GetAllPatientRepresentativeByPatientId(invoicedata.relSupplierCustomerResourse.LeadsId),
                comments = CommentInfo,
                patientServices=await homeCareService.GetAllPatientServiceByPatientId(invoicedata.relSupplierCustomerResourse.LeadsId),
                photoes = photoInfo,
                documents = docInfo,
                termsAndConditions = await rentInvoiceMasterService.GetAllTermsAndConditions()
            };
            ViewBag.InWord = AmountInWord.ConvertToWords(model.salesInvoiceMaster.totalAmount.ToString());

            return View(model);
        }

        public async Task<IActionResult> ReturnInvoiceDetails(int id)
        {
            ViewBag.masterId = id;
            var CommentInfo = await attachmentCommentService.GetCommentByActionTypeId(id, "SalesReturnInvoice", 4);
            if (CommentInfo == null)
            {
                CommentInfo = new List<Comment>();
            }
            var photoInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "SalesReturnInvoice", "photo", 4);
            if (photoInfo == null)
            {
                photoInfo = new List<DocumentPhotoAttachment>();
            }
            var docInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "SalesReturnInvoice", "Document", 4);
            if (docInfo == null)
            {
                docInfo = new List<DocumentPhotoAttachment>();
            }


            SalesInvoiceViewModel model = new SalesInvoiceViewModel
            {
                salesReturnInvoiceMaster = await salesInvoiceMasterService.GetSalesReturnInvoiceMasterById(id),
                salesReturnInvoiceDetails = await salesInvoiceDetailService.GetAllSalesReturnInvoiceDetailByMasterId(id),
                comments = CommentInfo,
                photoes = photoInfo,
                documents = docInfo,
            };
            ViewBag.InWord = AmountInWord.ConvertToWords(model.salesReturnInvoiceMaster.totalAmount.ToString());

            return View(model);
        }

        #endregion

        #region Report

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> PatInvoicePDF(int id, string userName)
        {
            ViewBag.masterId = id;
            var CommentInfo = await attachmentCommentService.GetCommentByActionTypeId(id, "SalesInvoice", 4);
            if (CommentInfo == null)
            {
                CommentInfo = new List<Comment>();
            }
            var photoInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "SalesInvoice", "photo", 4);
            if (photoInfo == null)
            {
                photoInfo = new List<DocumentPhotoAttachment>();
            }
            var docInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "SalesInvoice", "Document", 4);
            if (docInfo == null)
            {
                docInfo = new List<DocumentPhotoAttachment>();
            }

            var invoicedata = await salesInvoiceMasterService.GetSalesInvoiceMasterById(id);
            int? relids = invoicedata.relSupplierCustomerResourseId;
            var cusdata = await customerService.GetRelSupplierCustomerResourseByRoleId(0);
            cusdata = cusdata.Where(x => x.relId == relids);
            var condata = await contactsService.GetAllContactsByLeadsId((int)cusdata.Select(x => x.leadId).FirstOrDefault());
            var resdata = await resourceService.GetResourceById((int)condata.FirstOrDefault().resourceId);
            ViewBag.mobile = cusdata.FirstOrDefault().mobile;
            ViewBag.name = resdata.resourceName;
            var allps = await homeCareService.GetAllPatientService();
            ViewBag.presentAddress = cusdata.FirstOrDefault().presentAddress;

            SalesInvoiceViewModel model = new SalesInvoiceViewModel
            {
                salesInvoiceMaster = invoicedata, //await salesInvoiceMasterService.GetSalesInvoiceMasterById(id),
                PatientRepresentatives = await homeCareService.GetAllPatientRepresentativeByPatientId(invoicedata.relSupplierCustomerResourse.LeadsId),
                PatientServices = allps.Where(x => x.leadsId == invoicedata.relSupplierCustomerResourse.LeadsId).ToList(),
                salesInvoiceDetails = await salesInvoiceDetailService.GetAllSalesInvoiceDetailByMasterId(id),
                company = await ledgerService.Company(3),
                salesTermsConditions = await salesInvoiceMasterService.GetTermsConditionsByMasterId(id),

                comments = CommentInfo,
                photoes = photoInfo,
                documents = docInfo,
            };
            ViewBag.InWord = AmountInWord.ConvertToWords(model.salesInvoiceMaster.NetTotal.ToString());

            return View(model);
        }
        [AllowAnonymous]
        public IActionResult RentInvoicePDFD(DateTime? fromDate, DateTime? toDate)
        {
            string userName = HttpContext.User.Identity.Name;
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/OtherSales/SalesInvoice/RentInvoicePDFList?fromDate=" + fromDate + "&toDate=" + toDate;

            string fileName;
            string status = myPDF.GeneratePDF(out fileName, url);

            // string status = myPDF.GeneratePDF(out fileName, url);

            FileName = fileName;
            if (status != "done")
            {
                return Content("<h1>" + status + "</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> RentInvoicePDFList(DateTime? fromDate, DateTime? toDate)
        {
            ViewBag.fromDate = fromDate;
            ViewBag.toDate = toDate;
            var detaildata = await salesInvoiceDetailService.GetAllSalesInvoicelistbydaterange(fromDate, toDate);
            SalesInvoiceViewModel model = new SalesInvoiceViewModel
            {

                salesInvoiceDetails = detaildata.Where(x => x.salesInvoiceMaster.invoiceNumber.Contains("RENT")),
                //  PatientRepresentatives = await homeCareService.GetAllPatientRepresentativeByPatientId(invoicedata.relSupplierCustomerResourse.LeadsId),
                company = await ledgerService.Company(3),
                relSupplierCustomerResourseViewModels = await customerService.GetRelSupplierCustomerResourseByRoleId(0)
                //comments = CommentInfo,
                //photoes = photoInfo,
                //documents = docInfo,
            };
            // ViewBag.InWord = AmountInWord.ConvertToWords(model.salesInvoiceMaster.NetTotal.ToString());
            // ViewBag.address = data.FirstOrDefault().maillingAddress;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> RentInvoicePDFListLoad(DateTime? fromDate, DateTime? toDate)
        {
            ViewBag.fromDate = fromDate;
            ViewBag.toDate = toDate;
            var detaildata = await salesInvoiceDetailService.GetAllSalesInvoicelistbydaterange(fromDate, toDate);
            SalesInvoiceViewModel model = new SalesInvoiceViewModel
            {

                salesInvoiceDetails = detaildata.Where(x => x.salesInvoiceMaster.invoiceNumber.Contains("RENT")),
                //  PatientRepresentatives = await homeCareService.GetAllPatientRepresentativeByPatientId(invoicedata.relSupplierCustomerResourse.LeadsId),
                company = await ledgerService.Company(3),
                relSupplierCustomerResourseViewModels = await customerService.GetRelSupplierCustomerResourseByRoleId(0)
                //comments = CommentInfo,
                //photoes = photoInfo,
                //documents = docInfo,
            };
            // ViewBag.InWord = AmountInWord.ConvertToWords(model.salesInvoiceMaster.NetTotal.ToString());
            // ViewBag.address = data.FirstOrDefault().maillingAddress;

            return Json(model);
        }
        [AllowAnonymous]
        public IActionResult SalesInvoicePDFD(DateTime? fromDate, DateTime? toDate)
        {
            string userName = HttpContext.User.Identity.Name;
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/OtherSales/SalesInvoice/SalesInvoicePDFList?fromDate=" + fromDate + "&toDate=" + toDate;

            string fileName;
            string status = myPDF.GeneratePDF(out fileName, url);

            // string status = myPDF.GeneratePDF(out fileName, url);

            FileName = fileName;
            if (status != "done")
            {
                return Content("<h1>" + status + "</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> SalesInvoicePDFList(DateTime? fromDate, DateTime? toDate)
        {
            ViewBag.fromDate = fromDate;
            ViewBag.toDate = toDate;
            var detaildata = await salesInvoiceDetailService.GetAllSalesInvoicelistbydaterange(fromDate, toDate);
            SalesInvoiceViewModel model = new SalesInvoiceViewModel
            {

                salesInvoiceDetails = detaildata.Where(x => x.salesInvoiceMaster.invoiceNumber.Contains("SALE")),
                //  PatientRepresentatives = await homeCareService.GetAllPatientRepresentativeByPatientId(invoicedata.relSupplierCustomerResourse.LeadsId),
                company = await ledgerService.Company(3),
                relSupplierCustomerResourseViewModels = await customerService.GetRelSupplierCustomerResourseByRoleId(0)
                //comments = CommentInfo,
                //photoes = photoInfo,
                //documents = docInfo,
            };
            // ViewBag.InWord = AmountInWord.ConvertToWords(model.salesInvoiceMaster.NetTotal.ToString());
            // ViewBag.address = data.FirstOrDefault().maillingAddress;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SalesInvoicePDFListLoad(DateTime? fromDate, DateTime? toDate, string userName)
        {

            ViewBag.fromDate = fromDate;
            ViewBag.toDate = toDate;
            var detaildata = await salesInvoiceDetailService.GetAllSalesInvoicelistBydaterangeAndUser(fromDate, toDate, userName);
            SalesInvoiceViewModel model = new SalesInvoiceViewModel
            {

                salesInvoiceDetails = detaildata.Where(x => x.salesInvoiceMaster.invoiceNumber.Contains("SALE")),
                //  PatientRepresentatives = await homeCareService.GetAllPatientRepresentativeByPatientId(invoicedata.relSupplierCustomerResourse.LeadsId),
                company = await ledgerService.Company(3),
                relSupplierCustomerResourseViewModels = await customerService.GetRelSupplierCustomerResourseByRoleId(0)
                //comments = CommentInfo,
                //photoes = photoInfo,
                //documents = docInfo,
            };
            // ViewBag.InWord = AmountInWord.ConvertToWords(model.salesInvoiceMaster.NetTotal.ToString());
            // ViewBag.address = data.FirstOrDefault().maillingAddress;

            return Json(model);
        }
        [AllowAnonymous]
        public IActionResult ServiceInvoicePDFD(DateTime? fromDate, DateTime? toDate)
        {
            string userName = HttpContext.User.Identity.Name;
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/OtherSales/SalesInvoice/ServiceInvoicePDFList?fromDate=" + fromDate + "&toDate=" + toDate;

            string fileName;
            string status = myPDF.GeneratePDF(out fileName, url);

            // string status = myPDF.GeneratePDF(out fileName, url);

            FileName = fileName;
            if (status != "done")
            {
                return Content("<h1>" + status + "</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ServiceInvoicePDFList(DateTime? fromDate, DateTime? toDate)
        {
            ViewBag.fromDate = fromDate;
            ViewBag.toDate = toDate;
            var detaildata = await salesInvoiceDetailService.GetAllSalesInvoicelistbydaterange(fromDate, toDate);
            SalesInvoiceViewModel model = new SalesInvoiceViewModel
            {

                salesInvoiceDetails = detaildata.Where(x => x.salesInvoiceMaster.invoiceNumber.Contains("Service")),
                //  PatientRepresentatives = await homeCareService.GetAllPatientRepresentativeByPatientId(invoicedata.relSupplierCustomerResourse.LeadsId),
                company = await ledgerService.Company(3),
                relSupplierCustomerResourseViewModels = await customerService.GetRelSupplierCustomerResourseByRoleId(0)

            };
            // ViewBag.InWord = AmountInWord.ConvertToWords(model.salesInvoiceMaster.NetTotal.ToString());
            // ViewBag.address = data.FirstOrDefault().maillingAddress;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ServiceInvoicePDFListLoad(DateTime? fromDate, DateTime? toDate)
        {
            ViewBag.fromDate = fromDate;
            ViewBag.toDate = toDate;
            var detaildata = await salesInvoiceDetailService.GetAllSalesInvoicelistbydaterange(fromDate, toDate);
            SalesInvoiceViewModel model = new SalesInvoiceViewModel
            {

                salesInvoiceDetails = detaildata.Where(x => x.salesInvoiceMaster.invoiceNumber.Contains("Service")),
                //  PatientRepresentatives = await homeCareService.GetAllPatientRepresentativeByPatientId(invoicedata.relSupplierCustomerResourse.LeadsId),
                company = await ledgerService.Company(3),
                relSupplierCustomerResourseViewModels = await customerService.GetRelSupplierCustomerResourseByRoleId(0)

            };
            // ViewBag.InWord = AmountInWord.ConvertToWords(model.salesInvoiceMaster.NetTotal.ToString());
            // ViewBag.address = data.FirstOrDefault().maillingAddress;

            return Json(model);
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> RentInvoicePDF(int id, string userName)
        {
            ViewBag.masterId = id;
            var CommentInfo = await attachmentCommentService.GetCommentByActionTypeId(id, "SalesInvoice", 4);
            if (CommentInfo == null)
            {
                CommentInfo = new List<Comment>();
            }
            var photoInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "SalesInvoice", "photo", 4);
            if (photoInfo == null)
            {
                photoInfo = new List<DocumentPhotoAttachment>();
            }
            var docInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "SalesInvoice", "Document", 4);
            if (docInfo == null)
            {
                docInfo = new List<DocumentPhotoAttachment>();
            }

            var invoicedata = await salesInvoiceMasterService.GetSalesInvoiceMasterById(id);
            var data = await addressesService.GetAddressesByLeadId((int)invoicedata.relSupplierCustomerResourse.LeadsId);
            int? relids = invoicedata.relSupplierCustomerResourseId;
            var cusdata = await customerService.GetRelSupplierCustomerResourseByRoleId(0);
            cusdata = cusdata.Where(x => x.relId == relids);
            ViewBag.mobile = cusdata.FirstOrDefault().mobile;
            SalesInvoiceViewModel model = new SalesInvoiceViewModel
            {
                salesInvoiceMaster = invoicedata,
                salesInvoiceDetails = await salesInvoiceDetailService.GetAllSalesInvoiceDetailByMasterId(id),
                //  PatientRepresentatives = await homeCareService.GetAllPatientRepresentativeByPatientId(invoicedata.relSupplierCustomerResourse.LeadsId),
                company = await ledgerService.Company(3),
                salesTermsConditions = await salesInvoiceMasterService.GetTermsConditionsByMasterId(id),
                comments = CommentInfo,
                photoes = photoInfo,
                documents = docInfo,
            };
            ViewBag.InWord = AmountInWord.ConvertToWords(model.salesInvoiceMaster.NetTotal.ToString());
            ViewBag.address = data.FirstOrDefault().maillingAddress;

            return View(model);
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> InvoicePDF(int id, string userName)
        {
            ViewBag.masterId = id;
            var CommentInfo = await attachmentCommentService.GetCommentByActionTypeId(id, "SalesInvoice", 4);
            if (CommentInfo == null)
            {
                CommentInfo = new List<Comment>();
            }
            var photoInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "SalesInvoice", "photo", 4);
            if (photoInfo == null)
            {
                photoInfo = new List<DocumentPhotoAttachment>();
            }
            var docInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "SalesInvoice", "Document", 4);
            if (docInfo == null)
            {
                docInfo = new List<DocumentPhotoAttachment>();
            }

            var invoicedata = await salesInvoiceMasterService.GetSalesInvoiceMasterById(id);
            int? relids = invoicedata.relSupplierCustomerResourseId;
            var cusdata = await customerService.GetRelSupplierCustomerResourseByRoleId(0);
            cusdata = cusdata.Where(x => x.relId == relids);
            ViewBag.mobile = cusdata.FirstOrDefault().mobile;
            SalesInvoiceViewModel model = new SalesInvoiceViewModel
            {
                salesInvoiceMaster = invoicedata,
                salesInvoiceDetails = await salesInvoiceDetailService.GetAllSalesInvoiceDetailByMasterId(id),
                PatientRepresentatives = await homeCareService.GetAllPatientRepresentativeByPatientId(invoicedata.relSupplierCustomerResourse.LeadsId),
                company = await ledgerService.Company(3),
                salesTermsConditions = await salesInvoiceMasterService.GetTermsConditionsByMasterId(id),
                comments = CommentInfo,
                photoes = photoInfo,
                documents = docInfo,
                companies = await iERPCompanyService.GetAllCompany(),
            };
            ViewBag.InWord = AmountInWord.ConvertToWords(model.salesInvoiceMaster.NetTotal.ToString());

            return View(model);
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ReturnInvoicePDF(int id, string userName)
        {
            ViewBag.masterId = id;
            //var companylist = await ledgerService.GetAllCompanyList(userName);

            SalesInvoiceViewModel model = new SalesInvoiceViewModel
            {
                salesReturnInvoiceMaster = await salesInvoiceMasterService.GetSalesReturnInvoiceMasterById(id),
                salesReturnInvoiceDetails = await salesInvoiceDetailService.GetAllSalesReturnInvoiceDetailByMasterId(id),
                companies = await iERPCompanyService.GetAllCompany(),

            };
            ViewBag.InWord = AmountInWord.ConvertToWords(model.salesReturnInvoiceMaster.totalAmount.ToString());

            return View(model);
        }
        [AllowAnonymous]
        public IActionResult RentInvoicePDFAction(int id)
        {
            string userName = HttpContext.User.Identity.Name;
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/OtherSales/SalesInvoice/RentInvoicePDF?id=" + id + "&userName=" + userName;

            string fileName;
            string status = myPDF.GeneratePDF(out fileName, url);

            // string status = myPDF.GeneratePDF(out fileName, url);

            FileName = fileName;
            if (status != "done")
            {
                return Content("<h1>" + status + "</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }
        [AllowAnonymous]
        public IActionResult PatInvoicePDFAction(int id)
        {
            string userName = HttpContext.User.Identity.Name;
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/OtherSales/SalesInvoice/PatInvoicePDF?id=" + id + "&userName=" + userName;

            string fileName;
            string status = myPDF.GeneratePDF(out fileName, url);

            // string status = myPDF.GeneratePDF(out fileName, url);

            FileName = fileName;
            if (status != "done")
            {
                return Content("<h1>" + status + "</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }
        [AllowAnonymous]
        public IActionResult InvoicePDFAction(int id)
        {
            string userName = HttpContext.User.Identity.Name;
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/OtherSales/SalesInvoice/InvoicePDF?id=" + id + "&userName=" + userName;

            string fileName;
            string status = myPDF.GeneratePDF(out fileName, url);

            // string status = myPDF.GeneratePDF(out fileName, url);

            FileName = fileName;
            if (status != "done")
            {
                return Content("<h1>" + status + "</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }
        [AllowAnonymous]
        public IActionResult ReturnInvoicePDFAction(int id)
        {
            string userName = HttpContext.User.Identity.Name;
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/OtherSales/SalesInvoice/ReturnInvoicePDF?id=" + id + "&userName=" + userName;

            string fileName;
            string status = myPDF.GeneratePDF(out fileName, url);

            // string status = myPDF.GeneratePDF(out fileName, url);

            FileName = fileName;
            if (status != "done")
            {
                return Content("<h1>" + status + "</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }

        #endregion

        #region Others

        [Route("/api/global/getInvoiceNo/{invoiceDate}")]
        public async Task<JsonResult> getInvoiceNo(string invoiceDate)
        {
            var sale = await salesInvoiceMasterService.GetAllSalesInvoiceMaster();
            int Cpurchase = 0;
            Cpurchase = sale.Where(x => Convert.ToDateTime(x.invoiceDate).ToString("yyyyMMdd") == Convert.ToDateTime(invoiceDate).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(invoiceDate).ToString("yyyy-MM-dd");
            string issueNo = "Sale" + '-' + idate + '-' + (Cpurchase + 1);
            return Json(issueNo);
        }


        [HttpPost]
        public async Task<IActionResult> SavePhoto([FromForm] DocumentAttachmentViewModel model, IFormFile formFile)
        {
            try
            {
                if (formFile != null)
                {
                    string userName = User.Identity.Name;
                    string documentType = "photo";
                    var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                    var fileName = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName;
                    string fileType = Path.GetExtension(fileName);
                    fileName = fileName.Contains("\\")
                        ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                        : fileName.Trim('"');

                    string NewFileName = model.actionTypeId + "_" + documentType + "_" + fileName;
                    string fileLocation = "/Upload/Photo/" + NewFileName;
                    var fullFilePath = Path.Combine(filesPath, NewFileName);

                    using (var stream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    DocumentPhotoAttachment data = new DocumentPhotoAttachment
                    {
                        Id = Convert.ToInt32(model.documentId),
                        actionType = model.actionType,
                        actionTypeId = model.actionTypeId,
                        subject = model.subject,
                        fileName = NewFileName,
                        filePath = fileLocation,
                        fileType = fileType,
                        description = model.description,
                        documentType = documentType,
                        createdBy = userName
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);
                }

                return RedirectToAction("InvoiceDetails", "SalesInvoice", new { id = model.actionTypeId, Area = "OtherSales" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> RentSavePhoto([FromForm] DocumentAttachmentViewModel model, IFormFile formFile)
        {
            try
            {
                if (formFile != null)
                {
                    string userName = User.Identity.Name;
                    string documentType = "photo";
                    var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                    var fileName = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName;
                    string fileType = Path.GetExtension(fileName);
                    fileName = fileName.Contains("\\")
                        ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                        : fileName.Trim('"');

                    string NewFileName = model.actionTypeId + "_" + documentType + "_" + fileName;
                    string fileLocation = "/Upload/Photo/" + NewFileName;
                    var fullFilePath = Path.Combine(filesPath, NewFileName);

                    using (var stream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    DocumentPhotoAttachment data = new DocumentPhotoAttachment
                    {
                        Id = Convert.ToInt32(model.documentId),
                        actionType = model.actionType,
                        actionTypeId = model.actionTypeId,
                        subject = model.subject,
                        fileName = NewFileName,
                        filePath = fileLocation,
                        fileType = fileType,
                        description = model.description,
                        documentType = documentType,
                        createdBy = userName
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);
                }

                return RedirectToAction("RentInvoiceDetails", "SalesInvoice", new { id = model.actionTypeId, Area = "OtherSales" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> PatSavePhoto([FromForm] DocumentAttachmentViewModel model, IFormFile formFile)
        {
            try
            {
                if (formFile != null)
                {
                    string userName = User.Identity.Name;
                    string documentType = "photo";
                    var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                    var fileName = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName;
                    string fileType = Path.GetExtension(fileName);
                    fileName = fileName.Contains("\\")
                        ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                        : fileName.Trim('"');

                    string NewFileName = model.actionTypeId + "_" + documentType + "_" + fileName;
                    string fileLocation = "/Upload/Photo/" + NewFileName;
                    var fullFilePath = Path.Combine(filesPath, NewFileName);

                    using (var stream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    DocumentPhotoAttachment data = new DocumentPhotoAttachment
                    {
                        Id = Convert.ToInt32(model.documentId),
                        actionType = model.actionType,
                        actionTypeId = model.actionTypeId,
                        subject = model.subject,
                        fileName = NewFileName,
                        filePath = fileLocation,
                        fileType = fileType,
                        description = model.description,
                        documentType = documentType,
                        createdBy = userName
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);
                }

                return RedirectToAction("PatInvoiceDetails", "SalesInvoice", new { id = model.actionTypeId, Area = "OtherSales" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> SavePhotoReturn([FromForm] DocumentAttachmentViewModel model, IFormFile formFile)
        {
            try
            {
                if (formFile != null)
                {
                    string userName = User.Identity.Name;
                    string documentType = "photo";
                    var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                    var fileName = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName;
                    string fileType = Path.GetExtension(fileName);
                    fileName = fileName.Contains("\\")
                        ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                        : fileName.Trim('"');

                    string NewFileName = model.actionTypeId + "_" + documentType + "_" + fileName;
                    string fileLocation = "/Upload/Photo/" + NewFileName;
                    var fullFilePath = Path.Combine(filesPath, NewFileName);

                    using (var stream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    DocumentPhotoAttachment data = new DocumentPhotoAttachment
                    {
                        Id = Convert.ToInt32(model.documentId),
                        actionType = model.actionType,
                        actionTypeId = model.actionTypeId,
                        subject = model.subject,
                        fileName = NewFileName,
                        filePath = fileLocation,
                        fileType = fileType,
                        description = model.description,
                        documentType = documentType,
                        createdBy = userName
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);
                }

                return RedirectToAction("ReturnInvoiceDetails", "SalesInvoice", new { id = model.actionTypeId, Area = "OtherSales" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveDoc([FromForm] DocumentAttachmentViewModel model, IFormFile formFile)
        {
            try
            {
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

                    string NewFileName = model.actionTypeId + "_" + documentType + "_" + fileName;
                    string fileLocation = "/Upload/Photo/" + NewFileName;
                    var fullFilePath = Path.Combine(filesPath, NewFileName);

                    using (var stream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    DocumentPhotoAttachment data = new DocumentPhotoAttachment
                    {
                        Id = Convert.ToInt32(model.documentId),
                        actionType = model.actionType,
                        actionTypeId = model.actionTypeId,
                        subject = model.subject,
                        fileName = NewFileName,
                        filePath = fileLocation,
                        fileType = fileType,
                        description = model.description,
                        documentType = documentType,
                        createdBy = userName
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);
                }

                return RedirectToAction("InvoiceDetails", "SalesInvoice", new { id = model.actionTypeId, Area = "OtherSales" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> RentSaveDoc([FromForm] DocumentAttachmentViewModel model, IFormFile formFile)
        {
            try
            {
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

                    string NewFileName = model.actionTypeId + "_" + documentType + "_" + fileName;
                    string fileLocation = "/Upload/Photo/" + NewFileName;
                    var fullFilePath = Path.Combine(filesPath, NewFileName);

                    using (var stream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    DocumentPhotoAttachment data = new DocumentPhotoAttachment
                    {
                        Id = Convert.ToInt32(model.documentId),
                        actionType = model.actionType,
                        actionTypeId = model.actionTypeId,
                        subject = model.subject,
                        fileName = NewFileName,
                        filePath = fileLocation,
                        fileType = fileType,
                        description = model.description,
                        documentType = documentType,
                        createdBy = userName
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);
                }

                return RedirectToAction("RentInvoiceDetails", "SalesInvoice", new { id = model.actionTypeId, Area = "OtherSales" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> PatSaveDoc([FromForm] DocumentAttachmentViewModel model, IFormFile formFile)
        {
            try
            {
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

                    string NewFileName = model.actionTypeId + "_" + documentType + "_" + fileName;
                    string fileLocation = "/Upload/Photo/" + NewFileName;
                    var fullFilePath = Path.Combine(filesPath, NewFileName);

                    using (var stream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    DocumentPhotoAttachment data = new DocumentPhotoAttachment
                    {
                        Id = Convert.ToInt32(model.documentId),
                        actionType = model.actionType,
                        actionTypeId = model.actionTypeId,
                        subject = model.subject,
                        fileName = NewFileName,
                        filePath = fileLocation,
                        fileType = fileType,
                        description = model.description,
                        documentType = documentType,
                        createdBy = userName
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);
                }

                return RedirectToAction("PatInvoiceDetails", "SalesInvoice", new { id = model.actionTypeId, Area = "OtherSales" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> SaveDocReturn([FromForm] DocumentAttachmentViewModel model, IFormFile formFile)
        {
            try
            {
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

                    string NewFileName = model.actionTypeId + "_" + documentType + "_" + fileName;
                    string fileLocation = "/Upload/Photo/" + NewFileName;
                    var fullFilePath = Path.Combine(filesPath, NewFileName);

                    using (var stream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    DocumentPhotoAttachment data = new DocumentPhotoAttachment
                    {
                        Id = Convert.ToInt32(model.documentId),
                        actionType = model.actionType,
                        actionTypeId = model.actionTypeId,
                        subject = model.subject,
                        fileName = NewFileName,
                        filePath = fileLocation,
                        fileType = fileType,
                        description = model.description,
                        documentType = documentType,
                        createdBy = userName
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);
                }

                return RedirectToAction("ReturnInvoiceDetails", "SalesInvoice", new { id = model.actionTypeId, Area = "OtherSales" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> SaveComment([FromForm] CommentsViewModel model)
        {
            try
            {
                Comment entityComment = new Comment
                {
                    Id = Convert.ToInt32(model.commentsId),
                    actionType = "SalesInvoice",
                    actionTypeId = model.actionTypeId,
                    title = model.title,
                    comment = model.comment,
                    createdBy = HttpContext.User.Identity.Name,
                };
                int commentsId = await attachmentCommentService.SaveComment(entityComment);

                return RedirectToAction("InvoiceDetails", "SalesInvoice", new { id = model.actionTypeId, Area = "OtherSales" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<ActionResult> RentSaveComment([FromForm] CommentsViewModel model)
        {
            try
            {
                Comment entityComment = new Comment
                {
                    Id = Convert.ToInt32(model.commentsId),
                    actionType = "SalesInvoice",
                    actionTypeId = model.actionTypeId,
                    title = model.title,
                    comment = model.comment,
                    createdBy = HttpContext.User.Identity.Name,
                };
                int commentsId = await attachmentCommentService.SaveComment(entityComment);

                return RedirectToAction("RentInvoiceDetails", "SalesInvoice", new { id = model.actionTypeId, Area = "OtherSales" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<ActionResult> PatSaveComment([FromForm] CommentsViewModel model)
        {
            try
            {
                Comment entityComment = new Comment
                {
                    Id = Convert.ToInt32(model.commentsId),
                    actionType = "SalesInvoice",
                    actionTypeId = model.actionTypeId,
                    title = model.title,
                    comment = model.comment,
                    createdBy = HttpContext.User.Identity.Name,
                };
                int commentsId = await attachmentCommentService.SaveComment(entityComment);

                return RedirectToAction("PatInvoiceDetails", "SalesInvoice", new { id = model.actionTypeId, Area = "OtherSales" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<ActionResult> SaveCommentReturn([FromForm] CommentsViewModel model)
        {
            try
            {
                Comment entityComment = new Comment
                {
                    Id = Convert.ToInt32(model.commentsId),
                    actionType = "SalesReturnInvoice",
                    actionTypeId = model.actionTypeId,
                    title = model.title,
                    comment = model.comment,
                    createdBy = HttpContext.User.Identity.Name,
                };
                int commentsId = await attachmentCommentService.SaveComment(entityComment);

                return RedirectToAction("ReturnInvoiceDetails", "SalesInvoice", new { id = model.actionTypeId, Area = "OtherSales" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteComments(int Id, int actionId)
        {
            await attachmentCommentService.DeleteCommentById(Id);
            return RedirectToAction("InvoiceDetails", "SalesInvoice", new { id = actionId, Area = "OtherSales" });
        }
        [HttpGet]
        public async Task<IActionResult> RentDeleteComments(int Id, int actionId)
        {
            await attachmentCommentService.DeleteCommentById(Id);
            return RedirectToAction("RentInvoiceDetails", "SalesInvoice", new { id = actionId, Area = "OtherSales" });
        }
        [HttpGet]
        public async Task<IActionResult> PatDeleteComments(int Id, int actionId)
        {
            await attachmentCommentService.DeleteCommentById(Id);
            return RedirectToAction("PatInvoiceDetails", "SalesInvoice", new { id = actionId, Area = "OtherSales" });
        }
        [HttpGet]
        public async Task<IActionResult> DeleteCommentsReturn(int Id, int actionId)
        {
            await attachmentCommentService.DeleteCommentById(Id);
            return RedirectToAction("ReturnInvoiceDetails", "SalesInvoice", new { id = actionId, Area = "OtherSales" });
        }

        [HttpGet]
        public async Task<IActionResult> DeletePhoto(int actionId, int photoId)
        {
            var data = await attachmentCommentService.GetDocumentAttachmentById(photoId);
            var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";

            var fullFilePath = Path.Combine(filesPath, data.fileName);
            if (System.IO.File.Exists(fullFilePath))
                System.IO.File.Delete(fullFilePath);
            await attachmentCommentService.DeleteDocumentAttachmentById(photoId);
            return RedirectToAction("InvoiceDetails", "SalesInvoice", new { id = actionId, Area = "OtherSales" });
        }
        [HttpGet]
        public async Task<IActionResult> RentDeletePhoto(int actionId, int photoId)
        {
            var data = await attachmentCommentService.GetDocumentAttachmentById(photoId);
            var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";

            var fullFilePath = Path.Combine(filesPath, data.fileName);
            if (System.IO.File.Exists(fullFilePath))
                System.IO.File.Delete(fullFilePath);
            await attachmentCommentService.DeleteDocumentAttachmentById(photoId);
            return RedirectToAction("RentInvoiceDetails", "SalesInvoice", new { id = actionId, Area = "OtherSales" });
        }
        [HttpGet]
        public async Task<IActionResult> PatDeletePhoto(int actionId, int photoId)
        {
            var data = await attachmentCommentService.GetDocumentAttachmentById(photoId);
            var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";

            var fullFilePath = Path.Combine(filesPath, data.fileName);
            if (System.IO.File.Exists(fullFilePath))
                System.IO.File.Delete(fullFilePath);
            await attachmentCommentService.DeleteDocumentAttachmentById(photoId);
            return RedirectToAction("PatInvoiceDetails", "SalesInvoice", new { id = actionId, Area = "OtherSales" });
        }
        [HttpGet]
        public async Task<IActionResult> DeletePhotoReturn(int actionId, int photoId)
        {
            var data = await attachmentCommentService.GetDocumentAttachmentById(photoId);
            var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";

            var fullFilePath = Path.Combine(filesPath, data.fileName);
            if (System.IO.File.Exists(fullFilePath))
                System.IO.File.Delete(fullFilePath);
            await attachmentCommentService.DeleteDocumentAttachmentById(photoId);
            return RedirectToAction("ReturnInvoiceDetails", "SalesInvoice", new { id = actionId, Area = "OtherSales" });
        }

        #endregion

        #region API Section

        [Route("global/api/getAllORelSupplierCustomerResoursePatByRoleId/")]
        [HttpGet]
        public async Task<IActionResult> GetAllRelSupplierCustomerResoursePatByRoleId()
        {
            var roledata = await customerService.GetAllRoleType();
            var resdata = await homeCareService.GetAllPatientService();
            List<int?> resdataids = resdata.Select(x => x.leadsId).ToList();
            var data = await customerService.GetRelSupplierCustomerResourseByRoleId(roledata.Where(x => x.name == "Leads").Select(x => x.Id).FirstOrDefault());
            return Json(data.Where(x => resdataids.Contains(x.leadId)));
            //return Json(await customerService.GetAllRelSupplierCustomerResourseByRoleId(roledata.Where(x => x.name == "Leads").Select(x => x.Id).FirstOrDefault()));
        }


        [Route("global/api/getAllOSalesInvoiceDetailByMasterId/{masterId}")]
        [HttpGet]
        public async Task<IActionResult> GetAllSalesInvoiceDetailByMasterId(int masterId)
        {
            return Json(await salesInvoiceDetailService.GetAllSalesInvoiceDetailByMasterId(masterId));
        }

        [Route("global/api/GetAllSalesInvoiceMasterByrelSuplierCustomerId/{Id}")]
        [HttpGet]
        public async Task<IActionResult> GetAllSalesInvoiceMasterByrelSuplierCustomerId(int Id)
        {
            return Json(await salesInvoiceMasterService.GetAllSalesInvoiceMasterByrelSuplierCustomerId(Id));
        }

        [Route("global/api/GetOnlySalesInvoiceNoByCustomerId/{Id}")]
        [HttpGet]
        public async Task<IActionResult> GetOnlySalesInvoiceNoByCustomerId(int Id)
        {
            var data = await salesInvoiceMasterService.GetAllSalesInvoiceMasterByrelSuplierCustomerId(Id);
            return Json(data.Where(a => a.invoiceNumber.Contains("SALE")));
        }

        [Route("global/api/getAllSalesInvoiceMaster/")]
        [HttpGet]
        public async Task<IActionResult> GetAllSalesInvoiceMaster()
        {
            return Json(await salesInvoiceMasterService.GetAllSalesInvoiceMaster());
        }
        [Route("global/api/getAllSalesInvoiceMasterD/")]
        [HttpGet]
        public async Task<IActionResult> GetAllSalesInvoiceMasterD()
        {
            var data = await salesInvoiceMasterService.GetDueSalesInvoice(0);
            return Json(data.Where(x => x.salesInvoiceMaster.invoiceNumber.Contains("RENT")));
        }
        [Route("global/api/getAllSalesInvoiceMasterS/")]
        [HttpGet]
        public async Task<IActionResult> GetAllSalesInvoiceMasterS()
        {
            var data = await salesInvoiceMasterService.GetDueSalesInvoice(0);
            return Json(data.Where(x => x.salesInvoiceMaster.invoiceNumber.Contains("Sales")));
        }
        [Route("global/api/getAllSalesInvoiceMasterH/")]
        [HttpGet]
        public async Task<IActionResult> GetAllSalesInvoiceMasterH()
        {
            var data = await salesInvoiceMasterService.GetDueSalesInvoice(0);
            return Json(data.Where(x => x.salesInvoiceMaster.invoiceNumber.Contains("Service")));
        }

        [Route("global/api/getAllRentInvoiceMaster/{Id}")]
        [HttpGet]
        public async Task<IActionResult> getAllRentInvoiceMaster(int Id)
        {
            var data = await rentInvoiceMasterService.GetSalesInvoiceMasterByLeadId(Id);
            return Json(data);
        }

        //[Route("global/api/getAllRentInvoiceItem/{Id}")]
        //[HttpGet]
        //public async Task<IActionResult> getAllRentInvoiceItem(int Id)
        //{
        //    var data = await rentInvoiceDetailService.GetAllSalesInvoiceDetailByMasterId(Id);
        //    List<int?> itemid = data.Select(x => x.itemSpecication.itemId).ToList();
        //    var item = await itemPriceFixingService.GetAllActiveItemFromItemPrice();



        //    return Json(item.Where(x => itemid.Contains(x.Id)));
        //    //return Json(item.Where(x=>itemid.Contains(x.Id)).FirstOrDefault());
        //}

        [Route("global/api/getAllRentInvoiceItem/{Id}")]
        [HttpGet]
        public async Task<IActionResult> getAllRentInvoiceItem(int Id)
        {
            var data = await rentInvoiceDetailService.GetAllSalesInvoiceDetailByAgreementId(Id);
            return Json(data);
            //List<int?> itemid = data.Select(x => x.itemSpecication.itemId).ToList();
            //var item = await itemPriceFixingService.GetAllActiveItemFromItemPrice();

            //return Json(item.Where(x => itemid.Contains(x.Id)));
            //return Json(item.Where(x=>itemid.Contains(x.Id)).FirstOrDefault());
        }

        [Route("global/api/GetItemSpecByRentIdAndItemId/{Id}/{itemId}")]
        [HttpGet]
        public async Task<IActionResult> GetItemSpecByRentIdAndItemId(int Id, int itemId)
        {
            var data = await rentInvoiceDetailService.GetAllSalesInvoiceDetailByAgreementId(Id);
            return Json(data.Where(a => a.itemId == itemId));
        }

        [Route("global/api/getAllRentInvoiceDetails/{Id}/{SpecId}")]
        [HttpGet]
        public async Task<IActionResult> getAllRentInvoiceDetails(int Id, int specid)
        {
            var data = await rentInvoiceDetailService.GetAllSalesInvoiceDetailByMasterId(Id);
            var datax = data.Where(x => x.itemSpecicationId == specid).Distinct().FirstOrDefault();
            return Json(datax);
        }

        [Route("global/api/GetRentInvoiceDetailsByItemId/{Id}/{itemid}")]
        [HttpGet]
        public async Task<IActionResult> GetRentInvoiceDetailsByItemId(int Id, int itemid)
        {
            var data = await rentInvoiceDetailService.GetAllSalesInvoiceDetailByMasterId(Id);
            var datax = data.Where(x => x.itemSpecication.itemId == itemid).FirstOrDefault();
            return Json(datax);
        }

        [Route("global/api/getAllPatInvoiceItem/{Id}")]
        [HttpGet]
        public async Task<IActionResult> getAllPatInvoiceItem(int Id)
        {
            var data = await rentInvoiceDetailService.GetAllSalesInvoiceDetailPatientByMasterId(Id);
            List<int?> itemid = data.Select(x => x.itemSpecification.itemId).Distinct().ToList();
            var item = await itemPriceFixingService.GetAllActiveItemFromItemPrice();
            return Json(item.Where(x => itemid.Contains(x.Id)));
        }

        [Route("global/api/GetPatientInvoiceItemByLeadsId/{leadId}")]
        [HttpGet]
        public async Task<IActionResult> GetPatientInvoiceItemByLeadsId(int leadId)
        {
            var data = await rentInvoiceDetailService.GetPatientInvoiceItemByLeadsId(leadId);
            return Json(data);
        }

        [Route("global/api/GetPatientInvoiceItemSpecByItemLeadId/{Id}/{itemId}")]
        [HttpGet]
        public async Task<IActionResult> GetPatientInvoiceItemSpecByItemLeadId(int Id, int itemId)
        {
            var data = await rentInvoiceDetailService.GetPatientInvoiceItemByLeadsId(Id);
            return Json(data.Where(a => a.itemId == itemId));
        }

        [Route("global/api/getAllRentInvoiceItemDetails/{Id}/{masterId}")]
        [HttpGet]
        public async Task<IActionResult> getAllRentInvoiceItemDetails(int Id, int masterId)
        {
            var data = await rentInvoiceDetailService.GetAllSalesInvoiceDetailByMasterItemId(Id, masterId);

            return Json(data);
        }
        [Route("global/api/getAllPatInvoiceItemDetails/{Id}/{masterId}")]
        [HttpGet]
        public async Task<IActionResult> getAllParInvoiceItemDetails(int Id, int masterId)
        {
            var data = await rentInvoiceDetailService.GetAllSalesInvoiceDetailByMasterpatItemId(Id, masterId);

            return Json(data);
        }
        [Route("global/api/getAllRentRelSupplierCustomerResourseByRoleId/")]
        [HttpGet]
        public async Task<IActionResult> getAllRentRelSupplierCustomerResourseByRoleId()
        {
            var roledata = await customerService.GetAllRoleType();
            var rentleads = await rentInvoiceMasterService.GetAllSalesInvoiceMaster();
            List<int?> relids = rentleads.Select(x => x.relSupplierCustomerResourseId).ToList();
            //var data = await customerService.GetAllRelSupplierCustomerResourseByRoleId(roledata.Where(x => x.name == "Leads").Select(x => x.Id).FirstOrDefault());
            var data = await customerService.GetAllRelSupplierCustomerResourseByRoleId(0);
            //return Json(data.Where(x=>relids.Contains(x.Id)));
            var contacts = await contactsService.GetAllContacts();
            contacts = contacts.Where(x => data.Select(y => y.LeadsId).ToList().Contains(x.leadsId));
            SalesInvoiceViewModel model = new SalesInvoiceViewModel
            {
                relSupplierCustomerResourses = data,
                Contacts = contacts

            };
            return Json(model);
        }

        [Route("global/api/getAllPInvoiceLIst/{Id}/{address}/{date}")]
        [HttpGet]
        public async Task<IActionResult> getAllPInvoiceLIst(int? Id, string address, DateTime? date)
        {
            var data = await salesInvoiceMasterService.GetAllSalesInvoiceMaster();
            if (Id != null)
            {
                data = data.Where(x => x.relSupplierCustomerResourseId == Id && x.invoiceNumber.Contains("RENT")).ToList();
            }
            if (address != "NoData")
            {
                data = data.Where(x => x.shippingAddress == address && x.invoiceNumber.Contains("RENT")).ToList();
            }
            if (date != null)
            {
                data = data.Where(x => x.invoiceDate == date && x.invoiceNumber.Contains("RENT")).ToList();
            }

            var dataaddress = await AddressService.GetAddress();
            List<int?> relids = data.Select(x => x.relSupplierCustomerResourse.resourceId).ToList();
            List<int?> leadsid = data.Select(x => x.relSupplierCustomerResourse.LeadsId).ToList();
            dataaddress = dataaddress.Where(x => leadsid.Contains(x.leadsId));
            var contacts = await contactsService.GetAllContacts();
            contacts = contacts.Where(x => relids.Contains(x.resourceId));
            var resources = await resourceService.GetAllResource();
            resources = resources.Where(x => relids.Contains(x.Id));

            SalesInvoiceViewModel model = new SalesInvoiceViewModel
            {
                salesInvoiceMasters = data, //await salesInvoiceMasterService.GetAllSalesInvoiceMaster(),
                GetAddresses = dataaddress,
                Contacts = contacts,
                Resources = resources
            };
            // var data = await rentInvoiceDetailService.GetAllSalesInvoiceDetailByMasterpatItemId(Id, masterId);

            return Json(model);
        }
        [Route("global/api/getAllPInvoiceLIstC/{fromdate}/{toDate}")]
        [HttpGet]
        public async Task<IActionResult> getAllPInvoiceLIstC(DateTime? fromdate, DateTime? toDate)
        {
            var data = await salesInvoiceMasterService.GetAllSalesInvoiceMaster();
            data = data.Where(x => x.invoiceDate >= fromdate && x.invoiceDate <= toDate && x.invoiceNumber.Contains("RENT")).ToList();
            List<int?> relids = data.Select(x => x.relSupplierCustomerResourseId).ToList();
            var cusdata = await customerService.GetRelSupplierCustomerResourseByRoleId(0);
            cusdata = cusdata.Where(x => relids.Contains(x.relId));

            return Json(cusdata);
        }
        [Route("global/api/getAllPInvoiceLIstCS/{fromdate}/{toDate}")]
        [HttpGet]
        public async Task<IActionResult> getAllPInvoiceLIstCS(DateTime? fromdate, DateTime? toDate)
        {
            var data = await salesInvoiceMasterService.GetAllSalesInvoiceMaster();
            data = data.Where(x => x.invoiceDate >= fromdate && x.invoiceDate <= toDate && x.invoiceNumber.Contains("Sales")).ToList();
            List<int?> relids = data.Select(x => x.relSupplierCustomerResourseId).ToList();
            var cusdata = await customerService.GetRelSupplierCustomerResourseByRoleId(0);
            cusdata = cusdata.Where(x => relids.Contains(x.relId));

            return Json(cusdata);
        }
        [Route("global/api/getAllPInvoiceLIstCSR/{fromdate}/{toDate}")]
        [HttpGet]
        public async Task<IActionResult> getAllPInvoiceLIstCSR(DateTime? fromdate, DateTime? toDate)
        {
            var data = await salesInvoiceMasterService.GetAllSalesInvoiceMaster();
            data = data.Where(x => x.invoiceDate >= fromdate && x.invoiceDate <= toDate && x.invoiceNumber.Contains("Service")).ToList();
            List<int?> relids = data.Select(x => x.relSupplierCustomerResourseId).ToList();
            var cusdata = await customerService.GetRelSupplierCustomerResourseByRoleId(0);
            cusdata = cusdata.Where(x => relids.Contains(x.relId));

            return Json(cusdata);
        }
        [Route("global/api/getAllPInvoiceLIstS/{Id}/{address}/{date}")]
        [HttpGet]
        public async Task<IActionResult> getAllPInvoiceLIstS(int? Id, string address, DateTime? date)
        {
            var data = await salesInvoiceMasterService.GetAllSalesInvoiceMaster();
            if (Id != null)
            {
                data = data.Where(x => x.relSupplierCustomerResourseId == Id && x.invoiceNumber.Contains("SALE")).ToList();
            }
            if (address != "NoData")
            {
                data = data.Where(x => x.shippingAddress == address && x.invoiceNumber.Contains("SALE")).ToList();
            }
            if (date != null)
            {
                data = data.Where(x => x.invoiceDate == date && x.invoiceNumber.Contains("SALE")).ToList();
            }
            // var data = await rentInvoiceDetailService.GetAllSalesInvoiceDetailByMasterpatItemId(Id, masterId);
            data = data.Where(x => x.invoiceNumber.Contains("SALE")).ToList();
            var dataaddress = await AddressService.GetAddress();
            List<int?> relids = data.Select(x => x.relSupplierCustomerResourse.resourceId).ToList();
            List<int?> leadsid = data.Select(x => x.relSupplierCustomerResourse.LeadsId).ToList();
            dataaddress = dataaddress.Where(x => leadsid.Contains(x.leadsId));
            var contacts = await contactsService.GetAllContacts();
            contacts = contacts.Where(x => leadsid.Contains(x.leadsId));
            var resources = await resourceService.GetAllResource();
            resources = resources.Where(x => relids.Contains(x.Id));

            SalesInvoiceViewModel model = new SalesInvoiceViewModel
            {
                salesInvoiceMasters = data, //await salesInvoiceMasterService.GetAllSalesInvoiceMaster(),
                GetAddresses = dataaddress,
                Contacts = contacts,
                Resources = resources
            };
            // var data = await rentInvoiceDetailService.GetAllSalesInvoiceDetailByMasterpatItemId(Id, masterId);

            return Json(model);
        }
        [Route("global/api/getAllRenttermsconditionrs/{masterId}")]
        [HttpGet]
        public async Task<IActionResult> getAllRenttermsconditionrs(int masterId)
        {
            return Json(await salesInvoiceMasterService.GetTermsConditionsByMasterId(masterId));
        }
        [Route("global/api/getAllPInvoiceLIstP/{Id}/{address}/{date}")]
        [HttpGet]
        public async Task<IActionResult> getAllPInvoiceLIstP(int? Id, string address, DateTime? date)
        {
            var data = await salesInvoiceMasterService.GetAllSalesInvoiceMaster();
            if (Id != null)
            {
                data = data.Where(x => x.relSupplierCustomerResourseId == Id && x.invoiceNumber.Contains("Service")).ToList();
            }
            if (address != "NoData")
            {
                data = data.Where(x => x.shippingAddress == address && x.invoiceNumber.Contains("Service")).ToList();
            }
            if (date != null)
            {
                data = data.Where(x => x.invoiceDate == date && x.invoiceNumber.Contains("Service")).ToList();
            }
            // var data = await rentInvoiceDetailService.GetAllSalesInvoiceDetailByMasterpatItemId(Id, masterId);

            data = data.Where(x => x.invoiceNumber.Contains("Service")).ToList();
            var dataaddress = await AddressService.GetAddress();
            List<int?> relids = data.Select(x => x.relSupplierCustomerResourse.resourceId).ToList();
            List<int?> leadsid = data.Select(x => x.relSupplierCustomerResourse.LeadsId).ToList();
            dataaddress = dataaddress.Where(x => leadsid.Contains(x.leadsId));
            var contacts = await contactsService.GetAllContacts();
            contacts = contacts.Where(x => leadsid.Contains(x.leadsId));
            var resources = await resourceService.GetAllResource();
            resources = resources.Where(x => relids.Contains(x.Id));

            SalesInvoiceViewModel model = new SalesInvoiceViewModel
            {
                salesInvoiceMasters = data, //await salesInvoiceMasterService.GetAllSalesInvoiceMaster(),
                GetAddresses = dataaddress,
                Contacts = contacts,
                Resources = resources
            };
            // var data = await rentInvoiceDetailService.GetAllSalesInvoiceDetailByMasterpatItemId(Id, masterId);

            return Json(model);
        }
        #endregion

    }
}