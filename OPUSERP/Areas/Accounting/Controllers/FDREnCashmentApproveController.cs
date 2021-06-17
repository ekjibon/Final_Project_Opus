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
using OPUSERP.Accounting.Data.Entity.FDR;
using OPUSERP.Accounting.Services.AccountingSettings.Interfaces;
using OPUSERP.Accounting.Services.FDR.Interface;
using OPUSERP.Accounting.Services.MasterData.Interfaces;
using OPUSERP.Areas.Accounting.Models;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;

namespace OPUSERP.Areas.Accounting.Controllers
{
    [Authorize]
    [Area("Accounting")]
    public class FDREnCashmentApproveController : Controller
    {
        private readonly IBankBranchService bankBranchService;
        private readonly ILedgerService ledgerService;
        private readonly IFDRInvestmentService fDRInvestmentService;
        private readonly IBankChargeMasterService bankChargeMasterService;
        private readonly IAttachmentCommentService attachmentCommentService;
        private readonly ICostCentreService costCentreService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public FDREnCashmentApproveController(ILedgerService ledgerService, IBankBranchService bankBranchService, IBankChargeMasterService bankChargeMasterService, IFDRInvestmentService fDRInvestmentService, ICostCentreService costCentreService, IHostingEnvironment hostingEnvironment, IAttachmentCommentService attachmentCommentService)
        {
            this.ledgerService = ledgerService;
            this.bankBranchService = bankBranchService;
            this.bankChargeMasterService = bankChargeMasterService;
            this.fDRInvestmentService = fDRInvestmentService;
            this.costCentreService = costCentreService;
            this._hostingEnvironment = hostingEnvironment;
            this.attachmentCommentService = attachmentCommentService;
        }

        public async Task<IActionResult> Index()
        {
            FDREnCashmentViewModel model = new FDREnCashmentViewModel
            {
                banks = await bankBranchService.GetAllBank(),
            };
            return View(model);
        }
        public async Task<IActionResult> EnCashmentApproveNew(int? fdrID)
        {
            FDREnCashmentViewModel model = new FDREnCashmentViewModel
            {
                getListOfFDREncashmentWithFDRViewModels = await fDRInvestmentService.GetFDREncashmentInfo(fdrID),
            };
            return View(model);
        }
        public async Task<IActionResult> EnCashmentApprovedList()
        {
            FDREnCashmentViewModel model = new FDREnCashmentViewModel
            {
                banks = await bankBranchService.GetAllBank(),
            };
            return View(model);
        }
       
       
        [HttpPost]
        public async Task<JsonResult> ApproveFDREncashment([FromForm] FDREnCashmentViewModel encash)
        {
            #region AUTO Voucher

            string fdrNo = "Encash" + "/" + encash.AccountName;
            await costCentreService.FDREncashmentAutoVoucher(encash.TotalAmount, encash.ExciseDuty,encash.AdvanceTax,encash.PrincipalAmount,encash.ProvisionAmount,encash.EncashDate, fdrNo, HttpContext.User.Identity.Name);
            #endregion

            int? Result = 0;
            await fDRInvestmentService.UpdatefDREncashmentApprove(encash.ID, 1, encash.Remarks);
            return Json(Result);

        }
        [HttpPost]
        public async Task<JsonResult> ReturnFDREncashment([FromForm] FDREnCashmentViewModel encash)
        {
            int? Result = 0;
            await fDRInvestmentService.UpdatefDREncashmentApprove(encash.ID, 4, encash.Remarks);
            return Json(Result);

        }
        [HttpPost]
        public async Task<JsonResult> RejectFDREncashment([FromForm] FDREnCashmentViewModel encash)
        {
            int? Result = 0;
            await fDRInvestmentService.UpdatefDREncashmentApprove(encash.ID, -1, encash.Remarks);
            return Json(Result);

        }
    }
}