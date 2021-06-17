using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Auth.Models;
using OPUSERP.Areas.SCMPurchaseOrder.Models;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using OPUSERP.SCM.Helpers;
using OPUSERP.SCM.Services.PurchaseOrder.Interface;
using OPUSERP.SCM.Services.PurchaseProcess.Interfaces;
using OPUSERP.SCM.Services.SCMMail.interfaces;

namespace OPUSERP.Areas.SCMPurchaseOrder.Controllers
{
    [Area("SCMPurchaseOrder")]
    public class PurchaseOrderController : Controller
    {
        private readonly IAddressService addressService;
        private readonly IPurchaseProcessService purchaseProcessService;
        private readonly IPurchaseOrderService purchaseOrderService;
        private readonly RequisitionStatusHistory requisitionStatusHistory;
        private readonly IUserInfoes userInfo;
        private readonly ISCMMailService sCMMailService;
        private readonly string rootPath;
        private readonly MyPDF myPDF;

        public PurchaseOrderController(IAddressService addressService, RequisitionStatusHistory requisitionStatusHistory, IHostingEnvironment hostingEnvironment, IConverter converter, IPurchaseProcessService purchaseProcessService, IUserInfoes userInfo, IPurchaseOrderService purchaseOrderService, ISCMMailService sCMMailService)
        {
            this.addressService = addressService;
            this.purchaseProcessService = purchaseProcessService;
            this.purchaseOrderService = purchaseOrderService;
            this.requisitionStatusHistory = requisitionStatusHistory;
            this.userInfo = userInfo;
            this.sCMMailService = sCMMailService;
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        // GET: PurchaseOrder
        public async Task<ActionResult> Index()
        {
            string userName = HttpContext.User.Identity.Name;
            PurchaseOrderViewModel model = new PurchaseOrderViewModel
            {
                purchaseOrderMasters = await purchaseOrderService.GetPurchaseOrderMaster(userName),
                issuedpurchaseOrder = await purchaseOrderService.GetIssuedPurchaseOrderMaster(userName)

            };
            return View(model);
        }

        public async Task<ActionResult> CreatePO()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos =await userInfo.GetUserInfoByUser(userName);
            PurchaseOrderViewModel model = new PurchaseOrderViewModel
            {
                cSMasters = await purchaseProcessService.GetCSMasterListForPO(userInfos.UserId),
                deliveryLocations = await purchaseOrderService.GetDeliveryLocation(),
                deliveryModes = await purchaseOrderService.GetDeliveryMode(),
                paymentModes = await purchaseOrderService.GetPaymentMode(),
                paymentTerms = await purchaseOrderService.GetpaymentTerms(),
                currencies = await purchaseOrderService.GetCurrency(),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] PurchaseOrderViewModel model)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);

            if (!ModelState.IsValid || model.csDetailsall == null)
            {
                model.cSMasters = await purchaseProcessService.GetCSMasterList(userInfos.UserId);
                model.deliveryLocations = await purchaseOrderService.GetDeliveryLocation();
                model.deliveryModes = await purchaseOrderService.GetDeliveryMode();
                model.paymentModes = await purchaseOrderService.GetPaymentMode();
                model.paymentTerms = await purchaseOrderService.GetpaymentTerms();
                model.currencies = await purchaseOrderService.GetCurrency();
                if (model.csDetailsall == null)
                {
                    ModelState.AddModelError(string.Empty, "You have to Add minimum 1 Item");
                }
                return View(model);
            }

            //return Json(model);

            var poNoInfo = purchaseOrderService.GetPuerchaseOrderNo((int)model.txtRequsitionId);
            PurchaseOrderMaster data = new PurchaseOrderMaster
            {
                Id = model.PurchaseOrderId,
                poNo = poNoInfo.autoNumber,
                poDate = model.poDate,
                csMasterId = model.txtCsmasterId,
                deliveryDate = model.deliveryDate,
                supplierId= model.txtSupplierId,
                poStatus = 1,
                paymentModeId = model.paymentType,
                paymentTermsId = model.paymenTerm,
                deliveryModeId = model.deliveryType,
                billToLocation = model.billToLocation,
                receiveStatus = 0,
                userId = userInfos.UserId,
                savingAmount = model.savingsAmount,
                savingPercent = model.savingsPercent,
                remarks = model.remarks
            };

            int masterId = await purchaseOrderService.SavePurchaseOrderMaster(data);

            if (model.PurchaseOrderId > 0)
            {
                await purchaseOrderService.DeletePurchaseOrderDetailsByMasterId(model.PurchaseOrderId);
                await purchaseOrderService.DeletePOTermAndConditionByMasterId(model.PurchaseOrderId);
            }

            POTermAndCondition pOTermAndCondition = new POTermAndCondition
            {
                purchaseId = masterId,
                tarmsType = "Purchase Order",
                description = model.content,
            };

            await purchaseOrderService.SavePOTermAndCondition(pOTermAndCondition);

            for (int i = 0; i < model.csDetailsall.Length; i++)
            {
                PurchaseOrderDetails data1 = new PurchaseOrderDetails
                {
                    purchaseId = masterId,
                    cSDetailId = model.csDetailsall[i],
                    poQty = model.poQntall[i],
                    poRate = model.txtUnitRateall[i],
                    vat = model.txtVatAmountall[i],
                    vatPercent = model.txtVatall[i],
                    tax = model.txtAitAmountall[i],
                    taxPercent = model.txtAitall[i],
                    currencyId = model.currencyall[i],
                    deliveryLocationId= model.txtLocationall[i],
                    otherDeliveryLocation = model.txtOtherLocationall[i],
                };
                await purchaseOrderService.SavePurchaseOrderDetails(data1);
            }

            var cSMaster = await purchaseProcessService.GetCSMasterById(Convert.ToInt32(model.txtCsmasterId));
            string empNameCode = userInfos.EmpCode + "-" + userInfos.EmpName;
            //string nextEmpNameCode = nextUserInfo.EmpCode + "-" + nextUserInfo.EmpName;
            await requisitionStatusHistory.SaveRequisitionStatusLog(Convert.ToInt32(cSMaster.requisitionId), 2, Convert.ToInt32(userInfos.UserTypeId), userInfos.UserId, empNameCode, "", "", 14, "PO", masterId, poNoInfo.autoNumber);

            //string host = HttpContext.Request.Host.ToString();
            //string scheme = Request.Scheme;
            //string baseUrl = $"" + scheme + "://" + host + "/Auth/Account/Login";
            //await sCMMailService.MailMessage(nextEmail, poNoInfo.autoNumber, logStatusId, empNameCode, baseUrl);
            return RedirectToAction(nameof(Index));
        }

        #region api

        [Route("global/api/GetPurchaseOrderNo/{reqId}")]
        [HttpGet]
        public IActionResult GetPurchaseOrderNo(int reqId)
        {
            return Json(purchaseOrderService.GetPuerchaseOrderNo(reqId));
        }

        [Route("global/api/GetCSDetailListByMasterId/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetCSDetailListByMasterId(int id)
        {
            return Json(await purchaseProcessService.GetCSSupplierListBycsId(id));
        }

        [Route("global/api/GetCSDetailListBySupplierId/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetCSDetailListBySupplierId(int id)
        {
            return Json(await purchaseProcessService.GetCSDetailListBySupplierId(id));
        }

        [Route("global/api/GetCSDetailListByCSAndSupplierId/{csId}/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetCSDetailListBySupplierId(int csId,int id)
        {
            return Json(await purchaseProcessService.GetCSDetailListByCSAndSupplierId(csId,id));
        }

        [Route("global/api/TestRPTData/{id}")]
        [HttpGet]
        public async Task<IActionResult> TestRPTData(int id)
        {
            var result = await purchaseOrderService.GetRPTPurchaseOrderDetailsByMasterId(id);
            return Json(result);
        }
        #endregion

    }
}