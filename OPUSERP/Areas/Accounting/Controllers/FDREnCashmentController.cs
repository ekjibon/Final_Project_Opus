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
using OPUSERP.Areas.Accounting.Models;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;

namespace OPUSERP.Areas.Accounting.Controllers
{
    [Authorize]
    [Area("Accounting")]
    public class FDREnCashmentController : Controller
    {
        private readonly IBankBranchService bankBranchService;
        private readonly ILedgerService ledgerService;
        private readonly IFDRInvestmentService fDRInvestmentService;
        private readonly IBankChargeMasterService bankChargeMasterService;
        private readonly IAttachmentCommentService attachmentCommentService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public FDREnCashmentController(ILedgerService ledgerService, IBankBranchService bankBranchService, IBankChargeMasterService bankChargeMasterService, IFDRInvestmentService fDRInvestmentService, IHostingEnvironment hostingEnvironment, IAttachmentCommentService attachmentCommentService)
        {
            this.ledgerService = ledgerService;
            this.bankBranchService = bankBranchService;
            this.bankChargeMasterService = bankChargeMasterService;
            this.fDRInvestmentService = fDRInvestmentService;
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
        public async Task<IActionResult> EnCashmentNew(int? fdrID)
        {
            FDREnCashmentViewModel model = new FDREnCashmentViewModel
            {
                getFDRInformationForEncashmentViewModels = await fDRInvestmentService.GetFDRInformationForEncashment(fdrID),
                banks = await bankBranchService.GetAllBank(),
            };
            return View(model);
        }
        public async Task<IActionResult> EnCashmentNewCopy(int? fdrID)
        {
            FDREnCashmentViewModel model = new FDREnCashmentViewModel
            {
                getFDRInformationForEncashmentViewModels = await fDRInvestmentService.GetFDRInformationForEncashment(fdrID),
                banks = await bankBranchService.GetAllBank(),
            };
            return View(model);
        }
        public async Task<IActionResult> EnCashmentList()
        {
            FDREnCashmentViewModel model = new FDREnCashmentViewModel
            {
                banks = await bankBranchService.GetAllBank(),
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFDREncashPendingList(string bankName, string frmDate, string toDate)
        {
            string userName = HttpContext.User.Identity.Name;
            return Json(await fDRInvestmentService.GETFDRMaturityStatus(bankName, frmDate, toDate, userName));
        }

        [HttpGet]
        public async Task<IActionResult> GetFDREncashmentList(string bankName, string frmDate, string toDate, int? fDRType)
        {
            string userName = HttpContext.User.Identity.Name;
            return Json(await fDRInvestmentService.GetListOfFDREncashmentWithFDR(bankName, frmDate, toDate, userName, fDRType));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteFDREncashmentByEncashId(int Id)
        {
            await fDRInvestmentService.DeleteFDREncashmentByEncashId(Id);           
            return Json(true);
        }

        [HttpPost]
        public async Task<JsonResult> ProcessFDREncashment([FromForm] FDREnCashmentViewModel encash, IFormFile imagePath)
        {
            int? Result = 0;
            FDREncashment eFDREncash = new FDREncashment();
            string userid = HttpContext.User.Identity.Name;

            eFDREncash.fDRInvestmentDetailId = encash.FDRID;
            eFDREncash.encashDate = Convert.ToDateTime(encash.EncashDate);
            eFDREncash.provisionAmount = encash.ProvisionAmount;
            eFDREncash.receiveAmount = encash.ReceiveAmount;
            eFDREncash.diffAmount = encash.DiffAmount;
            eFDREncash.percentDiff = encash.PercentDiff;
            eFDREncash.exciseDuty = encash.ExciseDuty;
            eFDREncash.advanceTax = encash.AdvanceTax;
            eFDREncash.totalAmount = encash.TotalAmount;
            eFDREncash.encashStatus = encash.MaturityStatus;
            eFDREncash.prematurePercent = encash.PrematurePercent;
            eFDREncash.accountName = encash.accountName;
            eFDREncash.principleBankId = encash.principleBankId;
            eFDREncash.principleBankAccountNumber = encash.principleBankAccountNumber;
            eFDREncash.interestBankId = encash.interestBankBankId;
            eFDREncash.interestBankAccountNumber = encash.interestBankAccountNumber;
            eFDREncash.status = 0;
            if (encash.DiffAmount < 0)
            {
                eFDREncash.typeOfDiff = -1;
            }
            else if (encash.DiffAmount > 0)
            {
                eFDREncash.typeOfDiff = 1;
            }
            else
            {
                eFDREncash.typeOfDiff = 0;
            }
            if (encash.ID > 0)
            {
                eFDREncash.Id = Convert.ToInt32(encash.ID);
                eFDREncash.updatedBy = HttpContext.User.Identity.Name;
                eFDREncash.updatedAt = DateTime.Now;
                Result = await fDRInvestmentService.SaveFDREncashment(eFDREncash);
            }
            else
            {
                eFDREncash.createdBy = HttpContext.User.Identity.Name;
                eFDREncash.createdAt = DateTime.Now;
                Result = await fDRInvestmentService.SaveFDREncashment(eFDREncash);
            }

            if (imagePath != null)
            {
                string userName = User.Identity.Name;
                string documentType = "photo";
                var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                var fileName = ContentDispositionHeaderValue.Parse(imagePath.ContentDisposition).FileName;
                string fileType = Path.GetExtension(fileName);
                fileName = fileName.Contains("\\")
                    ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                    : fileName.Trim('"');

                string NewFileName = Result + "_" + documentType + "_" + fileName;
                string fileLocation = "/Upload/Photo/" + NewFileName;
                var fullFilePath = Path.Combine(filesPath, NewFileName);

                using (var stream = new FileStream(fullFilePath, FileMode.Create))
                {
                    await imagePath.CopyToAsync(stream);
                }

                DocumentPhotoAttachment dataPH = new DocumentPhotoAttachment
                {
                    Id = 0,
                    actionType = "FDR Encash",
                    actionTypeId = Result,
                    subject = "",
                    fileName = NewFileName,
                    filePath = fileLocation,
                    fileType = fileType,
                    description = "",
                    documentType = documentType,
                    moduleId = 4,
                    createdBy = userName
                };
                await attachmentCommentService.SaveDocumentAttachment(dataPH);
            }

            return Json(Result);

        }
       
        
       }
    }