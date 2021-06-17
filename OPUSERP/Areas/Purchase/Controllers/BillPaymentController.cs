using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Purchase.Models;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.Purchase.Data.Entity;
using OPUSERP.Purchase.Service.Interfaces;
using OPUSERP.Purchase.Services.Interfaces;
using OPUSERP.SCM.Services.PurchaseOrder.Interface;
using OPUSERP.SCM.Services.Supplier.Interface;

namespace OPUSERP.Areas.Purchase.Controllers
{
    [Authorize]
    [Area("Purchase")]
    public class BillPaymentController : Controller
    {
        private readonly IPurchaseService purchaseService;
        private readonly IPurchaseOrderService purchaseOrderService;
        private readonly IBillPaymentService billPaymentService;
        private readonly IAttachmentCommentService attachmentCommentService;
        private readonly IERPCompanyService iERPCompanyService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly string rootPath;
        private readonly MyPDF myPDF;
        private readonly IOrganizationService organizationService;
        private readonly ISupplierItemInfoService supplierItemInfoService;

        public string FileName;
        public BillPaymentController(IHostingEnvironment hostingEnvironment, IOrganizationService organizationService, ISupplierItemInfoService supplierItemInfoService, IAttachmentCommentService attachmentCommentService, IConverter converter, IPurchaseService purchaseService, IBillPaymentService billPaymentService, IPurchaseOrderService purchaseOrderService, IERPCompanyService iERPCompanyService)
        {

            this._hostingEnvironment = hostingEnvironment;
            this.attachmentCommentService = attachmentCommentService;
            this.purchaseService = purchaseService;
            this.organizationService = organizationService;
            this.supplierItemInfoService = supplierItemInfoService;
            this.billPaymentService = billPaymentService;
            this.purchaseOrderService = purchaseOrderService;
            this.iERPCompanyService = iERPCompanyService;

            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }


        public async Task<IActionResult> Index()
        {
            BillPaymentViewModel model = new BillPaymentViewModel
            {
                relSupplierCustomerResourses = await billPaymentService.GetDueSupplierList(),
                paymentModes = await purchaseOrderService.GetPaymentMode(),
                billPaymentMasters = await billPaymentService.GetBillPaymentMaster()
            };
            return View(model);
        }


        public async Task<IActionResult> BillReceive()
        {
            var purchase = await billPaymentService.GetBillReceiveInformation();
            int Cpurchase = 0;
            Cpurchase = purchase.Where(x => Convert.ToDateTime(x.billPaymentDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "BillRCV-No" + '-' + idate + '-' + (Cpurchase + 1);
            ViewBag.issueNo = issueNo;
            BillPaymentViewModel model = new BillPaymentViewModel
            {
                relSupplierCustomerResourses = await billPaymentService.GetDueSupplierList(),
                paymentModes = await purchaseOrderService.GetPaymentMode(),
                billPaymentMasters = await billPaymentService.GetBillPaymentMaster()
            };
            return View(model);
        }

        public async Task<IActionResult> PurchaseReturn()
        {
            var purchase = await billPaymentService.GetPurchaseReturnInfoMaster();
            int Cpurchase = 0;
            Cpurchase = purchase.Where(x => Convert.ToDateTime(x.invoiceDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "PurchaseReturn-No" + '-' + idate + '-' + (Cpurchase + 1);
            ViewBag.issueNo = issueNo;
            BillPaymentViewModel model = new BillPaymentViewModel
            {
                relSupplierCustomerResourses = await organizationService.GetAllOrganization(),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PurchaseReturn([FromForm] PurchaseReturnViewModel model)
        {
            //return Json(model);

            var purchase = await billPaymentService.GetBillReceiveInformation();
            int Cpurchase = 0;
            Cpurchase = purchase.Where(x => Convert.ToDateTime(x.billPaymentDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "PurchaseReturn-No-" + '-' + idate + '-' + (Cpurchase + 1);

            PurchaseReturnInfoMaster data = new PurchaseReturnInfoMaster
            {
                relSupplierCustomerResourseId = model.customerId,
                invoiceNumber = issueNo,
                purchaseOrdersMasterId = model.invoiceId,
                totalAmount = model.netTotal,
                invoiceDate = model.poDate,
                NetTotal = model.netTotal,
                isClose = 0,
                isStockClose = 0,
            };
            int saleCollectionId = await billPaymentService.SavePurchaseReturnInfoMaster(data);

            if (model.selectPaymentHeadIds != null)
            {
                for (int i = 0; i < model.selectPaymentHeadIds.Length; i++)
                {
                    PurchaseReturnInfoDetail purchaseReturnInfoDetail = new PurchaseReturnInfoDetail
                    {
                        purchaseReturnInfoMasterId = saleCollectionId,
                        purchaseOrdersDetailId = model.selectPaymentHeadIds[i],
                        quantity = model.selectPaymentHeadAmounts[i],
                    };
                    await billPaymentService.SavePurchaseReturnInfoDetail(purchaseReturnInfoDetail);
                }
            }

            return RedirectToAction("PurchaseReturnDetail", "PurchaseOrder", new
            {
                Id = saleCollectionId,
            });
        }

        public async Task<IActionResult> PurchaseReturnList()
        {
            PurchaseReturnViewModel model = new PurchaseReturnViewModel
            {
                purchaseReturnInfoMasters = await billPaymentService.GetPurchaseReturnInfoMaster(),
            };
            return View(model);
        }

       public async Task<IActionResult> BillReceiveList()
        {
            BillPaymentViewModel model = new BillPaymentViewModel
            {
                billReceiveInformation = await billPaymentService.GetBillReceiveInformation(),
            };
            return View(model);
        }

        public async Task<IActionResult> SuplierWithDue()
        {
            BillPaymentViewModel model = new BillPaymentViewModel
            {
                supplierWithDues = await billPaymentService.GetSupplierWithDue(),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BillReceive([FromForm] BillReceivedViewModel model)
        {
            //return Json(model);

            var purchase = await billPaymentService.GetBillReceiveInformation();
            int Cpurchase = 0;
            Cpurchase = purchase.Where(x => Convert.ToDateTime(x.billPaymentDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "BillRCV-No-" + '-' + idate + '-' + (Cpurchase + 1);

            BillReceiveInformation data = new BillReceiveInformation
            {
                relSupplierCustomerResourseId = model.customerId,
                billPaymentNo = issueNo,
                totalAmount = model.BillTotal,
                billPaymentDate = model.poDate,
                purchaseOrderMasterId = model.invoiceId,
                dueAmount = model.DueDate,
                ispaid = 0,
            };
            int saleCollectionId = await billPaymentService.SaveBillReceiveInformation(data);

            return RedirectToAction(nameof(BillReceiveList));
        }


        // POST: BillPayment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([FromForm] BillPaymentViewModel model)
        {
            if (model.total < 1)
            {
                model.relSupplierCustomerResourses = await billPaymentService.GetDueSupplierList();
                model.paymentModes = await purchaseOrderService.GetPaymentMode();
                model.billPaymentMasters = await billPaymentService.GetBillPaymentMaster();
                ModelState.AddModelError(string.Empty, "You have to pay minimum 1 taka.");
                return View(model);
            }

            if (model.paymentModeId == 1)
            {
                if (model.total != model.cashAmount)
                {
                    model.relSupplierCustomerResourses = await billPaymentService.GetDueSupplierList();
                    model.paymentModes = await purchaseOrderService.GetPaymentMode();
                    model.billPaymentMasters = await billPaymentService.GetBillPaymentMaster();
                    ModelState.AddModelError(string.Empty, "Total & Cash amount not same.");
                    return View(model);
                }

            }
            else if (model.paymentModeId == 2)
            {
                if (model.total != model.bankAmount)
                {
                    model.relSupplierCustomerResourses = await billPaymentService.GetDueSupplierList();
                    model.paymentModes = await purchaseOrderService.GetPaymentMode();
                    model.billPaymentMasters = await billPaymentService.GetBillPaymentMaster();
                    ModelState.AddModelError(string.Empty, "Total & Bank amount not same.");
                    return View(model);
                }

            }
            else
            {
                if (model.total != model.bankAmount + model.cashAmount)
                {
                    model.relSupplierCustomerResourses = await billPaymentService.GetDueSupplierList();
                    model.paymentModes = await purchaseOrderService.GetPaymentMode();
                    model.billPaymentMasters = await billPaymentService.GetBillPaymentMaster();
                    ModelState.AddModelError(string.Empty, "Total and Bank & Cash  amount not same.");
                    return View(model);
                }
            }
            //var accountinfo = await voucherService.GetVoucherSetting();
            var accountIdB = 0;
            var accountIdC = 0;
            decimal bankAmount = 0;
            decimal cashAmount = 0;
            if (model.paymentModeId == 1)
            {
                cashAmount = (decimal)model.cashAmount;
                //accountIdC = (int)accountinfo.FirstOrDefault().cashAccountLedgerId;

            }
            else if (model.paymentModeId == 2)
            {
                bankAmount = (decimal)model.bankAmount;
                //accountIdB = (int)accountinfo.FirstOrDefault().bankAccountLedgerId;

            }
            else
            {
                cashAmount = (decimal)model.cashAmount;
                bankAmount = (decimal)model.bankAmount;
            }
           

            var purchase = await billPaymentService.GetBillPaymentMaster();
            int Cpurchase = 0;
            Cpurchase = purchase.Where(x => Convert.ToDateTime(x.billPaymentDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "Payment-No-" + '-' + idate + '-' + (Cpurchase + 1);

            BillPaymentMaster data = new BillPaymentMaster
            {
                relSupplierCustomerResourseId = model.relSupplierCustomerResourseId,
                billNumber = issueNo,
                totalAmount = model.total,
                billPaymentDate = model.date,
                paymentModeId = model.paymentModeId,
                bankAmount = model.bankAmount,
                cashAmount = model.cashAmount,
                remarks = model.remarks
            };
            int saleCollectionId = await billPaymentService.SaveBillPaymentMaster(data);


            if (model.selectPaymentHeadIds != null)
            {
                for (var i = 0; i < model.selectPaymentHeadIds.Length; i++)
                {
                    BillPaymentDetail data1 = new BillPaymentDetail
                    {
                        billPaymentMasterId = saleCollectionId,
                        billReceiveInformationId = model.selectPaymentHeadIds[i],
                        amount = model.selectPaymentHeadAmounts[i],
                        date = model.date,
                    };
                    await billPaymentService.SaveBillPaymentDetail(data1);
                    if (model.selectPaymentHeadAmounts[i] == model.selectPaymentHeadDues[i])
                    {
                        //Update SalesInvoiceMaster isClose = 1;
                        await billPaymentService.UpdateBillReceiveInformationById((int)model.selectPaymentHeadIds[i]);
                    }
                }
            }

            //if (accountinfo.FirstOrDefault().voucherTypeId == 1)
            //{
            //    string voucherNoC = "";
            //    string voucherNoB = "";
            //    if (model.paymentModeId == 1)
            //    {

            //        var VocuherInfo = await ledgerService.GetAutoNumber((int)LedgerRelInfoC.ledgerId, 6, model.date.ToString("yyyyMMdd"));
            //        voucherNoC = VocuherInfo.autoNumber.ToString();
            //    }
            //    else if (model.paymentModeId == 2)
            //    {
            //        var VocuherInfo = await ledgerService.GetAutoNumber((int)LedgerRelInfoB.ledgerId, 6, model.date.ToString("yyyyMMdd"));
            //        voucherNoB = VocuherInfo.autoNumber.ToString();
            //    }
            //    else
            //    {
            //        var VocuherInfoC = await ledgerService.GetAutoNumber((int)LedgerRelInfoC.ledgerId, 6, model.date.ToString("yyyyMMdd"));
            //        var VocuherInfo = await ledgerService.GetAutoNumber((int)LedgerRelInfoB.ledgerId, 6, model.date.ToString("yyyyMMdd"));
            //        voucherNoB = VocuherInfo.autoNumber.ToString();
            //        voucherNoC = VocuherInfoC.autoNumber.ToString();

            //    }
            //    string userName = User.Identity.Name;
            //    if (model.paymentModeId == 1)
            //    {
            //        VoucherMaster voucherMaster = new VoucherMaster
            //        {
            //            Id = 0,
            //            voucherNo = voucherNoC,
            //            refNo = issueNo,
            //            voucherDate = model.date,
            //            voucherTypeId = 6,
            //            remarks = model.remarks,
            //            voucherAmount = cashAmount,
            //            isPosted = 0,
            //            companyId = 1,
            //            fiscalYearId = 13,
            //            taxYearId = 9,
            //            fundSourceId = 1,
            //            createdBy = userName
            //        };
            //        int VMID = await voucherService.SavevoucherMaster(voucherMaster);
            //        await voucherService.DeletevoucherDetailByVMId(Convert.ToInt32(VMID));
            //        VoucherDetail voucherDetail = new VoucherDetail();

            //        voucherDetail.Id = 0;
            //        voucherDetail.voucherId = VMID;
            //        voucherDetail.ledgerRelationId = accountIdC;
            //        voucherDetail.transectionModeId = 2;
            //        voucherDetail.amount = cashAmount;
            //        voucherDetail.accountName = LedgerRelInfoC.ledger.accountName + "(" + LedgerRelInfoC.ledger.accountCode + ")";
            //        voucherDetail.isPrincAcc = 1;
            //        int vid = await voucherService.SavevoucherDetail(voucherDetail);

            //        VoucherDetail voucherDetailCr = new VoucherDetail();

            //        voucherDetailCr.Id = 0;
            //        voucherDetailCr.voucherId = VMID;
            //        voucherDetailCr.ledgerRelationId = relledger.Id;
            //        voucherDetailCr.transectionModeId = 1;
            //        voucherDetailCr.amount = cashAmount;
            //        voucherDetailCr.accountName = LedgerRelInfoA.ledger.accountName + "(" + LedgerRelInfoA.ledger.accountCode + ")"; ;
            //        voucherDetailCr.isPrincAcc = 0;
            //        int vidcr = await voucherService.SavevoucherDetail(voucherDetailCr);
            //    }
            //    else if (model.paymentModeId == 2)
            //    {
            //        VoucherMaster voucherMaster = new VoucherMaster
            //        {
            //            Id = 0,
            //            voucherNo = voucherNoB,
            //            refNo = issueNo,
            //            voucherDate = model.date,
            //            voucherTypeId = 6,
            //            remarks = model.remarks,
            //            voucherAmount = bankAmount,
            //            isPosted = 0,
            //            companyId = 1,
            //            fiscalYearId = 13,
            //            taxYearId = 9,
            //            fundSourceId = 1,
            //            createdBy = userName
            //        };
            //        int VMID = await voucherService.SavevoucherMaster(voucherMaster);
            //        await voucherService.DeletevoucherDetailByVMId(Convert.ToInt32(VMID));
            //        VoucherDetail voucherDetail = new VoucherDetail();

            //        voucherDetail.Id = 0;
            //        voucherDetail.voucherId = VMID;
            //        voucherDetail.ledgerRelationId = accountIdB;
            //        voucherDetail.transectionModeId = 2;
            //        voucherDetail.amount = bankAmount;
            //        voucherDetail.accountName = LedgerRelInfoB.ledger.accountName + "(" + LedgerRelInfoB.ledger.accountCode + ")"; ;
            //        voucherDetail.isPrincAcc = 1;
            //        int vid = await voucherService.SavevoucherDetail(voucherDetail);

            //        VoucherDetail voucherDetailCr = new VoucherDetail();

            //        voucherDetailCr.Id = 0;
            //        voucherDetailCr.voucherId = VMID;
            //        voucherDetailCr.ledgerRelationId = relledger.Id;
            //        voucherDetailCr.transectionModeId = 1;
            //        voucherDetailCr.amount = bankAmount;
            //        voucherDetailCr.accountName = LedgerRelInfoA.ledger.accountName + "(" + LedgerRelInfoA.ledger.accountCode + ")"; ;
            //        voucherDetailCr.isPrincAcc = 0;
            //        int vidcr = await voucherService.SavevoucherDetail(voucherDetailCr);


            //    }
            //    else
            //    {
            //        VoucherMaster voucherMaster = new VoucherMaster
            //        {
            //            Id = 0,
            //            voucherNo = voucherNoB,
            //            refNo = issueNo,
            //            voucherDate = model.date,
            //            voucherTypeId = 6,
            //            remarks = model.remarks,
            //            voucherAmount = bankAmount,
            //            isPosted = 0,
            //            companyId = 1,
            //            fiscalYearId = 13,
            //            taxYearId = 9,
            //            fundSourceId = 1,
            //            createdBy = userName
            //        };
            //        int VMID = await voucherService.SavevoucherMaster(voucherMaster);
            //        VoucherDetail voucherDetail = new VoucherDetail();

            //        voucherDetail.Id = 0;
            //        voucherDetail.voucherId = VMID;
            //        voucherDetail.ledgerRelationId = accountIdB;
            //        voucherDetail.transectionModeId = 2;
            //        voucherDetail.amount = bankAmount;
            //        voucherDetail.accountName = LedgerRelInfoB.ledger.accountName + "(" + LedgerRelInfoB.ledger.accountCode + ")"; ;
            //        voucherDetail.isPrincAcc = 1;
            //        int vid = await voucherService.SavevoucherDetail(voucherDetail);

            //        VoucherDetail voucherDetailCr = new VoucherDetail();

            //        voucherDetailCr.Id = 0;
            //        voucherDetailCr.voucherId = VMID;
            //        voucherDetailCr.ledgerRelationId = relledger.Id;
            //        voucherDetailCr.transectionModeId = 1;
            //        voucherDetailCr.amount = bankAmount;
            //        voucherDetailCr.accountName = LedgerRelInfoA.ledger.accountName + "(" + LedgerRelInfoA.ledger.accountCode + ")"; ;
            //        voucherDetailCr.isPrincAcc = 0;
            //        int vidcr = await voucherService.SavevoucherDetail(voucherDetailCr);

            //        VoucherMaster voucherMasterC = new VoucherMaster
            //        {
            //            Id = 0,
            //            voucherNo = voucherNoC,
            //            refNo = issueNo,
            //            voucherDate = model.date,
            //            voucherTypeId = 6,
            //            remarks = model.remarks,
            //            voucherAmount = cashAmount,
            //            isPosted = 0,
            //            companyId = 1,
            //            fiscalYearId = 13,
            //            taxYearId = 9,
            //            fundSourceId = 1,
            //            createdBy = userName
            //        };
            //        int VMIDC = await voucherService.SavevoucherMaster(voucherMasterC);
            //        VoucherDetail voucherDetailc = new VoucherDetail();

            //        voucherDetailc.Id = 0;
            //        voucherDetailc.voucherId = VMIDC;
            //        voucherDetailc.ledgerRelationId = accountIdC;
            //        voucherDetailc.transectionModeId = 2;
            //        voucherDetailc.amount = cashAmount;
            //        voucherDetailc.accountName = LedgerRelInfoC.ledger.accountName + "(" + LedgerRelInfoC.ledger.accountCode + ")"; ;
            //        voucherDetailc.isPrincAcc = 1;
            //        int vidc = await voucherService.SavevoucherDetail(voucherDetailc);

            //        VoucherDetail voucherDetailCrc = new VoucherDetail();

            //        voucherDetailCrc.Id = 0;
            //        voucherDetailCrc.voucherId = VMIDC;
            //        voucherDetailCrc.ledgerRelationId = relledger.Id;
            //        voucherDetailCrc.transectionModeId = 1;
            //        voucherDetailCrc.amount = cashAmount;
            //        voucherDetailCrc.accountName = LedgerRelInfoA.ledger.accountName + "(" + LedgerRelInfoA.ledger.accountCode + ")"; ;
            //        voucherDetailCrc.isPrincAcc = 0;
            //        int vidcrc = await voucherService.SavevoucherDetail(voucherDetailCrc);
            //    }

            //}
            //return Json(data);
            return RedirectToAction(nameof(Index));
        }


        // GET: SalesCollection
        [AllowAnonymous]
        public async Task<IActionResult> BillCollectionReport(int id)
        {
            BillPaymentViewModel model = new BillPaymentViewModel
            {
                billPaymentMaster = await billPaymentService.GetBillPaymentMasterById(id),
                billPaymentDetails = await billPaymentService.GetBillPaymentDetailByMasterId(id),
                company = await iERPCompanyService.GetAllCompany()
            };
            return View(model);
        }

        //PrintPDF
        [AllowAnonymous]
        public IActionResult pdspdf(int id)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/Purchase/BillPayment/BillCollectionReport/" + id;

            string fileName;
            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [Route("global/api/GetDuePurchaseInvoiceByCustomerId/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetDuePurchaseInvoiceByCustomerId(int Id)
        {
            return Json(await purchaseOrderService.GetDuePurchaseInvoiceByCustomerId(Id));
        }

        [Route("global/api/GetDueBillReceiveByCustomerId/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetDueBillReceiveByCustomerId(int Id)
        {
            return Json(await purchaseService.GetDueBillReceiveByCustomerId(Id));
        }

        [Route("global/api/GetBillReceiveInformationByInvoiceId/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetBillReceiveInformationByInvoiceId(int Id)
        {
            return Json(await billPaymentService.GetBillReceiveInformationByInvoiceId(Id));
        }
    }
}