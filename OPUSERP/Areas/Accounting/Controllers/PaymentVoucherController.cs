using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using OPUSERP.Accounting.Data.Entity.Voucher;
using OPUSERP.Accounting.Services.AccountingSettings.Interfaces;
using OPUSERP.Accounting.Services.MasterData.Interfaces;
using OPUSERP.Accounting.Services.Voucher.Interfaces;
using OPUSERP.Areas.Accounting.Models;
using OPUSERP.Areas.MasterData.Models;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.Payroll.Services.Salary.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Accounting.Controllers
{
    [Authorize]
    [Area("Accounting")]
    public class PaymentVoucherController : Controller
    {
        private readonly ILedgerService ledgerService;
        private readonly IVoucherService voucherService;
        private readonly IUserInfoes userInfo;
        private readonly IAttachmentCommentService attachmentCommentService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ICostCentreService costCentreService;
        private readonly IOpeningBalanceService openingBalanceService;
        private readonly ISalaryService salaryService;
        private readonly IERPCompanyService eRPCompanyService;
        private readonly ISignatureService signatureService;
        private readonly string rootPath;
        private readonly MyPDF myPDF;

        public string FileName;
        public PaymentVoucherController(IHostingEnvironment hostingEnvironment, IOpeningBalanceService openingBalanceService, ICostCentreService costCentreService, ILedgerService ledgerService, ISalaryService salaryService, IERPCompanyService eRPCompanyService, IAttachmentCommentService attachmentCommentService, IUserInfoes userInfo, IVoucherService voucherService, IConverter converter, ISignatureService signatureService)
        {
            this.ledgerService = ledgerService;
            this._hostingEnvironment = hostingEnvironment;
            this.attachmentCommentService = attachmentCommentService;
            this.voucherService = voucherService;
            this.ledgerService = ledgerService;
            this.costCentreService = costCentreService;
            this.openingBalanceService = openingBalanceService;
            this.salaryService = salaryService;
            this.eRPCompanyService = eRPCompanyService;
            this.userInfo = userInfo;
            this.signatureService = signatureService;

            myPDF = new MyPDF(hostingEnvironment, converter);

            rootPath = hostingEnvironment.ContentRootPath;
        }


        [HttpGet]
        public async Task<IActionResult> Index(int id, string actionType)
        {
            string username = HttpContext.User.Identity.Name;
            var userinfo = await userInfo.GetUserInfoByUser(username);
            ViewBag.branchid = userinfo?.specialBranchUnitId;

            ViewBag.masterId = 0;
            if (id > 0)
            {
                ViewBag.masterId = id;
            }
            var data = await voucherService.GetvoucherDetailByMasterId(id);

            VoucherMasterViewModel model = new VoucherMasterViewModel
            {
                VoucherMaster = await voucherService.GetGetvoucherMasterById(id),
                costCentres = await costCentreService.GetCostCentres()

            };
            if (model.VoucherMaster == null) model.VoucherMaster = new VoucherMaster();


            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> Index([FromForm] VoucherMasterViewModel model)
        {
            string userName = User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            ViewBag.sbuId = userInfos.projectId;
            var salaryYear = await salaryService.GetAllSalaryYear();
            var taxYear = await salaryService.GetAllTaxYear();
            var company = await eRPCompanyService.GetAllCompany();
            VoucherMaster voucherMaster = new VoucherMaster
            {
                Id = Convert.ToInt32(model.voucherId),
                voucherNo = model.voucherNo,
                refNo = model.refNo,
                voucherDate = model.voucherDate,
                voucherTypeId = 6,
                remarks = model.remarks,
                voucherAmount = model.voucherAmount,
                isPosted = 1,
                companyId = company.OrderByDescending(a => a.Id).FirstOrDefault().Id,
                fiscalYearId = salaryYear.OrderByDescending(a => a.Id).FirstOrDefault().Id,
                taxYearId = taxYear.OrderByDescending(a => a.Id).FirstOrDefault().Id,
                fundSourceId = 1,
                projectId = model.projectId
            };
            int VMID = await voucherService.SavevoucherMaster(voucherMaster);

            if (model.voucherId > 0)
            {
                await voucherService.DeleteCostCentreAllocationByVMId(Convert.ToInt32(model.voucherId));
                await voucherService.DeletevoucherDetailByVMId(Convert.ToInt32(model.voucherId));

            }
            if (model.ledgerRelationId.Length > 0)
            {
                var doAccountCode = 0;
                for (int i = 0; i < model.ledgerRelationId.Length; i++)
                {
                    //if (model.accountCode[i].StartsWith("4"))
                    if (model.natureId[i] == 4)
                    {
                        int vid = 0;
                        decimal Amount = 0;

                        for (int z = 0; z < model.costCentreallId.Length; z++)
                        {
                            if (model.transectionModeId[i] == 1)
                            {
                                //if (model.costCentreallId[z] != 0 && model.accountCode[z] == model.accountCode[i] && model.transectionModeId[z] == model.transectionModeId[i])
                                //{
                                if (model.costCentreallId[z] != 0 && model.ledgerRelationId[z] == model.ledgerRelationId[i] && model.transectionModeId[z] == model.transectionModeId[i])
                                {
                                    Amount = Amount + (decimal)model.drAmount[z];
                                }
                            }
                            else
                            {
                                if (model.costCentreallId[z] != 0 && model.ledgerRelationId[z] == model.ledgerRelationId[i] && model.transectionModeId[z] == model.transectionModeId[i])
                                {
                                    Amount = Amount + (decimal)model.crAmount[z];
                                }
                            }
                        }
                        VoucherDetail voucherDetail = new VoucherDetail();

                        voucherDetail.Id = 0;
                        voucherDetail.voucherId = VMID;
                        voucherDetail.ledgerRelationId = Convert.ToInt32(model.ledgerRelationId[i]);
                        voucherDetail.transectionModeId = model.transectionModeId[i];

                        voucherDetail.amount = Amount;
                        voucherDetail.accountName = model.subLedgerName[i];
                        voucherDetail.isPrincAcc = model.isPrincAcc[i];
                        if (doAccountCode != (int)model.ledgerRelationId[i])
                        {
                            vid = await voucherService.SavevoucherDetail(voucherDetail);


                            for (int j = 0; j < model.costCentreallId.Length; j++)
                            {
                                if (model.ledgerRelationId[j] == model.ledgerRelationId[i])
                                {


                                    CostCentreAllocation costCentreAllocation = new CostCentreAllocation();
                                    costCentreAllocation.Id = 0;
                                    costCentreAllocation.costCentreId = model.costCentreallId[j];
                                    costCentreAllocation.voucherDetailId = vid;
                                    if (model.transectionModeId[i] == 1)
                                    {
                                        costCentreAllocation.amount = model.drAmount[j];
                                    }
                                    else
                                    {
                                        costCentreAllocation.amount = model.crAmount[j];
                                    }
                                    if (costCentreAllocation.costCentreId > 0 && costCentreAllocation.amount > 0)
                                    {
                                        await voucherService.SavecostCentreAllocation(costCentreAllocation);
                                    }
                                }
                            }
                            doAccountCode = (int)model.ledgerRelationId[i];
                        }
                    }
                    else
                    {
                        VoucherDetail voucherDetail = new VoucherDetail();

                        voucherDetail.Id = 0;
                        voucherDetail.voucherId = VMID;
                        voucherDetail.ledgerRelationId = Convert.ToInt32(model.ledgerRelationId[i]);
                        voucherDetail.transectionModeId = model.transectionModeId[i];
                        if (model.transectionModeId[i] == 1)
                        {
                            voucherDetail.amount = model.drAmount[i];
                        }
                        else
                        {
                            voucherDetail.amount = model.crAmount[i];
                        }
                        voucherDetail.accountName = model.subLedgerName[i];
                        voucherDetail.isPrincAcc = model.isPrincAcc[i];
                        int vid = await voucherService.SavevoucherDetail(voucherDetail);

                    }
                }

            }
            return Json(VMID);
        }
        [HttpGet]
        public async Task<IActionResult> PaymentVoucherList()
        {
            var model = new VoucherMasterViewModel
            {
                //GetVoucherMasters =  voucherService.GetvoucherMasterByTypeId(6),
                GetVoucherMasters = await voucherService.GetvoucherMasterDetailsByTypeId(6),
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AllVoucherList()
        {
            var model = new VoucherMasterViewModel
            {
                //voucherLists = await voucherService.GetvoucherMaster(),
                voucherLists = await voucherService.GetvoucherMasterDetailsByTypeId(0),
            };
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult PaymentVoucherPdfAction(int MasterId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/Accounting/PaymentVoucher/PaymentVoucherPDF?MasterId=" + MasterId;

            string fileName;
            string status = myPDF.GeneratePDFVoucher(out fileName, url);


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
        public async Task<IActionResult> PaymentVoucherPDF(int MasterId)
        {
            var data = await voucherService.GetvoucherDetailByMasterId(MasterId);
            var ledgerRelationId = data.FirstOrDefault().ledgerRelationId;
            var dataLedger = await ledgerService.GetledgerRelationByLedgerRelationId(ledgerRelationId);
            var companyId = dataLedger.FirstOrDefault().ledger.companyId;
            var costInf = await voucherService.GetCostCentreAllocationsByDetailId(MasterId);
            if (costInf == null)
            {
                costInf = new List<CostCentreAllocation>();
            }
            var model = new VoucherMasterViewModel
            {
                VoucherMaster = await voucherService.GetGetvoucherMasterById(MasterId),
                GetVoucherDetailsByMasterId = await voucherService.GetvoucherDetailByMasterId(MasterId),
                costCentreAllocations = costInf,
                companies = await eRPCompanyService.GetAllCompany(),
                getSignatureViewModel = await signatureService.GetSignatureByType(1)
            };
            string Val = model.GetVoucherDetailsByMasterId.Where(x => x.transectionModeId == 1).Sum(x => x.amount).ToString();
            ViewBag.InWord = AmountInWord.ConvertToWords(Val);

            return PartialView(model);
        }

        public async Task<IActionResult> PaymentDetails(int id)
        {
            try
            {
                ViewBag.masterId = id;
                var data = await voucherService.GetvoucherDetailByMasterId(id);
                var ledgerRelationId = data.FirstOrDefault().ledgerRelationId;
                var dataLedger = await ledgerService.GetledgerRelationByLedgerRelationId(ledgerRelationId);
                var companyId = dataLedger.FirstOrDefault().ledger.companyId;
                var CommentInfo = await attachmentCommentService.GetCommentByActionTypeId(id, "Voucher", 4);
                if (CommentInfo == null)
                {
                    CommentInfo = new List<Comment>();
                }
                var photoInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "Voucher", "photo", 4);
                if (photoInfo == null)
                {
                    photoInfo = new List<DocumentPhotoAttachment>();
                }
                var docInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "Voucher", "Document", 4);
                if (docInfo == null)
                {
                    docInfo = new List<DocumentPhotoAttachment>();
                }
                var costInf = await voucherService.GetCostCentreAllocationsByDetailId(id);
                if (costInf == null)
                {
                    costInf = new List<CostCentreAllocation>();
                }

                var model = new VoucherMasterViewModel
                {
                    GetGetvoucherMasterById = await voucherService.GetGetvoucherMasterById(id),
                    VoucherDetails = await voucherService.GetvoucherDetailByMasterId(id),
                    GetVoucherDetailsByMasterId = await voucherService.GetvoucherDetailByMasterId(id),
                    company = await ledgerService.Company(Convert.ToInt32(companyId)),
                    costCentreAllocations = costInf,
                    comments = CommentInfo,
                    photoes = photoInfo,
                    documents = docInfo,
                };
                string Val = model.GetVoucherDetailsByMasterId.Where(x => x.transectionModeId == 1).Sum(x => x.amount).ToString();
                ViewBag.InWord = AmountInWord.ConvertToWords(Val);
                return View(model);
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
                    actionType = "Voucher",
                    actionTypeId = model.actionTypeId,
                    title = model.title,
                    comment = model.comment,
                    moduleId = 4,
                };
                int commentsId = await attachmentCommentService.SaveComment(entityComment);
                return RedirectToAction("PaymentDetails", "PaymentVoucher", new { id = model.actionTypeId, Area = "Accounting" });

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
            return RedirectToAction("PaymentDetails", "PaymentVoucher", new { id = actionId, Area = "Accounting" });
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
                        moduleId = 4
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);
                }

                return RedirectToAction("PaymentDetails", "PaymentVoucher", new { id = model.actionTypeId, Area = "Accounting" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                        moduleId = 4
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);
                }

                return RedirectToAction("PaymentDetails", "PaymentVoucher", new { id = model.actionTypeId, Area = "Accounting" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
            return RedirectToAction("PaymentDetails", "PaymentVoucher", new { id = actionId, Area = "Accounting" });
        }


        #region API

        [Route("/api/global/GetVoucherInformation/{id}")]
        public async Task<ActionResult<System.Object>> GetVoucherInformation(int id)
        {
            var master = await voucherService.GetGetvoucherMasterById(id);
            var ddetails = await voucherService.GetvoucherDetailByMasterId(id);
            var details = ddetails.Where(x => x.isPrincAcc == 0);
            var prinAccount = ddetails.Where(x => x.isPrincAcc == 1).FirstOrDefault();
            var costdetails = await voucherService.GetCostCentreAllocationsByDetailId(id);
            return Ok(new { master, details, prinAccount, costdetails });
        }

        [Route("/api/global/getVoucherNo/{accountId}/{voucherTypeId}/{voucherDate}")]
        public async Task<JsonResult> GetVoucherNumber(int accountId, int voucherTypeId, string voucherDate)
        {
            var voucherNo = await ledgerService.GetAutoNumber(accountId, voucherTypeId, voucherDate);
            return Json(voucherNo.autoNumber);
        }

        [Route("/api/global/getLedersForPayment/{branchid}")]
        public async Task<JsonResult> GetLedgersForPayment(int branchid)
        {
            var ledgers = await ledgerService.GetLedgerForPayment(branchid);
            return Json(ledgers);
        }

        [Route("/api/global/GetOnlyCashLedger/{branchid}")]
        public async Task<JsonResult> GetOnlyCashLedger(int branchid)
        {
            var ledgers = await ledgerService.GetOnlyCashLedger(branchid);
            return Json(ledgers);
        }

        [Route("/api/global/GetOnlyBankLedger/{branchid}")]
        public async Task<JsonResult> GetOnlyBankLedger(int branchid)
        {
            var ledgers = await ledgerService.GetOnlyBankLedger(branchid);
            return Json(ledgers);
        }

        [Route("/api/global/getLedersForParticular/{branchid}")]
        public async Task<JsonResult> GetLedgersForParticular(int branchid)
        {
            var subLedgers = await ledgerService.GetLedgerForParticular(branchid);
            return Json(subLedgers);
        }

        [Route("/api/global/GetAllLedgerForJournal/{branchid}")]
        public async Task<JsonResult> GetAllLedgerForJournal(int branchid)
        {
            var subLedgers = await ledgerService.GetAllLedgerForJournal(branchid);
            return Json(subLedgers);
        }

        [Route("/api/global/GetLedgerAllBySbuId/{branchid}")]
        public async Task<JsonResult> GetLedgerAllBySbuId(int branchid)
        {
            var subLedgers = await ledgerService.GetLedgerAllBySbuId(branchid);
            return Json(subLedgers);
        }

        [Route("/api/global/getLedgerForOpeningBalance/")]
        public async Task<JsonResult> GetLedgerForOpeningBalance()
        {
            var subLedgers = await ledgerService.GetLedgerForOpeningBalance();
            return Json(subLedgers);
        }

        [Route("/api/global/getSubLedersByLedgerId/{ledgerId}")]
        public async Task<JsonResult> GetSubLedgers(int ledgerId)
        {
            string userName = User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            var subLedgers = await ledgerService.GetSubledgerByLedgerBrId(ledgerId, (int)userInfos?.specialBranchUnitId);
            return Json(subLedgers);
        }

        [Route("/api/global/getOpenBalanceById/{id}")]
        public async Task<JsonResult> getOpenBalance(int id)
        {
            decimal OpenBalanceAmount = 0;
            var ledgerdata = await ledgerService.GetledgerRelationById(id);
            var data = await ledgerService.GetledgerById((int)ledgerdata.ledgerId);
            if (data.haveSubLedger == 2)
            {

            }
            else
            {
                if (data.haveSubLedger == 0)
                {
                    var OpenBalanace = await openingBalanceService.GetOpeningBalancebyLedgerRelId(id);
                    var Voucherdata = await voucherService.GetGetvoucherDetailByLedgerRelId(id);
                    var drBalAmount = OpenBalanace.Where(x => x.transectionModeId == 1).Sum(x => x.amount);
                    var crBalAmount = OpenBalanace.Where(x => x.transectionModeId == 2).Sum(x => x.amount);
                    var drBTrAmount = Voucherdata.Where(x => x.transectionModeId == 1).Sum(x => x.amount);
                    var crTrAmount = Voucherdata.Where(x => x.transectionModeId == 2).Sum(x => x.amount);
                    OpenBalanceAmount = (decimal)drBalAmount + (decimal)drBTrAmount - (decimal)crBalAmount - (decimal)crTrAmount;
                }
                if (data.haveSubLedger == 1)
                {
                    List<OpeningBalance> OpenBalanace = new List<OpeningBalance>();
                    var allLedgerRel = await ledgerService.GetledgerRelationByLedgerId((int)ledgerdata.ledgerId);

                    foreach (var reldata in allLedgerRel)
                    {
                        var Cdata = await openingBalanceService.GetOpeningBalancebyLedgerRelId(reldata.Id);
                        OpenBalanace.AddRange((List<OpeningBalance>)Cdata);

                    }
                    List<VoucherDetail> Voucherdata = new List<VoucherDetail>();
                    foreach (var reldata in allLedgerRel)
                    {
                        var Cdata = await voucherService.GetGetvoucherDetailByLedgerRelId(reldata.Id);
                        Voucherdata.AddRange((List<VoucherDetail>)Cdata);

                    }

                    var drBalAmount = OpenBalanace.Where(x => x.transectionModeId == 1).Sum(x => x.amount);
                    var crBalAmount = OpenBalanace.Where(x => x.transectionModeId == 2).Sum(x => x.amount);
                    var drBTrAmount = Voucherdata.Where(x => x.transectionModeId == 1).Sum(x => x.amount);
                    var crTrAmount = Voucherdata.Where(x => x.transectionModeId == 2).Sum(x => x.amount);
                    OpenBalanceAmount = (decimal)drBalAmount + (decimal)drBTrAmount - (decimal)crBalAmount - (decimal)crTrAmount;
                }
            }
            return Json(OpenBalanceAmount);
        }

        [Route("/api/global/getOpenSubBalanceById/{id}")]
        public async Task<JsonResult> getOpenSubBalance(int id)
        {
            var OpenBalanace = await openingBalanceService.GetOpeningBalancebyLedgerRelId(id);
            var Voucherdata = await voucherService.GetGetvoucherDetailByLedgerRelId(id);
            var drBalAmount = OpenBalanace.Where(x => x.transectionModeId == 1).Sum(x => x.amount);
            var crBalAmount = OpenBalanace.Where(x => x.transectionModeId == 2).Sum(x => x.amount);
            var drBTrAmount = Voucherdata.Where(x => x.transectionModeId == 1).Sum(x => x.amount);
            var crTrAmount = Voucherdata.Where(x => x.transectionModeId == 2).Sum(x => x.amount);
            var OpenBalanceAmount = (decimal)drBalAmount + (decimal)drBTrAmount - (decimal)crBalAmount - (decimal)crTrAmount;
            return Json(OpenBalanceAmount);
        }


        #endregion
    }
}

