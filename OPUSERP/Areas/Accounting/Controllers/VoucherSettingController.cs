using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using OPUSERP.Accounting.Data.Entity.MasterData;
using OPUSERP.Accounting.Data.Entity.Voucher;
using OPUSERP.Accounting.Services.AccountingSettings.Interfaces;
using OPUSERP.Accounting.Services.MasterData.Interfaces;
using OPUSERP.Accounting.Services.Voucher.Interfaces;
using OPUSERP.Areas.Accounting.Models;
using OPUSERP.Areas.MasterData.Models;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.SCM.Services.PurchaseOrder.Interface;
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
    public class VoucherSettingController : Controller
    {
        private readonly IVoucherService VoucherService;
        private readonly ILedgerService ledgerService;
        private readonly IPurchaseOrderService purchaseOrderService;
        private readonly IGroupNatureService groupNatureService;
        private readonly IAccountGroupService accountGroupService;
        private readonly ICostCentreService costCentreService;
        private readonly IVoucherTypeService voucherTypeService;
        private readonly IAttachmentCommentService attachmentCommentService;
        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly string rootPath;
        private readonly MyPDF myPDF;
        public string FileName;
        public VoucherSettingController(IVoucherService VoucherService, ILedgerService ledgerService, IPurchaseOrderService purchaseOrderService, IGroupNatureService groupNatureService, IAccountGroupService accountGroupService, ICostCentreService costCentreService, IVoucherTypeService voucherTypeService, IHostingEnvironment hostingEnvironment, IAttachmentCommentService attachmentCommentService, IConverter converter)
        {
            this.VoucherService = VoucherService;
            this.ledgerService = ledgerService;
            this.purchaseOrderService = purchaseOrderService;
            this.groupNatureService = groupNatureService;
            this.accountGroupService = accountGroupService;
            this.costCentreService = costCentreService;
            this.voucherTypeService = voucherTypeService;
            this._hostingEnvironment = hostingEnvironment;
            this.attachmentCommentService = attachmentCommentService;

            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        #region Voucher Settings
        public async Task<IActionResult> Index()
        {
            VoucherSettingViewModel model = new VoucherSettingViewModel
            {
                voucherSettings = await VoucherService.GetVoucherSetting(),
                paymentModes = await purchaseOrderService.GetPaymentMode(),

            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] VoucherSettingViewModel model)
        {
            VoucherSetting data = new VoucherSetting
            {
                Id = model.voucherSettingId,
                voucherTypeId = model.voucherTypeId,
                bankAccountLedgerId = model.bankAccountLedgerId,
                cashAccountLedgerId = model.cashAccountLedgerId,
                paymentModeId = model.paymentModeId,
                companyId = 1

            };

            await VoucherService.SaveVoucherSetting(data);

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Account Group

        public async Task<IActionResult> AccountGroup(int id)
        {
            AccountGroupViewModel model = new AccountGroupViewModel
            {
                groupNatures = await groupNatureService.GetgroupNature(),
                accountGroups = await accountGroupService.GetaccountGroup(),

            };
            ViewBag.masterId = id;
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> GetAccountGroupDetailsById(int accountGroupId)
        {
            return Json(await accountGroupService.GetAccountGroupDetailsById(accountGroupId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AccountGroup([FromForm] AccountGroupViewModel model)
        {
            AccountGroup data = new AccountGroup
            {
                Id = model.accountGroupId,
                natureId = model.natureId,
                groupCode = model.groupCode,
                groupName = model.groupName,
                isCashBank = "",
                parentGroupId = model.parentGroupId
            };

            int accountGroupId = await accountGroupService.SaveaccountGroup(data);
            //return RedirectToAction(nameof(AccountGroup));
            return RedirectToAction("AccountGroupDetails", "VoucherSetting", new { id = accountGroupId, Area = "Accounting" });
        }

        [HttpPost]
        public async Task<JsonResult> DeleteaccountGroupById(int Id)
        {
            await accountGroupService.DeleteaccountGroupById(Id);
            return Json(true);
        }

        public async Task<IActionResult> AccountGroupDetails(int id)
        {
            try
            {
                ViewBag.masterId = id;
                var CommentInfo = await attachmentCommentService.GetCommentByActionTypeId(id, "AccountGroup", 4);
                if (CommentInfo == null)
                {
                    CommentInfo = new List<Comment>();
                }
                var photoInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "AccountGroup", "photo", 4);
                if (photoInfo == null)
                {
                    photoInfo = new List<DocumentPhotoAttachment>();
                }
                var docInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "AccountGroup", "Document", 4);
                if (docInfo == null)
                {
                    docInfo = new List<DocumentPhotoAttachment>();
                }


                var model = new AccountGroupViewModel
                {
                    getAccountGroupDetailsById = await accountGroupService.GetAccountGroupDetailsById(id),
                    comments = CommentInfo,
                    photoes = photoInfo,
                    documents = docInfo,
                };

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
                    actionType = "AccountGroup",
                    actionTypeId = model.actionTypeId,
                    title = model.title,
                    comment = model.comment,
                    moduleId = 4,
                };
                int commentsId = await attachmentCommentService.SaveComment(entityComment);
                return RedirectToAction("AccountGroupDetails", "VoucherSetting", new { id = model.actionTypeId, Area = "Accounting" });

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
            return RedirectToAction("AccountGroupDetails", "VoucherSetting", new { id = actionId, Area = "Accounting" });
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

                return RedirectToAction("AccountGroupDetails", "VoucherSetting", new { id = model.actionTypeId, Area = "Accounting" });
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

                return RedirectToAction("AccountGroupDetails", "VoucherSetting", new { id = model.actionTypeId, Area = "Accounting" });
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
            return RedirectToAction("AccountGroupDetails", "VoucherSetting", new { id = actionId, Area = "Accounting" });
        }

        #endregion

        #region Cost Centre

        public async Task<IActionResult> CostCentre()
        {
            CostCentreViewModel model = new CostCentreViewModel
            {
                costCentres = await costCentreService.GetCostCentres(),

            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CostCentre([FromForm] CostCentreViewModel model)
        {
            CostCentre data = new CostCentre
            {
                Id = model.costCentreId,
                centreName = model.centreName,
                aliesName = model.aliesName,
                centreCode=model.centreCode
            };

            await costCentreService.SavecostCentre(data);
            return RedirectToAction(nameof(CostCentre));
        }

        [HttpPost]
        public async Task<JsonResult> DeletecostCentresById(int Id)
        {
            await costCentreService.DeletecostCentresById(Id);
            return Json(true);
        }

        #endregion

        #region Auto Voucher Name

        public async Task<IActionResult> AutoVoucherName()
        {
            AutoVoucherNameViewModel model = new AutoVoucherNameViewModel
            {
                autoVoucherNames = await costCentreService.GetAllAutoVoucherName(),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AutoVoucherName([FromForm] AutoVoucherNameViewModel model)
        {
            AutoVoucherName data = new AutoVoucherName
            {
                Id = model.nameId,
                autoVoucherName = model.autoVoucherName,
                shortName = model.shortName
            };
            await costCentreService.SaveAutoVoucherName(data);
            return RedirectToAction(nameof(AutoVoucherName));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteAutoVoucherNameById(int Id)
        {
            await costCentreService.DeleteAutoVoucherNameById(Id);
            return Json(true);
        }

        #endregion

        #region Create Auto Voucher

        public async Task<IActionResult> CreateAutoVoucher()
        {
            AutoVoucherMasterViewModel model = new AutoVoucherMasterViewModel
            {
                autoVoucherNames = await costCentreService.GetAllAutoVoucherName(),
                voucherTypes = await voucherTypeService.GetVoucherType(),
                transectionModes = await voucherTypeService.GetTransectionMode(),
                autoVoucherMasters = await costCentreService.GetAllAutoVoucherMaster()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAutoVoucher([FromForm] AutoVoucherMasterViewModel model)
        {
            var periodCheck = await costCentreService.GetDuplicateAutoVoucherMaster(model.masterId, model.autoVoucherNameId);
            if (!ModelState.IsValid || periodCheck.Count() > 0)
            {
                model.autoVoucherNames = await costCentreService.GetAllAutoVoucherName();
                model.voucherTypes = await voucherTypeService.GetVoucherType();
                model.transectionModes = await voucherTypeService.GetTransectionMode();
                model.autoVoucherMasters = await costCentreService.GetAllAutoVoucherMaster();
                if (periodCheck.Count() > 0)
                {
                    ModelState.AddModelError(string.Empty, "Duplicate Voucher For!!! Already exists!!!");
                }
                return View(model);
            }

            AutoVoucherMaster data = new AutoVoucherMaster
            {
                Id = model.masterId,
                autoVoucherNameId = model.autoVoucherNameId,
                voucherTypeId = model.voucherTypeId,
                projectId = model.projectId,
                description = model.description
            };
            int vmasterId = await costCentreService.SaveAutoVoucherMaster(data);

            if (model.masterId > 0)
            {
                await costCentreService.DeleteAutoVoucherDetailByMasterId(Convert.ToInt32(model.masterId));
            }
            for (int i = 0; i < model.headIdAll.Length; i++)
            {
                AutoVoucherDetail details = new AutoVoucherDetail
                {
                    Id = 0,
                    autoVoucherMasterId = vmasterId,
                    transectionModeId = model.modeIdAll[i],
                    ledgerRelationId = model.headIdAll[i]
                };
                await costCentreService.SaveAutoVoucherDetail(details);
            }
            return RedirectToAction(nameof(CreateAutoVoucher));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteAutoVoucherDetailByMasterId(int Id)
        {
            await costCentreService.DeleteAutoVoucherDetailByMasterId(Id);
            await costCentreService.DeleteAutoVoucherMasterById(Id);
            return Json(true);
        }

        #endregion

        #region Upload Voucher

        public IActionResult UploadVoucher()
        {
            return View();
        }

        #endregion

        #region API

        [Route("/api/global/GetLedgerForVoucherSettingBank/")]
        public async Task<JsonResult> GetLedgerForVoucherSettingBank()
        {
            var ledgers = await ledgerService.GetLedgerForVoucherSettingBank();
            return Json(ledgers);
        }

        [Route("/api/global/GetLedgerForVoucherSettingCash/")]
        public async Task<JsonResult> GetLedgerForVoucherSettingCash()
        {
            var ledgers = await ledgerService.GetLedgerForVoucherSettingCash();
            return Json(ledgers);
        }

        [Route("global/api/VoucherSettingController/GetAutoVoucherDetailByMasterId/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetAutoVoucherDetailByMasterId(int id)
        {
            return Json(await costCentreService.GetAutoVoucherDetailByMasterId(id));
        }
        #endregion
    }
}