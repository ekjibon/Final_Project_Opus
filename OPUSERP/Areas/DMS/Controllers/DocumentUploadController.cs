using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.DMS.Models;
using OPUSERP.Areas.MasterData.Models;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.DMS.Data.Entity;
using OPUSERP.DMS.Services.Document.Interfaces;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OPUSERP.Areas.DMS.Controllers
{
    [Area("DMS")]
    public class DocumentUploadController : Controller
    {
        private readonly IDocumentUploadService documentUploadService;
        private readonly IAttachmentCommentService attachmentCommentService;
        public readonly IERPModuleService iERPModuleService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public DocumentUploadController(IDocumentUploadService documentUploadService, IAttachmentCommentService attachmentCommentService, IERPModuleService iERPModuleService, IHostingEnvironment hostingEnvironment)
        {
            this.documentUploadService = documentUploadService;
            this.attachmentCommentService = attachmentCommentService;
            this.iERPModuleService = iERPModuleService;
            this._hostingEnvironment = hostingEnvironment;
        }

        #region Field Type

        public async Task<IActionResult> FieldType()
        {
            var model = new FieldTypeViewModel
            {
                //fLang = _lang.PerseLang("Employee/SpouseEN.json", "Employee/SpouseBN.json", Request.Cookies["lang"]),
                fieldTypes = await documentUploadService.GetFieldType()
            };
            return View(model);
        }

        // POST: FieldType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FieldType([FromForm] FieldTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fieldTypes = await documentUploadService.GetFieldType();
                //model.fLang = _lang.PerseLang("Employee/LicenseEN.json", "Employee/LicenseBN.json", Request.Cookies["lang"]);
                return View(model);
            }

            FieldType data = new FieldType
            {
                Id = Int32.Parse(model.fieldTypeId),
                fieldTypeName = model.fieldTypeName
            };

            await documentUploadService.SaveFieldType(data);
            return RedirectToAction(nameof(FieldType));
        }

        // Delete: Field Type        
        [HttpPost]
        public async Task<JsonResult> DeleteFieldTypeById(int Id)
        {
            await documentUploadService.DeleteFieldTypeById(Id);
            return Json(true);
        }
        #endregion

        #region Document Category
        public async Task<IActionResult> DocumentCategory()
        {
            var model = new DocumentCategoryViewModel
            {
                //fLang = _lang.PerseLang("Employee/SpouseEN.json", "Employee/SpouseBN.json", Request.Cookies["lang"]),
                documentCategories = await documentUploadService.GetDocumentCategory()
            };
            return View(model);
        }

        // POST: FieldType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DocumentCategory([FromForm] DocumentCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.documentCategories = await documentUploadService.GetDocumentCategory();
                //model.fLang = _lang.PerseLang("Employee/LicenseEN.json", "Employee/LicenseBN.json", Request.Cookies["lang"]);
                return View(model);
            }

            DocumentCategory data = new DocumentCategory
            {
                Id = Int32.Parse(model.documentCategoryId),
                categoryName = model.documentCategoryName
            };

            await documentUploadService.SaveDocumentCategory(data);
            return RedirectToAction(nameof(DocumentCategory));
        }

        // Delete: Document Category        

        [HttpPost]
        public async Task<JsonResult> DeleteDocumentCategoryById(int Id)
        {
            await documentUploadService.DeleteDocumentCategoryById(Id);
            return Json(true);
        }
        #endregion

        #region Field Setting
        public async Task<IActionResult> FieldSetting()
        {
            var model = new FieldSettingViewModel
            {
                //fLang = _lang.PerseLang("Employee/SpouseEN.json", "Employee/SpouseBN.json", Request.Cookies["lang"]),
                fieldTypes = await documentUploadService.GetFieldType(),
                documentCategories = await documentUploadService.GetDocumentCategory(),
                fieldSettings = await documentUploadService.GetFieldSettings()
            };
            return View(model);
        }

        // POST: Field Setting/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FieldSetting([FromForm] FieldSettingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.documentCategories = await documentUploadService.GetDocumentCategory();
                model.fieldTypes = await documentUploadService.GetFieldType();
                model.fieldSettings = await documentUploadService.GetFieldSettings();
                return View(model);
            }

            FieldSettings data = new FieldSettings
            {
                Id = model.fieldSettingId,
                fieldName = model.fieldName,
                fieldTypeId = model.fieldTypeId,
                documentCategoryId = model.documentCategoryId
            };

            await documentUploadService.SaveFieldSettings(data);
            return RedirectToAction(nameof(FieldSetting));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteFieldSettingsById(int Id)
        {
            await documentUploadService.DeleteFieldSettingsById(Id);
            return Json(true);
        }
        #endregion

        #region Document List

        public async Task<IActionResult> DocumentList()
        {
            DocumentSettingViewModel model = new DocumentSettingViewModel
            {
                documentMasters = await documentUploadService.GetAllDocumentMaster()
            };
            return View(model);

        }

        #endregion

        #region Document List Lead/Client/Company

        public async Task<IActionResult> DocumentListByCompany()
        {
            DocumentSettingViewModel model = new DocumentSettingViewModel
            {
                totalDocumentByLeadViewModels = await documentUploadService.GetTotalDocumentByLead()
            };
            return View(model);

        }

        [HttpGet]
        public async Task<IActionResult> GetDocumentMasterByLeadId(int leadId)
        {
            return Json(await documentUploadService.GetDocumentMasterByLeadId(leadId));
        }

        [HttpGet]
        public async Task<IActionResult> GetDocumentMasterByLeadIdBySP(int leadId)
        {
            return Json(await documentUploadService.GetDocumentMasterByLeadIdBySP(leadId, HttpContext.User.Identity.Name));
        }

        [HttpPost]
        public async Task<ActionResult> SaveAssignMasterDoc([FromForm] AssignDocumentMasterViewModel model)
        {
            try
            {
                await documentUploadService.DeleteAssignDocumentMasterByDocId(Convert.ToInt32(model.assigndocumentMasterId));

                if (model.hasEmployee == "Private")
                {
                    if (model.assigndocumentMasterId != 0)
                    {
                        if (model.ids != null)
                        {
                            for (var i = 0; i < model.ids.Length; i++)
                            {
                                AssignDocumentMaster assign = new AssignDocumentMaster
                                {
                                    employeeInfoId = model.ids[i],
                                    documentMasterId = model.assigndocumentMasterId,
                                    isViewed = 1,
                                    hasEmployee = model.hasEmployee
                                };
                                await documentUploadService.SaveAssignDocumentMaster(assign);
                            }
                        }
                    }
                }
                else
                {
                    AssignDocumentMaster assign = new AssignDocumentMaster
                    {
                        documentMasterId = model.assigndocumentMasterId,
                        isViewed = 1,
                        hasEmployee = model.hasEmployee
                    };
                    await documentUploadService.SaveAssignDocumentMaster(assign);
                }

                #region Doc Status Log                
                DocumentStatusLog dlog = new DocumentStatusLog
                {
                    Id = 0,
                    documentMasterId = Convert.ToInt32(model.assigndocumentMasterId),
                    documentStatus = "Assign Documents to " + model.hasEmployee + " "
                };
                await documentUploadService.SaveDocumentStatusLog(dlog);

                #endregion

                return RedirectToAction(nameof(DocumentListByCompany));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Document Master / Index

        public async Task<IActionResult> Index(int? Id)
        {
            var model = new DocumentSettingViewModel
            {
                documentCategories = await documentUploadService.GetDocumentCategory()
            };
            ViewBag.masterId = Id;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] DocumentSettingViewModel model)
        {
            var docMasterId = 0;

            if (model.headIdAll == null)
            {
                ModelState.AddModelError(string.Empty, "Have to Add minimum 1");
                model.documentMasterId = 0;
                return View(model);
            }

            DocumentMaster data = new DocumentMaster
            {
                Id = model.documentMasterId,
                documentCategoryId = Convert.ToInt32(model.documentCategoryId),
                leadsId = model.leadsId,
                employeeInfoId = model.employeeInfoId,
                documentName = model.documentName,
                documentNo = model.documentNo,
                docCreateDate = model.docCreateDate,
                docCreateBy = model.docCreateBy
            };

            docMasterId = await documentUploadService.SaveDocumentMaster(data);

            if (model.documentMasterId > 0)
            {
                await documentUploadService.DeleteDocumentDetailByMasterId(Convert.ToInt32(model.documentMasterId));
            }
            for (int i = 0; i < model.headIdAll.Length; i++)
            {
                DocumentDetail details = new DocumentDetail
                {
                    Id = 0,
                    documentMasterId = docMasterId,
                    fieldSettingId = model.headIdAll[i],
                    fileldValue = model.amountAll[i]
                };
                await documentUploadService.SaveDocumentDetail(details);
            }

            #region Doc Status Log
            string dstatus = "";
            if (model.documentMasterId == 0)
            {
                dstatus = "Create document";
            }
            else
            {
                dstatus = "Update document";
            }
            DocumentStatusLog dlog = new DocumentStatusLog
            {
                Id = 0,
                documentMasterId = docMasterId,
                documentStatus = dstatus
            };
            await documentUploadService.SaveDocumentStatusLog(dlog);

            #endregion



            return RedirectToAction(nameof(DocumentDetails), new { Id = docMasterId });
        }

        [HttpPost]
        public async Task<JsonResult> DeleteDocumentMasterById(int Id)
        {
            await documentUploadService.DeleteDocumentStatusLogByMasterId(Id);
            await documentUploadService.DeleteDocumentDetailByMasterId(Id);
            await documentUploadService.DeleteDocumentMasterById(Id);
            return Json(true);
        }

        [HttpGet]
        public async Task<IActionResult> GetDocumentMasterById(int id)
        {
            return Json(await documentUploadService.GetDocumentMasterById(id));
        }
        #endregion

        #region Document Details

        public async Task<IActionResult> DocumentDetails(int Id)
        {
            try
            {
                var module = await iERPModuleService.GetERPModuleByModuleName("Docs Management");
                int moduleId = module.Id;
                string userName = HttpContext.User.Identity.Name;

                var CommentInfo = await attachmentCommentService.GetCommentByActionTypeId(Id, "DMS", moduleId);
                if (CommentInfo == null)
                {
                    CommentInfo = new List<Comment>();
                }
                var photoInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(Id, "DMS", "Photo", moduleId);
                if (photoInfo == null)
                {
                    photoInfo = new List<DocumentPhotoAttachment>();
                }
                //var docInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(Id, "DMS", "Document", moduleId);
                //if (docInfo == null)
                //{
                //    docInfo = new List<DocumentPhotoAttachment>();
                //}
                var docInfo = await attachmentCommentService.GetDocumentAttachmentForDMSByActionId(Id, "DMS", "Document", moduleId, userName);
                if (docInfo == null)
                {
                    docInfo = new List<DocumentAccessViewModel>();
                }

                var statusLog = await documentUploadService.GetDocumentStatusLogByMasterId(Id);
                if (statusLog == null)
                {
                    statusLog = new List<DocumentStatusLog>();
                }


                var model = new DocumentSettingViewModel
                {
                    documentMaster = await documentUploadService.GetDocumentMasterById(Id),
                    //documentDetails = await documentUploadService.GetDocumentDetailByMasterId(Id),
                    comments = CommentInfo,
                    photoes = photoInfo,
                    documents = docInfo,
                    documentStatusLogs = statusLog,
                    documentCategories = await documentUploadService.GetDocumentCategory()
                };

                ViewBag.masterId = Id;
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> SaveAssign([FromForm] AssignDocumentViewModel model)
        {
            try
            {
                await documentUploadService.DeleteAssignDocumentByDocId(Convert.ToInt32(model.assigndocumentMasterId));

                if (model.hasEmployee == "Private")
                {
                    if (model.assigndocumentMasterId != 0)
                    {
                        if (model.ids != null)
                        {
                            for (var i = 0; i < model.ids.Length; i++)
                            {
                                AssignDocument assign = new AssignDocument
                                {
                                    employeeInfoId = model.ids[i],
                                    documentMasterId = model.assigndocumentMasterId,
                                    documentPhotoAttachmentId = model.documentPhotoAttachmentId,
                                    isViewed = 1,
                                    hasEmployee = model.hasEmployee
                                };
                                await documentUploadService.SaveAssignDocument(assign);
                            }
                        }
                    }
                }
                else
                {
                    AssignDocument assign = new AssignDocument
                    {                        
                        documentMasterId = model.assigndocumentMasterId,
                        documentPhotoAttachmentId = model.documentPhotoAttachmentId,
                        isViewed = 1,
                        hasEmployee = model.hasEmployee
                    };
                    await documentUploadService.SaveAssignDocument(assign);
                }

                #region Doc Status Log                
                DocumentStatusLog dlog = new DocumentStatusLog
                {
                    Id = 0,
                    documentMasterId = Convert.ToInt32(model.assigndocumentMasterId),
                    documentStatus = "Assign Documents to " + model.hasEmployee + " "
                };
                await documentUploadService.SaveDocumentStatusLog(dlog);

                #endregion

                return RedirectToAction("DocumentDetails", "DocumentUpload", new { id = model.assigndocumentMasterId, Area = "DMS" });
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
                var module = await iERPModuleService.GetERPModuleByModuleName("Docs Management");
                int moduleId = module.Id;

                Comment entityComment = new Comment
                {
                    Id = Convert.ToInt32(model.commentsId),
                    actionType = "DMS",
                    actionTypeId = model.actionTypeId,
                    title = model.title,
                    comment = model.comment,
                    moduleId = moduleId,
                    createdBy = HttpContext.User.Identity.Name,
                };
                int commentsId = await attachmentCommentService.SaveComment(entityComment);

                #region Doc Status Log                
                DocumentStatusLog dlog = new DocumentStatusLog
                {
                    Id = 0,
                    documentMasterId = Convert.ToInt32(model.actionTypeId),
                    documentStatus = "Add Comments"
                };
                await documentUploadService.SaveDocumentStatusLog(dlog);

                #endregion

                return RedirectToAction("DocumentDetails", "DocumentUpload", new { id = model.actionTypeId, Area = "DMS" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteComments(int Id, int actionId)
        {
            #region Doc Status Log                
            DocumentStatusLog dlog = new DocumentStatusLog
            {
                Id = 0,
                documentMasterId = actionId,
                documentStatus = "Delete Comments"
            };
            await documentUploadService.SaveDocumentStatusLog(dlog);

            #endregion

            await attachmentCommentService.DeleteCommentById(Id);
            return RedirectToAction("DocumentDetails", "DocumentUpload", new { id = actionId, Area = "DMS" });
        }

        [HttpPost]
        public async Task<IActionResult> SaveDoc([FromForm] DocumentAttachmentViewModel model, IFormFile docformFile)
        {
            try
            {
                var module = await iERPModuleService.GetERPModuleByModuleName("Docs Management");
                int moduleId = module.Id;

                if (docformFile != null)
                {
                    string userName = User.Identity.Name;
                    string documentType = "Document";
                    var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                    var fileName = ContentDispositionHeaderValue.Parse(docformFile.ContentDisposition).FileName;
                    string fileType = Path.GetExtension(fileName);
                    fileName = fileName.Contains("\\")
                        ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                        : fileName.Trim('"');

                    string NewFileName = model.actionTypeId + "_" + documentType + "_" + fileName;
                    string fileLocation = "/Upload/Photo/" + NewFileName;
                    var fullFilePath = Path.Combine(filesPath, NewFileName);

                    using (var stream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await docformFile.CopyToAsync(stream);
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
                        moduleId = moduleId,
                        createdBy = userName
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);

                }

                #region Doc Status Log                
                DocumentStatusLog dlog = new DocumentStatusLog
                {
                    Id = 0,
                    documentMasterId = model.actionTypeId,
                    documentStatus = "Attach Document"
                };
                await documentUploadService.SaveDocumentStatusLog(dlog);

                #endregion


                return RedirectToAction("DocumentDetails", "DocumentUpload", new { id = model.actionTypeId, Area = "DMS" });

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
                var module = await iERPModuleService.GetERPModuleByModuleName("Docs Management");
                int moduleId = module.Id;

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
                        moduleId = moduleId,
                        createdBy = userName
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);
                }

                #region Doc Status Log                
                DocumentStatusLog dlog = new DocumentStatusLog
                {
                    Id = 0,
                    documentMasterId = model.actionTypeId,
                    documentStatus = "Attach Photo"
                };
                await documentUploadService.SaveDocumentStatusLog(dlog);

                #endregion

                return RedirectToAction("DocumentDetails", "DocumentUpload", new { id = model.actionTypeId, Area = "DMS" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeletePhoto(int actionId, int photoId)
        {
            #region Doc Status Log                
            DocumentStatusLog dlog = new DocumentStatusLog
            {
                Id = 0,
                documentMasterId = actionId,
                documentStatus = "Delete Documents/Photo"
            };
            await documentUploadService.SaveDocumentStatusLog(dlog);

            #endregion

            var data = await attachmentCommentService.GetDocumentAttachmentById(photoId);
            var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";

            var fullFilePath = Path.Combine(filesPath, data.fileName);
            if (System.IO.File.Exists(fullFilePath))
                System.IO.File.Delete(fullFilePath);
            await attachmentCommentService.DeleteDocumentAttachmentById(photoId);
            return RedirectToAction("DocumentDetails", "DocumentUpload", new { id = actionId, Area = "DMS" });
        }

        #endregion

        #region API
        [Route("global/api/DocumentUploadController/GetDocumentDetailsByDocType/{categoryId}")]
        [HttpGet]
        public async Task<IActionResult> GetDocumentDetailsByDocType(int categoryId)
        {
            return Json(await documentUploadService.GetDocumentDetailsByDocType(categoryId));
        }

        [Route("global/api/DocumentUploadController/GetDocumentDetailByMasterId/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetDocumentDetailByMasterId(int id)
        {
            return Json(await documentUploadService.GetDocumentDetailByMasterId(id));
        }

        [Route("global/api/GetAssignDocumentByDocId/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetAssignDocumentByDocId(int Id)
        {
            return Json(await documentUploadService.GetAssignDocumentByDocId(Id));
        }

        [Route("global/api/GetAssignDocumentMasterByDocId/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetAssignDocumentMasterByDocId(int Id)
        {
            return Json(await documentUploadService.GetAssignDocumentMasterByDocId(Id));
        }
        #endregion
    }
}