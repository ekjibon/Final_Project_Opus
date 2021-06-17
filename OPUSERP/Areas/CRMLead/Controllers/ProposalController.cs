using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.CRMLead.Models;
using OPUSERP.Areas.MasterData.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.Proposal;
using OPUSERP.CRM.Services.Lead.Interfaces;
using OPUSERP.CRM.Services.Proposal.Interfaces;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;

namespace OPUSERP.Areas.CRMLead.Controllers
{
    [Area("CRMLead")]
    public class ProposalController : Controller
    {
        private readonly IProposalService proposalService;
        private readonly ILeadsService leadsService;
        private readonly IAttachmentCommentService attachmentCommentService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ProposalController(IProposalService proposalService, ILeadsService leadsService, IAttachmentCommentService attachmentCommentService, IHostingEnvironment hostingEnvironment)
        {
            this.proposalService = proposalService;
            this.leadsService = leadsService;
            this.attachmentCommentService = attachmentCommentService;
            this._hostingEnvironment = hostingEnvironment;
        }

        #region  Proposal List

        public async Task<IActionResult> ProposalList()
        {
            ProposalMasterViewModel model = new ProposalMasterViewModel()
            {
                proposalMasters = await proposalService.GetAllProposalMaster()
            };
            return View(model);
        }
        #endregion

        #region Proposal Master

        public async Task<IActionResult> Index(int? id, string leadName)
        {
            ProposalMasterViewModel model = new ProposalMasterViewModel()
            {
                proposalMasters = await proposalService.GetProposalMasterByLeadId(Convert.ToInt32(id)),
                proposalTypes = await proposalService.GetAllProposalType(),
                products = await proposalService.GetAllProduct(),
                proposalParticulars = await proposalService.GetAllProposalParticulars(),
                proposalStatuses = await proposalService.GetAllProposalStatus()
            };
            ViewBag.leadName = leadName;
            ViewBag.leadId = id;
            return View(model);
        }

        public async Task<IActionResult> IndexPersonal(int? id, string leadName)
        {
            ProposalMasterViewModel model = new ProposalMasterViewModel()
            {
                proposalMasters = await proposalService.GetProposalMasterByLeadId(Convert.ToInt32(id)),
                proposalTypes = await proposalService.GetAllProposalType(),
                products = await proposalService.GetAllProduct(),
                proposalParticulars = await proposalService.GetAllProposalParticulars(),
                proposalStatuses = await proposalService.GetAllProposalStatus()
            };
            ViewBag.leadName = leadName;
            ViewBag.leadId = id;
            return View(model);
        }

        // POST: Proposal Master/save/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ProposalMasterViewModel model)
        {
            var masterId = 0;
            if (!ModelState.IsValid)
            {
                model.proposalMasters = await proposalService.GetProposalMasterByLeadId(Convert.ToInt32(model.leadsId));
                model.proposalTypes = await proposalService.GetAllProposalType();
                //ViewBag.leadName = model.leadName;
                //ViewBag.leadId = Convert.ToInt32(model.leadsId);
                return View(model);
            }

            ProposalMaster data = new ProposalMaster
            {
                Id = model.proposalMasterId,
                proposalTypeId = model.proposalTypeId,
                leadsId = model.leadsId,
                proposalNo = model.proposalNo,
                proposalDate = model.proposalDate,
                proposalStatusId = model.proposalStatusId
            };

            masterId = await proposalService.SaveProposalMaster(data);
            if (model.productList != null)
            {
                await proposalService.DeleteRelProductProposalByMasterId(masterId);
                if (model.productList.Count() > 0)
                {
                    for (int i = 0; i < model.productList.Count(); i++)
                    {
                        RelProductProposal dataadd = new RelProductProposal
                        {
                            Id = 0,
                            proposalMasterId = masterId,
                            productId = (int)model.productList[i]
                        };
                        await proposalService.SaveRelProductProposal(dataadd);
                    }
                }
            }

            if (model.proposalParticularsId != null)
            {
                if (model.proposalParticularsId.Length > 0)
                {
                    await proposalService.DeleteProposalDetailByMasterId(masterId);
                    for (int i = 0; i < model.proposalParticularsId.Length; i++)
                    {
                        ProposalDetail proposalDetail = new ProposalDetail();
                        proposalDetail.Id = 0;
                        proposalDetail.proposalMasterId = masterId;
                        proposalDetail.proposalParticularsId = model.proposalParticularsId[i];
                        proposalDetail.particularsValue = model.particularsValue[i];

                        await proposalService.SaveProposalDetail(proposalDetail);
                        proposalDetail = new ProposalDetail();
                    }
                }
            }

            #region Save History
            string actDetailss = string.Empty;
            if (model.proposalMasterId == 0)
            {
                actDetailss = "Proposal No: " + model.proposalNo + " is Created.";
            }
            else
            {
                actDetailss = "Proposal No: " + model.proposalNo + " is Updated.";
            }

            LeadsHistory leadsData = new LeadsHistory
            {
                Id = 0,
                leadsId = model.leadsId,
                actionName = "Proposal",
                actionDetails = actDetailss
            };
            await leadsService.SaveLeadsHistory(leadsData);
            #endregion

            return RedirectToAction(nameof(Index), new
            {
                id = Convert.ToInt32(model.leadsId),
                leadName = model.leadName
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetMasterProposalById(int masterId)
        {
            return Json(await proposalService.GetProposalMasterById(masterId));
        }

        [HttpGet]
        public async Task<IActionResult> GetProductProposalByMasterId(int masterId)
        {
            return Json(await proposalService.GetAllProductProposalByMasterId(masterId));
        }

        [HttpGet]
        public async Task<IActionResult> GetProposalDetailByMasterId(int masterId)
        {
            var data = await proposalService.GetProposalDetailByMasterId(masterId);
            return Json(data);
        }

        #endregion

        #region Proposal Details

        public async Task<IActionResult> ProposalDetails(int Id, int? leadid, string leadName)
        {
            try
            {
                ViewBag.masterId = Id;
                var CommentInfo = await attachmentCommentService.GetCommentByActionTypeId(Id, "Proposal", 2);
                if (CommentInfo == null)
                {
                    CommentInfo = new List<Comment>();
                }
                var photoInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(Id, "Proposal", "photo", 2);
                if (photoInfo == null)
                {
                    photoInfo = new List<DocumentPhotoAttachment>();
                }
                var docInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(Id, "Proposal", "Document", 2);
                if (docInfo == null)
                {
                    docInfo = new List<DocumentPhotoAttachment>();
                }

                var model = new ProposalMasterViewModel
                {
                    GetProposalMasterDetailsById = await proposalService.GetProposalMasterById(Id),
                    relProductProposals = await proposalService.GetAllProductProposalByMasterId(Id),
                    proposalDetails = await proposalService.GetProposalDetailByMasterId(Id),
                    comments = CommentInfo,
                    photoes = photoInfo,
                    documents = docInfo,
                };

                ViewBag.leadName = leadName;
                ViewBag.leadId = leadid;
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
                //ViewBag.leadName = model.leadName;
                //ViewBag.leadId = Convert.ToInt32(model.leadsId);

                Comment entityComment = new Comment
                {
                    Id = Convert.ToInt32(model.commentsId),
                    actionType = "Proposal",
                    actionTypeId = model.actionTypeId,
                    title = model.title,
                    comment = model.comment,
                    moduleId = 2,
                    createdBy = HttpContext.User.Identity.Name,
                };
                int commentsId = await attachmentCommentService.SaveComment(entityComment);

                #region History Save               

                LeadsHistory leadsData = new LeadsHistory
                {
                    Id = 0,
                    leadsId = model.leadsId,
                    actionName = "Proposal",
                    actionDetails = "Comments is Added."
                };
                await leadsService.SaveLeadsHistory(leadsData);

                #endregion

                return RedirectToAction("ProposalDetails", "Proposal", new { id = model.actionTypeId, leadId = Convert.ToInt32(model.leadsId), leadName = model.leadName, Area = "CRMLead" });
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeleteComments(int Id, int actionId,int leadId,string leadName)
        {
            #region History Save 
            LeadsHistory leadsData = new LeadsHistory
            {
                Id = 0,
                leadsId = leadId,
                actionName = "Proposal",
                actionDetails = "Comments is Deleted."
            };
            await leadsService.SaveLeadsHistory(leadsData);
            #endregion

            await attachmentCommentService.DeleteCommentById(Id);
            return RedirectToAction("ProposalDetails", "Proposal", new { id = actionId, leadId = Convert.ToInt32(leadId), leadName= leadName, Area = "CRMLead" });
        }

        [HttpPost]
        public async Task<IActionResult> SaveDoc([FromForm] DocumentAttachmentViewModel model, IFormFile docformFile)
        {
            try
            {
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
                        moduleId = 2,
                        createdBy = userName
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);

                    #region History Save 
                    LeadsHistory leadsData = new LeadsHistory
                    {
                        Id = 0,
                        leadsId = model.leadsId,
                        actionName = "Proposal",
                        actionDetails = "Document "+ NewFileName + " is Uploaded."
                    };
                    await leadsService.SaveLeadsHistory(leadsData);
                    #endregion
                }



                return RedirectToAction("ProposalDetails", "Proposal", new { id = model.actionTypeId, leadId = Convert.ToInt32(model.leadsId), leadName = model.leadName, Area = "CRMLead" });

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
                        moduleId = 2,
                        createdBy = userName
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);

                    #region History Save 
                    LeadsHistory leadsData = new LeadsHistory
                    {
                        Id = 0,
                        leadsId = model.leadsId,
                        actionName = "Proposal",
                        actionDetails = "Photo " + NewFileName + " is Uploaded."
                    };
                    await leadsService.SaveLeadsHistory(leadsData);
                    #endregion
                }

                return RedirectToAction("ProposalDetails", "Proposal", new { id = model.actionTypeId, leadId = Convert.ToInt32(model.leadsId), leadName = model.leadName, Area = "CRMLead" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeletePhoto(int actionId, int photoId,int leadId, string leadName)
        {
            #region History Save 
            LeadsHistory leadsData = new LeadsHistory
            {
                Id = 0,
                leadsId = leadId,
                actionName = "Proposal",
                actionDetails = "Documents/Photo is Deleted."
            };
            await leadsService.SaveLeadsHistory(leadsData);
            #endregion

            var data = await attachmentCommentService.GetDocumentAttachmentById(photoId);
            var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";

            var fullFilePath = Path.Combine(filesPath, data.fileName);
            if (System.IO.File.Exists(fullFilePath))
                System.IO.File.Delete(fullFilePath);
            await attachmentCommentService.DeleteDocumentAttachmentById(photoId);
            return RedirectToAction("ProposalDetails", "Proposal", new { id = actionId, leadId = Convert.ToInt32(leadId), leadName= leadName, Area = "CRMLead" });
        }

        #endregion

        #region Client Proposal Master

        public async Task<IActionResult> ClientProposal(int? id, string leadName)
        {
            ProposalMasterViewModel model = new ProposalMasterViewModel()
            {
                proposalMasters = await proposalService.GetProposalMasterByLeadId(Convert.ToInt32(id)),
                proposalTypes = await proposalService.GetAllProposalType(),
                products = await proposalService.GetAllProduct(),
                proposalParticulars = await proposalService.GetAllProposalParticulars(),
                proposalStatuses = await proposalService.GetAllProposalStatus()
            };
            ViewBag.leadName = leadName;
            ViewBag.leadId = id;
            return View(model);
        }

        // POST: Proposal Master/save/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClientProposal([FromForm] ProposalMasterViewModel model)
        {
            var masterId = 0;
            if (!ModelState.IsValid)
            {
                model.proposalMasters = await proposalService.GetProposalMasterByLeadId(Convert.ToInt32(model.leadsId));
                model.proposalTypes = await proposalService.GetAllProposalType();
                //ViewBag.leadName = model.leadName;
                //ViewBag.leadId = Convert.ToInt32(model.leadsId);
                return View(model);
            }

            ProposalMaster data = new ProposalMaster
            {
                Id = model.proposalMasterId,
                proposalTypeId = model.proposalTypeId,
                leadsId = model.leadsId,
                proposalNo = model.proposalNo,
                proposalDate = model.proposalDate,
                proposalStatusId = model.proposalStatusId
            };

            masterId = await proposalService.SaveProposalMaster(data);
            if (model.productList != null)
            {
                await proposalService.DeleteRelProductProposalByMasterId(masterId);
                if (model.productList.Count() > 0)
                {
                    for (int i = 0; i < model.productList.Count(); i++)
                    {
                        RelProductProposal dataadd = new RelProductProposal
                        {
                            Id = 0,
                            proposalMasterId = masterId,
                            productId = (int)model.productList[i]
                        };
                        await proposalService.SaveRelProductProposal(dataadd);
                    }
                }
            }

            if (model.proposalParticularsId != null)
            {
                if (model.proposalParticularsId.Length > 0)
                {
                    await proposalService.DeleteProposalDetailByMasterId(masterId);
                    for (int i = 0; i < model.proposalParticularsId.Length; i++)
                    {
                        ProposalDetail proposalDetail = new ProposalDetail();
                        proposalDetail.Id = 0;
                        proposalDetail.proposalMasterId = masterId;
                        proposalDetail.proposalParticularsId = model.proposalParticularsId[i];
                        proposalDetail.particularsValue = model.particularsValue[i];

                        await proposalService.SaveProposalDetail(proposalDetail);
                        proposalDetail = new ProposalDetail();
                    }
                }
            }

            #region Save History
            string actDetailss = string.Empty;
            if (model.proposalMasterId == 0)
            {
                actDetailss = "Proposal No: "+ model.proposalNo + " is Created.";
            }
            else
            {
                actDetailss = "Proposal No: " + model.proposalNo + " is Updated.";
            }

            LeadsHistory leadsData = new LeadsHistory
            {
                Id = 0,
                leadsId = model.leadsId,
                actionName = "Proposal",
                actionDetails = actDetailss
            };
            await leadsService.SaveLeadsHistory(leadsData);
            #endregion

            return RedirectToAction(nameof(ClientProposal), new
            {
                id = Convert.ToInt32(model.leadsId),
                leadName = model.leadName
            });
        }

        #endregion

        #region Client Proposal Details

        public async Task<IActionResult> ClientProposalDetails(int Id, int? leadid, string leadName)
        {
            try
            {
                ViewBag.masterId = Id;
                var CommentInfo = await attachmentCommentService.GetCommentByActionTypeId(Id, "Proposal", 2);
                if (CommentInfo == null)
                {
                    CommentInfo = new List<Comment>();
                }
                var photoInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(Id, "Proposal", "photo", 2);
                if (photoInfo == null)
                {
                    photoInfo = new List<DocumentPhotoAttachment>();
                }
                var docInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(Id, "Proposal", "Document", 2);
                if (docInfo == null)
                {
                    docInfo = new List<DocumentPhotoAttachment>();
                }

                var model = new ProposalMasterViewModel
                {
                    GetProposalMasterDetailsById = await proposalService.GetProposalMasterById(Id),
                    relProductProposals = await proposalService.GetAllProductProposalByMasterId(Id),
                    proposalDetails = await proposalService.GetProposalDetailByMasterId(Id),
                    comments = CommentInfo,
                    photoes = photoInfo,
                    documents = docInfo,
                };

                ViewBag.leadName = leadName;
                ViewBag.leadId = leadid;
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> SaveCommentClient([FromForm] CommentsViewModel model)
        {
            try
            {
                Comment entityComment = new Comment
                {
                    Id = Convert.ToInt32(model.commentsId),
                    actionType = "Proposal",
                    actionTypeId = model.actionTypeId,
                    title = model.title,
                    comment = model.comment,
                    moduleId = 2,
                    createdBy = HttpContext.User.Identity.Name,
                };
                int commentsId = await attachmentCommentService.SaveComment(entityComment);

                #region History Save               

                LeadsHistory leadsData = new LeadsHistory
                {
                    Id = 0,
                    leadsId = model.leadsId,
                    actionName = "Proposal",
                    actionDetails = "Comments is Added."
                };
                await leadsService.SaveLeadsHistory(leadsData);

                #endregion

                return RedirectToAction("ClientProposalDetails", "Proposal", new { id = model.actionTypeId, leadId = Convert.ToInt32(model.leadsId), leadName = model.leadName, Area = "CRMLead" });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<IActionResult> ClientDeleteComments(int Id, int actionId, int leadId, string leadName)
        {
            #region History Save 
            LeadsHistory leadsData = new LeadsHistory
            {
                Id = 0,
                leadsId = leadId,
                actionName = "Proposal",
                actionDetails = "Comments is Deleted."
            };
            await leadsService.SaveLeadsHistory(leadsData);
            #endregion

            await attachmentCommentService.DeleteCommentById(Id);
            return RedirectToAction("ClientProposalDetails", "Proposal", new { id = actionId, leadId = Convert.ToInt32(leadId), leadName = leadName, Area = "CRMLead" });
        }

        [HttpPost]
        public async Task<IActionResult> ClientSaveDoc([FromForm] DocumentAttachmentViewModel model, IFormFile docformFile)
        {
            try
            {
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
                        moduleId = 2,
                        createdBy = userName
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);

                    #region History Save 
                    LeadsHistory leadsData = new LeadsHistory
                    {
                        Id = 0,
                        leadsId = model.leadsId,
                        actionName = "Proposal",
                        actionDetails = "Document " + NewFileName + " is Uploaded."
                    };
                    await leadsService.SaveLeadsHistory(leadsData);
                    #endregion
                }



                return RedirectToAction("ClientProposalDetails", "Proposal", new { id = model.actionTypeId, leadId = Convert.ToInt32(model.leadsId), leadName = model.leadName, Area = "CRMLead" });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> ClientSavePhoto([FromForm] DocumentAttachmentViewModel model, IFormFile formFile)
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
                        moduleId = 2,
                        createdBy = userName
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);

                    #region History Save 
                    LeadsHistory leadsData = new LeadsHistory
                    {
                        Id = 0,
                        leadsId = model.leadsId,
                        actionName = "Proposal",
                        actionDetails = "Photo "+ NewFileName + " is Uploaded."
                    };
                    await leadsService.SaveLeadsHistory(leadsData);
                    #endregion


                }


                return RedirectToAction("ClientProposalDetails", "Proposal", new { id = model.actionTypeId, leadId = Convert.ToInt32(model.leadsId), leadName = model.leadName, Area = "CRMLead" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> ClientDeletePhoto(int actionId, int photoId, int leadId, string leadName)
        {
            #region History Save 
            LeadsHistory leadsData = new LeadsHistory
            {
                Id = 0,
                leadsId = leadId,
                actionName = "Proposal",
                actionDetails = "Documents/Photo is Deleted."
            };
            await leadsService.SaveLeadsHistory(leadsData);
            #endregion

            var data = await attachmentCommentService.GetDocumentAttachmentById(photoId);
            var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";

            var fullFilePath = Path.Combine(filesPath, data.fileName);
            if (System.IO.File.Exists(fullFilePath))
                System.IO.File.Delete(fullFilePath);
            await attachmentCommentService.DeleteDocumentAttachmentById(photoId);
            return RedirectToAction("ClientProposalDetails", "Proposal", new { id = actionId, leadId = Convert.ToInt32(leadId), leadName = leadName, Area = "CRMLead" });
        }

        #endregion

    }
}