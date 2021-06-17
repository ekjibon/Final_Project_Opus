using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.CRMLead.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Services.Lead;
using OPUSERP.CRM.Services.Lead.Interfaces;
using OPUSERP.SCM.Services.PurchaseOrder.Interface;

namespace OPUSERP.Areas.CRMLead.Controllers
{
    [Area("CRMLead")]
    public class BillCollectionController : Controller
    {

        private readonly ILeadsService leadsService;
        private readonly IBillGenerateService billGenerateService;
        private readonly IBillCollectionService billCollectionService;
        private readonly IAgreementService agreementService;
        private readonly IPurchaseOrderService purchaseOrderService;
        public BillCollectionController(IAgreementService agreementService, IPurchaseOrderService purchaseOrderService, ILeadsService leadsService, IBillGenerateService billGenerateService, IBillCollectionService billCollectionService)
        {
            this.agreementService = agreementService;
            this.leadsService = leadsService;
            this.billGenerateService = billGenerateService;
            this.billCollectionService = billCollectionService;
            this.purchaseOrderService = purchaseOrderService;
        }

        #region Create Collection

        public async Task<IActionResult> Index()
        {
            var bankData = await billGenerateService.GetBankBranchDetails();
            BillCollectionViewModel model = new BillCollectionViewModel
            {
                paymentModes = await purchaseOrderService.GetPaymentMode(),
                bankBranchDetails = bankData.Where(a => a.collectionType == "Yes")

            };
            return View(model);
        }

        [HttpPost]       
        public async Task<IActionResult> SaveCollection([FromForm] BillCollectionViewModel model)
        {
           // var bankData = await billGenerateService.GetBankBranchDetails();
            //if (!ModelState.IsValid)
            //{
            //    model.paymentModes = await purchaseOrderService.GetPaymentMode();
            //    model.bankBranchDetails = bankData.Where(a => a.collectionType == "Yes");
            //    return View(model);
            //}

            int billCollectionId = 0;
            BillCollection data = new BillCollection
            {
                Id = Convert.ToInt32(model.billCollectionId),
                billGenerateId = model.billGenerateId,
                paymentModeId = model.paymentModeId,
                chequeNo = model.chequeNo,
                receivedDate = model.receivedDate,
                bankAmount = model.bankAmount,
                cashAmount = model.cashAmount,
                mobileBankAmount = model.mobileBankAmount,
                totalAmount = model.totalAmount,
                discount = model.discount,
                moneyReceipt = model.moneyReceipt,
                challanReceiptVat = model.challanReceiptVat,
                challanReceiptTax = model.challanReceiptTax,
                bankBranchDetailsId = model.bankBranchDetailsId,
                payOrderAmount = model.payOrderAmount
            };

            billCollectionId = await billCollectionService.SaveBillCollection(data);


            #region Save History           

            BillCollectionHistory collecHistory = new BillCollectionHistory
            {
                Id = 0,
                billCollectionId = billCollectionId,
                chequeNo = model.chequeNo,
                receivedDate = model.receivedDate,
                bankAmount = model.bankAmount,
                cashAmount = model.cashAmount,
                mobileBankAmount = model.mobileBankAmount,
                totalAmount = model.totalAmount,
                discount = model.discount,
                moneyReceipt = model.moneyReceipt,
                challanReceiptVat = model.challanReceiptVat,
                challanReceiptTax = model.challanReceiptTax,
            };
            await billCollectionService.SaveBillCollectionHistory(collecHistory);
            #endregion
            if (model.billCollectionId != 0)
            {
                return RedirectToAction(nameof(BillCollectionList));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> GetBillCollectionPendingList()
        {
            var data = await billCollectionService.GetBillCollectionPendingList();
            return Json(data.Where(a => a.dueAmount != 0));
        }
        [HttpGet]
        public async Task<IActionResult> GetBillCollectionPendingListSD(DateTime fromDate,DateTime toDate)
        {
            var data = await billCollectionService.GetBillCollectionPendingListDatefilter(HttpContext.User.Identity.Name,fromDate,toDate);

            return Json(data.Where(a => a.dueAmount != 0));
        }
        [HttpGet]
        public async Task<IActionResult> GetBillCollectionPendingListS(string TeamLeader, string Fa, string BD, string LeadId)
        {
            if (TeamLeader == "NoData")
            {
                TeamLeader = "";
            }
            if (Fa == "NoData")
            {
                Fa = "";
            }
            if (BD == "NoData")
            {
                BD = "";
            }

            if (LeadId == "NoData")
            {
                LeadId = "";
            }
            var data = await billCollectionService.GetBillCollectionPendingListfilter(HttpContext.User.Identity.Name ,TeamLeader,Fa,BD,LeadId);
            return Json(data.Where(a => a.dueAmount != 0));
        }
        [HttpGet]
        public async Task<IActionResult> GetBillCollectionPendingListFC()
        {
            var data = await billCollectionService.GetBillCollectionPendingListFC();
            return Json(data.Where(a => a.dueAmount != 0));
        }




        #endregion

        #region Bill Collection List

        public async Task<IActionResult> BillCollectionList()
        {
            var bankData = await billGenerateService.GetBankBranchDetails();
            BillCollectionViewModel model = new BillCollectionViewModel
            {
                paymentModes = await purchaseOrderService.GetPaymentMode(),
                bankBranchDetails = bankData.Where(a => a.collectionType == "Yes")

            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetBillCollectionList()
        {
            var data = await billCollectionService.GetBillCollectionPendingList();
            return Json(data.Where(a => a.billCollectionId != 0));
        }

        //[HttpPost]
        //public async Task<IActionResult> UpdateCollection([FromForm] BillCollectionViewModel model)
        //{           

        //    int billCollectionId = 0;
        //    BillCollection data = new BillCollection
        //    {
        //        Id = Convert.ToInt32(model.billCollectionId),
        //        chequeNo = model.chequeNo,
        //        receivedDate = model.receivedDate,
        //        bankAmount = model.bankAmount,
        //        cashAmount = model.cashAmount,
        //        mobileBankAmount = model.mobileBankAmount,
        //        totalAmount = model.totalAmount,
        //        discount = model.discount,
        //        moneyReceipt = model.moneyReceipt,
        //        challanReceiptVat = model.challanReceiptVat,
        //        challanReceiptTax = model.challanReceiptTax,               
        //        payOrderAmount = model.payOrderAmount
        //    };

        //    billCollectionId = await billCollectionService.SaveBillCollection(data);


        //    #region Save History           

        //    BillCollectionHistory collecHistory = new BillCollectionHistory
        //    {
        //        Id = 0,
        //        billCollectionId = billCollectionId,
        //        chequeNo = model.chequeNo,
        //        receivedDate = model.receivedDate,
        //        bankAmount = model.bankAmount,
        //        cashAmount = model.cashAmount,
        //        mobileBankAmount = model.mobileBankAmount,
        //        totalAmount = model.totalAmount,
        //        discount = model.discount,
        //        moneyReceipt = model.moneyReceipt,
        //        challanReceiptVat = model.challanReceiptVat,
        //        challanReceiptTax = model.challanReceiptTax,
        //    };
        //    await billCollectionService.SaveBillCollectionHistory(collecHistory);
        //    #endregion

        //    return RedirectToAction(nameof(BillCollectionList));
        //}

        #endregion

        #region

        public IActionResult BillDueList()
        {
            BillCollectionViewModel model = new BillCollectionViewModel
            {

            };
            return View(model);
        }


        #endregion


        #region API

        [Route("global/api/GetInvoiceByLeadId/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetInvoiceByLeadId(int id)
        {
            return Json(await billGenerateService.GetBillGenerateByLeadId(id));
        }

        [Route("global/api/GetInvoiceInfoByInvoiceId/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetInvoiceInfoByInvoiceId(int id)
        {
            return Json(await billGenerateService.GetbillgenerateInfobybillId(id));
        }


        #endregion
    }
}