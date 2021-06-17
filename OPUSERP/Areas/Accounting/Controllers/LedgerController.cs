using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using OPUSERP.Accounting.Services.AccountingSettings.Interfaces;
using OPUSERP.Areas.Accounting.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;
using OPUSERP.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using OPUSERP.Areas.MasterData.Models;
using DinkToPdf.Contracts;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.ERPService.AuthService.Interfaces;

namespace OPUSERP.Areas.Accounting.Controllers
{
    [Authorize]
    [Area("Accounting")]
    public class LedgerController : Controller
    {
        private readonly ILedgerService ledgerService;
        private readonly IAccountGroupService accountGroupService;
        private readonly ISpecialBranchUnitService specialBranchUnitService;
        private readonly IERPCompanyService companyService;
        private readonly IUserInfoes userInfoes;
        private readonly IAttachmentCommentService attachmentCommentService;
        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly string rootPath;
        private readonly MyPDF myPDF;
        public string FileName;

        public LedgerController(ILedgerService ledgerService, IAccountGroupService accountGroupService, IHostingEnvironment hostingEnvironment, IAttachmentCommentService attachmentCommentService, IConverter converter, ISpecialBranchUnitService specialBranchUnitService, IERPCompanyService companyService, IUserInfoes userInfoes)
        {
            this.ledgerService = ledgerService;
            this.accountGroupService = accountGroupService;
            this.specialBranchUnitService = specialBranchUnitService;
            this.companyService = companyService;
            this.userInfoes = userInfoes;
            this._hostingEnvironment = hostingEnvironment;
            this.attachmentCommentService = attachmentCommentService;

            myPDF = new MyPDF(hostingEnvironment, converter);

            rootPath = hostingEnvironment.ContentRootPath;
        }

        // GET: Ledger
        public async Task<IActionResult> Index(int id)
        {
            string username = HttpContext.User.Identity.Name;
            var userinfo = await userInfoes.GetUserInfoByUser(username);
            string userName = userinfo?.UserName;
            int? branchid = 0;
            if (userName == "biplab" || userName == "suza")
            {
                branchid = 0;
            }
            else
            {
                branchid = userinfo?.specialBranchUnitId;
            }              
            

            LedgerViewModel model = new LedgerViewModel
            {
                //ledgers = await ledgerService.GetLedger(),
                ledgers = await ledgerService.GetLedgerBySbuId(branchid),
                accountGroups = await accountGroupService.GetaccountGroup(),
                specialBranchUnits = await specialBranchUnitService.GetSpecialBranchUnit()
            };
            ViewBag.masterId = id;
            ViewBag.branchid = branchid;
            return View(model);
        }

        [Route("global/api/GetaccountGroupLedger")]
        [HttpGet]
        public async Task<IActionResult> GetaccountGroupLedger()
        {
            return Json(await accountGroupService.GetaccountGroup());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] LedgerViewModel model)
        {
            string username = HttpContext.User.Identity.Name;
            var userinfo = await userInfoes.GetUserInfoByUser(username);
            string userName = userinfo?.UserName;
            int? branchid = 0;
            if (userName == "biplab" || userName == "suza")
            {
                branchid = 0;
            }
            else
            {
                branchid = userinfo?.specialBranchUnitId;
            }

            var company = await companyService.GetAllCompany();
            int validation = await ledgerService.GetLedgerCheckL(model.ledgerId, model.accountName, Convert.ToInt32(model.specialBranchUnitId));

            if (!ModelState.IsValid || validation == 0)
            {
                //model.ledgers = await ledgerService.GetLedger();
                model.ledgers = await ledgerService.GetLedgerBySbuId(branchid);
                model.accountGroups = await accountGroupService.GetaccountGroup();
                model.specialBranchUnits = await specialBranchUnitService.GetSpecialBranchUnit();
                if (validation == 0)
                {
                    ModelState.AddModelError(string.Empty, "Ledger Account Name '" + model.accountName + "' already exists.");
                }
                //   return RedirectToAction("Index", "Ledger", new { id = 0, Area = "Accounting" });
                ViewBag.branchid = 0;
                return View(model);
            }


           

            int? haveSubLedger = 0;
            if (model.haveSubLedger == 1 && model.ledgerType == "General")
            {
                haveSubLedger = 1;
            }

            Ledger data = new Ledger
            {
                Id = model.ledgerId,
                groupId = model.groupId,
                accountName = model.accountName,
                accountCode = model.accountCode,
                aliasName = model.aliasName,
                companyId = company.FirstOrDefault().Id,
                isActive = 1,
                currencyId = 1,
                haveSubLedger = haveSubLedger,
                ledgerType=model.ledgerType,
                specialBranchUnitId=model.specialBranchUnitId,
                effectiveDate=model.effectiveDate
            };

            //return Json(data);

            int ledgerId = await ledgerService.Saveledger(data);

            if (model.ledgerId == 0)
            {
                LedgerRelation data1 = new LedgerRelation
                {
                    ledgerId = ledgerId
                };
                await ledgerService.SaveLedgerRelation(data1);
            }

            //return RedirectToAction(nameof(Index));
            return RedirectToAction("LedgerDetails", "Ledger", new { id = ledgerId, Area = "Accounting" });
        }

        [HttpPost]
        public async Task<JsonResult> DeleteLedger(int Id)
        {
            await ledgerService.DeleteledgerRelationByLedgerId(Id);
            await ledgerService.DeleteledgerById(Id);
            return Json(true);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await ledgerService.DeleteledgerById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
       

        public async Task<IActionResult> LedgerDetails(int id)
        {
            try
            {
                ViewBag.masterId = id;
                var CommentInfo = await attachmentCommentService.GetCommentByActionTypeId(id, "Ledger", 4);
                if (CommentInfo == null)
                {
                    CommentInfo = new List<Comment>();
                }
                var photoInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "Ledger", "photo", 4);
                if (photoInfo == null)
                {
                    photoInfo = new List<DocumentPhotoAttachment>();
                }
                var docInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "Ledger", "Document", 4);
                if (docInfo == null)
                {
                    docInfo = new List<DocumentPhotoAttachment>();
                }


                var model = new LedgerViewModel
                {
                    getLedgerDetailsById = await ledgerService.GetLedgerDetailsById(id),
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

        [HttpGet]
        public async Task<IActionResult> GetLedgerDetailsById(int ledgerId)
        {
            return Json(await ledgerService.GetLedgerDetailsById(ledgerId));
        }

        [HttpPost]
        public async Task<ActionResult> SaveComment([FromForm] CommentsViewModel model)
        {
            try
            {
                Comment entityComment = new Comment
                {
                    Id = Convert.ToInt32(model.commentsId),
                    actionType = "Ledger",
                    actionTypeId = model.actionTypeId,
                    title = model.title,
                    comment = model.comment,
                    moduleId = 4,
                };
                int commentsId = await attachmentCommentService.SaveComment(entityComment);
                return RedirectToAction("LedgerDetails", "Ledger", new { id = model.actionTypeId, Area = "Accounting" });

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
            return RedirectToAction("LedgerDetails", "Ledger", new { id = actionId, Area = "Accounting" });
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

                return RedirectToAction("LedgerDetails", "Ledger", new { id = model.actionTypeId, Area = "Accounting" });
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

                return RedirectToAction("LedgerDetails", "Ledger", new { id = model.actionTypeId, Area = "Accounting" });
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
            return RedirectToAction("LedgerDetails", "Ledger", new { id = actionId, Area = "Accounting" });
        }

        #region API Section
        [Route("global/api/LedgerCode/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LedgerCode(int Id)
        {
            return Json(await ledgerService.GetLedgerCodeByGorupId(Id));
        }

        [Route("global/api/GetLedgerWithSubLedger/")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetLedgerWithSubLedger()
        {
            return Json(await ledgerService.GetLedgerWithSubLedger());
        }

        [Route("global/api/LedgerController/GetLedgerWithSubLedgerBySbu/{sbuId}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetLedgerWithSubLedgerBySbu(int sbuId)
        {
            return Json(await ledgerService.GetLedgerWithSubLedgerBySbu(sbuId));
        }
        #endregion
    }
}