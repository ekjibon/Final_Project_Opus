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
    public class VoucherApproveController : Controller
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
        private readonly string rootPath;
        private readonly MyPDF myPDF;

        public string FileName;
        public VoucherApproveController(IHostingEnvironment hostingEnvironment, IOpeningBalanceService openingBalanceService, ICostCentreService costCentreService, ILedgerService ledgerService, ISalaryService salaryService, IERPCompanyService eRPCompanyService, IAttachmentCommentService attachmentCommentService, IUserInfoes userInfo, IVoucherService voucherService, IConverter converter)
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

            myPDF = new MyPDF(hostingEnvironment, converter);

            rootPath = hostingEnvironment.ContentRootPath;
        }

        #region NotApprovedVoucher

        [HttpGet]
        public async Task<IActionResult> NotApprovedVoucherList()
        {
            var model = new VoucherApproveLogViewModel
            {
                voucherNotApproveLogs = await voucherService.GetvoucherMasterDetailsByTypeId(0),
            };
            return View(model);
        }
        

        public async Task<IActionResult> PaymentApproveDetails(int id)
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

        public async Task<IActionResult> ReceiptApproveDetails(int Id)
        {
            try
            {
                ViewBag.masterId = Id;
                var CommentInfo = await attachmentCommentService.GetCommentByActionTypeId(Id, "Voucher", 4);
                if (CommentInfo == null)
                {
                    CommentInfo = new List<Comment>();
                }
                var photoInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(Id, "Voucher", "photo", 4);
                if (photoInfo == null)
                {
                    photoInfo = new List<DocumentPhotoAttachment>();
                }
                var docInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(Id, "Voucher", "Document", 4);
                if (docInfo == null)
                {
                    docInfo = new List<DocumentPhotoAttachment>();
                }
                var costInf = await voucherService.GetCostCentreAllocationsByDetailId(Id);
                if (costInf == null)
                {
                    costInf = new List<CostCentreAllocation>();
                }
                var model = new VoucherMasterViewModel
                {
                    GetGetvoucherMasterById = await voucherService.GetGetvoucherMasterById(Id),
                    VoucherDetails = await voucherService.GetvoucherDetailByMasterId(Id),
                    GetVoucherDetailsByMasterId = await voucherService.GetvoucherDetailByMasterId(Id),
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

        public async Task<IActionResult> JournalApproveDetails(int Id)
        {
            try
            {
                ViewBag.masterId = Id;
                var CommentInfo = await attachmentCommentService.GetCommentByActionTypeId(Id, "Voucher", 4);
                if (CommentInfo == null)
                {
                    CommentInfo = new List<Comment>();
                }
                var photoInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(Id, "Voucher", "photo", 4);
                if (photoInfo == null)
                {
                    photoInfo = new List<DocumentPhotoAttachment>();
                }
                var docInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(Id, "Voucher", "Document", 4);
                if (docInfo == null)
                {
                    docInfo = new List<DocumentPhotoAttachment>();
                }
                var costInf = await voucherService.GetCostCentreAllocationsByDetailId(Id);
                if (costInf == null)
                {
                    costInf = new List<CostCentreAllocation>();
                }

                var model = new VoucherMasterViewModel
                {
                    GetGetvoucherMasterById = await voucherService.GetGetvoucherMasterByJournalId(Id),
                    VoucherDetails = await voucherService.GetvoucherDetailByMasterId(Id),
                    GetVoucherDetailsByMasterId = await voucherService.GetvoucherDetailByMasterId(Id),
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

        public async Task<IActionResult> ContraApproveDetails(int Id)
        {
            try
            {
                ViewBag.masterId = Id;
                var CommentInfo = await attachmentCommentService.GetCommentByActionTypeId(Id, "Voucher", 4);
                if (CommentInfo == null)
                {
                    CommentInfo = new List<Comment>();
                }
                var photoInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(Id, "Voucher", "photo", 4);
                if (photoInfo == null)
                {
                    photoInfo = new List<DocumentPhotoAttachment>();
                }
                var docInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(Id, "Voucher", "Document", 4);
                if (docInfo == null)
                {
                    docInfo = new List<DocumentPhotoAttachment>();
                }

                var model = new VoucherMasterViewModel
                {
                    GetGetvoucherMasterById = await voucherService.GetGetvoucherMasterById(Id),
                    VoucherDetails = await voucherService.GetvoucherDetailByMasterId(Id),
                    GetVoucherDetailsByMasterId = await voucherService.GetvoucherDetailByMasterId(Id),
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
        public async Task<JsonResult> SaveVoucherApprove([FromForm] VoucherApproveLogViewModel model)
        {
            try
            {
                int approveId = 0;

                VoucherApproveLog data = new VoucherApproveLog
                {
                    Id = 0,
                    voucherMasterId = Convert.ToInt32(model.voucherMasterId),
                    remarks = model.remarks,
                    statusId = 2
                };

                approveId = await voucherService.SaveVoucherApproveLog(data);

                var masterId = await voucherService.UpdateVoucherMasterStatus(model.voucherMasterId, 2, HttpContext.User.Identity.Name);


                return Json(approveId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Voucher Delete

        [HttpGet]
        public IActionResult NotDeletedVoucherList()
        {
            var model = new VoucherApproveLogViewModel
            {
                //voucherNotApproveLogs = await voucherService.GetvoucherMasterDetailsByTypeId(0),
            };
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> GetVoucherListForDelete(string FDate, string TDate, string voucherNo)
        {
            if(voucherNo==null)
            {
                voucherNo = "";
            }
            return Json(await voucherService.GetVoucherListForDelete(FDate, TDate, voucherNo));
        }

        public async Task<IActionResult> PaymentDeleteDetails(int id)
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

        public async Task<IActionResult> ReceiptDeleteDetails(int Id)
        {
            try
            {
                ViewBag.masterId = Id;
                var CommentInfo = await attachmentCommentService.GetCommentByActionTypeId(Id, "Voucher", 4);
                if (CommentInfo == null)
                {
                    CommentInfo = new List<Comment>();
                }
                var photoInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(Id, "Voucher", "photo", 4);
                if (photoInfo == null)
                {
                    photoInfo = new List<DocumentPhotoAttachment>();
                }
                var docInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(Id, "Voucher", "Document", 4);
                if (docInfo == null)
                {
                    docInfo = new List<DocumentPhotoAttachment>();
                }
                var costInf = await voucherService.GetCostCentreAllocationsByDetailId(Id);
                if (costInf == null)
                {
                    costInf = new List<CostCentreAllocation>();
                }
                var model = new VoucherMasterViewModel
                {
                    GetGetvoucherMasterById = await voucherService.GetGetvoucherMasterById(Id),
                    VoucherDetails = await voucherService.GetvoucherDetailByMasterId(Id),
                    GetVoucherDetailsByMasterId = await voucherService.GetvoucherDetailByMasterId(Id),
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

        public async Task<IActionResult> JournalDeleteDetails(int Id)
        {
            try
            {
                ViewBag.masterId = Id;
                var CommentInfo = await attachmentCommentService.GetCommentByActionTypeId(Id, "Voucher", 4);
                if (CommentInfo == null)
                {
                    CommentInfo = new List<Comment>();
                }
                var photoInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(Id, "Voucher", "photo", 4);
                if (photoInfo == null)
                {
                    photoInfo = new List<DocumentPhotoAttachment>();
                }
                var docInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(Id, "Voucher", "Document", 4);
                if (docInfo == null)
                {
                    docInfo = new List<DocumentPhotoAttachment>();
                }
                var costInf = await voucherService.GetCostCentreAllocationsByDetailId(Id);
                if (costInf == null)
                {
                    costInf = new List<CostCentreAllocation>();
                }

                var model = new VoucherMasterViewModel
                {
                    GetGetvoucherMasterById = await voucherService.GetGetvoucherMasterByJournalId(Id),
                    VoucherDetails = await voucherService.GetvoucherDetailByMasterId(Id),
                    GetVoucherDetailsByMasterId = await voucherService.GetvoucherDetailByMasterId(Id),
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

        public async Task<IActionResult> ContraDeleteDetails(int Id)
        {
            try
            {
                ViewBag.masterId = Id;
                var CommentInfo = await attachmentCommentService.GetCommentByActionTypeId(Id, "Voucher", 4);
                if (CommentInfo == null)
                {
                    CommentInfo = new List<Comment>();
                }
                var photoInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(Id, "Voucher", "photo", 4);
                if (photoInfo == null)
                {
                    photoInfo = new List<DocumentPhotoAttachment>();
                }
                var docInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(Id, "Voucher", "Document", 4);
                if (docInfo == null)
                {
                    docInfo = new List<DocumentPhotoAttachment>();
                }

                var model = new VoucherMasterViewModel
                {
                    GetGetvoucherMasterById = await voucherService.GetGetvoucherMasterById(Id),
                    VoucherDetails = await voucherService.GetvoucherDetailByMasterId(Id),
                    GetVoucherDetailsByMasterId = await voucherService.GetvoucherDetailByMasterId(Id),
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
        public async Task<JsonResult> SaveVoucherDelete([FromForm] VoucherApproveLogViewModel model)
        {
            try
            {
                int approveId = 0;

                var masterId = await voucherService.DeleteVoucher(model.voucherMasterId, model.remarks, HttpContext.User.Identity.Name);


                return Json(approveId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Voucher Re-Back

        [HttpGet]
        public  IActionResult ApprovedVoucherListReBack()
        {
           
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetVoucherListForReBack(string FDate, string TDate, string voucherNo)
        {
            if (voucherNo == null)
            {
                voucherNo = "";
            }
            return Json(await voucherService.GetVoucherListForReBack(FDate, TDate, voucherNo));
        }

        public async Task<IActionResult> PaymentReBackDetails(int id)
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

        public async Task<IActionResult> ReceiptReBackDetails(int Id)
        {
            try
            {
                ViewBag.masterId = Id;
                var CommentInfo = await attachmentCommentService.GetCommentByActionTypeId(Id, "Voucher", 4);
                if (CommentInfo == null)
                {
                    CommentInfo = new List<Comment>();
                }
                var photoInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(Id, "Voucher", "photo", 4);
                if (photoInfo == null)
                {
                    photoInfo = new List<DocumentPhotoAttachment>();
                }
                var docInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(Id, "Voucher", "Document", 4);
                if (docInfo == null)
                {
                    docInfo = new List<DocumentPhotoAttachment>();
                }
                var costInf = await voucherService.GetCostCentreAllocationsByDetailId(Id);
                if (costInf == null)
                {
                    costInf = new List<CostCentreAllocation>();
                }
                var model = new VoucherMasterViewModel
                {
                    GetGetvoucherMasterById = await voucherService.GetGetvoucherMasterById(Id),
                    VoucherDetails = await voucherService.GetvoucherDetailByMasterId(Id),
                    GetVoucherDetailsByMasterId = await voucherService.GetvoucherDetailByMasterId(Id),
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

        public async Task<IActionResult> JournalReBackDetails(int Id)
        {
            try
            {
                ViewBag.masterId = Id;
                var CommentInfo = await attachmentCommentService.GetCommentByActionTypeId(Id, "Voucher", 4);
                if (CommentInfo == null)
                {
                    CommentInfo = new List<Comment>();
                }
                var photoInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(Id, "Voucher", "photo", 4);
                if (photoInfo == null)
                {
                    photoInfo = new List<DocumentPhotoAttachment>();
                }
                var docInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(Id, "Voucher", "Document", 4);
                if (docInfo == null)
                {
                    docInfo = new List<DocumentPhotoAttachment>();
                }
                var costInf = await voucherService.GetCostCentreAllocationsByDetailId(Id);
                if (costInf == null)
                {
                    costInf = new List<CostCentreAllocation>();
                }

                var model = new VoucherMasterViewModel
                {
                    GetGetvoucherMasterById = await voucherService.GetGetvoucherMasterByJournalId(Id),
                    VoucherDetails = await voucherService.GetvoucherDetailByMasterId(Id),
                    GetVoucherDetailsByMasterId = await voucherService.GetvoucherDetailByMasterId(Id),
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

        public async Task<IActionResult> ContraReBackDetails(int Id)
        {
            try
            {
                ViewBag.masterId = Id;
                var CommentInfo = await attachmentCommentService.GetCommentByActionTypeId(Id, "Voucher", 4);
                if (CommentInfo == null)
                {
                    CommentInfo = new List<Comment>();
                }
                var photoInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(Id, "Voucher", "photo", 4);
                if (photoInfo == null)
                {
                    photoInfo = new List<DocumentPhotoAttachment>();
                }
                var docInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(Id, "Voucher", "Document", 4);
                if (docInfo == null)
                {
                    docInfo = new List<DocumentPhotoAttachment>();
                }

                var model = new VoucherMasterViewModel
                {
                    GetGetvoucherMasterById = await voucherService.GetGetvoucherMasterById(Id),
                    VoucherDetails = await voucherService.GetvoucherDetailByMasterId(Id),
                    GetVoucherDetailsByMasterId = await voucherService.GetvoucherDetailByMasterId(Id),
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
        public async Task<JsonResult> SaveVoucherReBack([FromForm] VoucherApproveLogViewModel model)
        {
            try
            {
                int approveId = 0;

                VoucherApproveLog data = new VoucherApproveLog
                {
                    Id = 0,
                    voucherMasterId = Convert.ToInt32(model.voucherMasterId),
                    remarks = model.remarks,
                    statusId = 5
                };

                approveId = await voucherService.SaveVoucherApproveLog(data);

                var masterId = await voucherService.UpdateVoucherMasterStatus(model.voucherMasterId, 1, HttpContext.User.Identity.Name);


                return Json(approveId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region ApprovedVoucher

        [HttpGet]
        public async Task<IActionResult> ApprovedVoucherList()
        {
            var model = new VoucherApproveLogViewModel
            {
                voucherNotApproveLogs = await voucherService.GetvoucherMasterDetailsByTypeId(0),
            };
            return View(model);
        }

        #endregion

        #region Reject Voucher

        [HttpGet]
        public async Task<IActionResult> RejectVoucherList()
        {
            var model = new VoucherApproveLogViewModel
            {
                voucherNotApproveLogs = await voucherService.GetvoucherMasterDetailsByTypeId(0),
            };
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> RejectVoucherApprove([FromForm] VoucherApproveLogViewModel model)
        {
            try
            {
                int approveId = 0;

                VoucherApproveLog data = new VoucherApproveLog
                {
                    Id = 0,
                    voucherMasterId = Convert.ToInt32(model.voucherMasterId),
                    remarks = model.remarks,
                    statusId = 3
                };

                approveId = await voucherService.SaveVoucherApproveLog(data);

                var masterId = await voucherService.UpdateVoucherMasterStatus(model.voucherMasterId, 3, HttpContext.User.Identity.Name);


                return Json(approveId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Voucher Delete

        public IActionResult ChangeVoucherNo()
        {            
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetVoucherListByVoucherNo(string voucherNo)
        {            
            return Json(await voucherService.GetVoucherListByVoucherNo(voucherNo));
        }

        [HttpPost]
        public async Task<JsonResult> UpdateVoucherNo([FromForm] VoucherApproveLogViewModel model)
        {
            try
            {
                var masterId = await voucherService.UpdateVoucherNo(model.voucherMasterId, model.remarks);
                return Json(masterId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}