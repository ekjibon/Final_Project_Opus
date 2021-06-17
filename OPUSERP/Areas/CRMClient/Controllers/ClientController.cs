using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.CRMClient.Models;
using OPUSERP.Areas.CRMLead.Models;
using OPUSERP.Areas.MasterData.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Services.Client.Interfaces;
using OPUSERP.CRM.Services.Lead.Interfaces;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.Data;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using IDesignationDepartmentService = OPUSERP.HRPMS.Services.MasterData.Interfaces.IDesignationDepartmentService;

namespace OPUSERP.Areas.CRMClient.Controllers
{
    [Area("CRMClient")]
    public class ClientController : Controller
    {
        private ERPDbContext _db;
        private readonly IClientService clientService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILeadsService leadsService;
        private readonly ISectorService sectorService;
        private readonly ICompanyGroupService companyGroupService;
        private readonly IOwnershipService ownershipService;
        private readonly ISourceService sourceService;
        private readonly IResourceService resourceService;
        private readonly ICRMDesignationDepartmentService designationDepartmentService;
        private readonly IContactsService contactsService;
        private readonly IAttachmentCommentService attachmentCommentService;
        private readonly IAddressesService addressesService;
        private readonly IActivityMasterService activityMasterService;
        private readonly IBankBranchService bankBranchService;
        public ClientController(ERPDbContext db, IHostingEnvironment hostingEnvironment, IBankBranchService bankBranchService,  IClientService clientService, ILeadsService leadsService, ISectorService sectorService, ICompanyGroupService companyGroupService, IOwnershipService ownershipService, ISourceService sourceService, IResourceService resourceService, ICRMDesignationDepartmentService designationDepartmentService, IContactsService contactsService, IAttachmentCommentService attachmentCommentService, IAddressesService addressesService, IActivityMasterService activityMasterService)
        {
           
            this._hostingEnvironment = hostingEnvironment;
            _db = db;
            this.clientService = clientService;
            this.leadsService = leadsService;
            this.sectorService = sectorService;
            this.companyGroupService = companyGroupService;
            this.ownershipService = ownershipService;
            this.sourceService = sourceService;
            this.resourceService = resourceService;
            this.designationDepartmentService = designationDepartmentService;
            this.contactsService = contactsService;
            this.attachmentCommentService = attachmentCommentService;
            this.addressesService = addressesService;
            this.activityMasterService = activityMasterService;
            this.bankBranchService = bankBranchService;
        }

        #region Client List
        public IActionResult Index()
        {
            ClientViewModel model = new ClientViewModel
            {
                //getClientInfoListViewModels = await clientService.GetClientsByOwner(HttpContext.User.Identity.Name)

            };
            return View(model);
        }
        //[Route("global/api/GetAllClientListS/{TeamLeader}/{Fa}/{BD}/{LeadId}")]
        //[HttpGet]
        public async Task<IActionResult> GetAllClientListS(string TeamLeader, string Fa, string BD, string LeadId)
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
            var data = await clientService.GetClientsByOwnerfilter(HttpContext.User.Identity.Name,TeamLeader,Fa,BD,LeadId);
           
            //  string FromDate,string Todate,string TeamLeader,string Fa,string BD,int LeadId,int JobstatusId,string ratingType
            return Json(data);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllClientList()
        {
            var data = await clientService.GetClientsByOwner(HttpContext.User.Identity.Name);
            return Json(data);
        }
        #endregion

        #region Client Info
        public async Task<IActionResult> ClientInfo(int? Id, string leadName, string message)
        {
            int Lid = Convert.ToInt32(Id);
            var sectorId = 0;

            var leadShortName = "";
            var leadNumber = "";
            int? companyGroupId = 0;
            int? ownerShipTypeId = 0;
            int? sourceId = 0;
            var sourceDescription = "";

            if (Lid != 0)
            {
                var sectordata = await leadsService.GetLeadsById(Lid);
                leadName = sectordata.leadName;
                sectorId = sectordata.sectorId != null ? (int)sectordata.sectorId : 0;
                leadShortName = sectordata.leadShortName;
                leadNumber = sectordata.leadNumber;
                companyGroupId = sectordata.companyGroupId;
                ownerShipTypeId = sectordata.ownerShipTypeId;
                sourceId = sectordata.sourceId;
                sourceDescription = sectordata.sourceDescription;
                leadName = sectordata.leadName;
            }
            var model = new LeadViewModel
            {
                leads = await leadsService.GetAllLeadsByuser(HttpContext.User.Identity.Name),
                sectors = await sectorService.GetAllSector(),
                companyGroups = await companyGroupService.GetAllCompanyGroup(),
                ownerShipTypes = await ownershipService.GetAllOwnership(),
                sources = await sourceService.GetAllSource(),
                leadAutoNumbers = await leadsService.GetLeadAutoNumberBySp(),
                fITypes = await bankBranchService.GetAllFIType()
            };

            ViewBag.leadName = leadName;
            ViewBag.leadId = Id;
            ViewBag.sectorId = sectorId;
            ViewBag.leadShortName = leadShortName;
            ViewBag.leadNumber = leadNumber;
            ViewBag.companyGroupId = companyGroupId;
            ViewBag.ownerShipTypeId = ownerShipTypeId;
            ViewBag.sourceId = sourceId;
            ViewBag.sourceDescription = sourceDescription;
            ViewBag.message = message;

            return View(model);
        }
        // POST: Lead/save/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClientInfo([FromForm] LeadViewModel model)
        {
            var id = 0;
            if (!ModelState.IsValid)
            {
                model.leads = await leadsService.GetAllLeadsByuser(HttpContext.User.Identity.Name);
                return View(model);
            }

            Leads data = new Leads
            {
                Id = model.leadId,
                leadOwner = HttpContext.User.Identity.Name,
                leadName = model.leadName,
                leadShortName = model.leadShortName,
                leadNumber = model.leadNumber,
                companyGroupId = model.companyGroupId,
                ownerShipTypeId = model.ownerShipTypeId,
                sourceId = model.sourceId,
                sourceDescription = model.sourceDescription,
                sectorId = (int)model.sector,
                createdBy = HttpContext.User.Identity.Name,
                createdAt = DateTime.Now
            };

            id = await leadsService.SaveLeads(data);
            string message = "Save";
            return RedirectToAction(nameof(ClientInfo), new { id, model.leadName, message });
        }
        #endregion

        #region Client Contacts
        public async Task<IActionResult> ClientContacts(int? id, string leadName)
        {
            var model = new ContactsViewModel
            {
                contacts = await contactsService.GetAllContactsByLeadsId(Convert.ToInt32(id)),
                departments = await designationDepartmentService.GetDepartment(),
                designations = await designationDepartmentService.GetDesignations()
            };
            ViewBag.leadName = leadName;
            ViewBag.leadId = id;
            return View(model);
        }

        // POST: Contact/save/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClientContacts([FromForm] ContactsViewModel model, IFormFile imagePath, IFormFile imagePath_Vcard_Font, IFormFile imagePath_Vcard_Back)
        {
            if (!ModelState.IsValid)
            {
                model.contacts = await contactsService.GetAllContactsByLeadsId(Convert.ToInt32(model.leadId));
                model.departments = await designationDepartmentService.GetDepartment();
                model.designations = await designationDepartmentService.GetDesignations();
                ViewBag.leadName = model.leadName;
                ViewBag.leadId = Convert.ToInt32(model.leadId);
                return View(model);
            }

            int resourceId = 0;
            //  string message = FileSave.SaveImage(out fileName, model.empPhoto);

            //if (message == "success")
            //{
            Resource resource = new Resource
            {
                Id = model.id,
                resourceName = model.contactName,
                phone = model.phone,
                mobile = model.mobile,
                email = model.email,
                imagePath = "",
                fax = model.fax,
                designationsId = Convert.ToInt32(model.designation),
                departmentsId = Convert.ToInt32(model.department)
            };
            resourceId = await resourceService.SaveResourceReturnId(resource);
            //}              

            Contacts data = new Contacts
            {
                Id = model.contactId,
                contactOwner = HttpContext.User.Identity.Name,
                resourceId = resourceId,
                leadsId = Convert.ToInt32(model.leadId),
                createdBy = HttpContext.User.Identity.Name,
                createdAt = DateTime.Now
            };

            int Id = await contactsService.SaveContacts(data);

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

                string NewFileName = Id + "_" + documentType + "_" + fileName;
                string fileLocation = "/Upload/Photo/" + NewFileName;
                var fullFilePath = Path.Combine(filesPath, NewFileName);

                using (var stream = new FileStream(fullFilePath, FileMode.Create))
                {
                    await imagePath.CopyToAsync(stream);
                }

                DocumentPhotoAttachment dataPH = new DocumentPhotoAttachment
                {
                    Id = 0,
                    actionType = "Contact",
                    actionTypeId = Id,
                    subject = "",
                    fileName = NewFileName,
                    filePath = fileLocation,
                    fileType = fileType,
                    description = "",
                    documentType = documentType,
                    moduleId = 2,
                    createdBy = userName
                };
                await attachmentCommentService.SaveDocumentAttachment(dataPH);
            }

            if (imagePath_Vcard_Font != null)
            {

                string userName = User.Identity.Name;
                string documentType = "Vcard_Font";
                var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                var fileName = ContentDispositionHeaderValue.Parse(imagePath_Vcard_Font.ContentDisposition).FileName;
                string fileType = Path.GetExtension(fileName);
                fileName = fileName.Contains("\\")
                    ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                    : fileName.Trim('"');

                string NewFileName = Id + "_" + documentType + "_" + fileName;
                string fileLocation = "/Upload/Photo/" + NewFileName;
                var fullFilePath = Path.Combine(filesPath, NewFileName);

                using (var stream = new FileStream(fullFilePath, FileMode.Create))
                {
                    await imagePath_Vcard_Font.CopyToAsync(stream);
                }

                DocumentPhotoAttachment dataF = new DocumentPhotoAttachment
                {
                    Id = 0,
                    actionType = "Contact",
                    actionTypeId = Id,
                    subject = "",
                    fileName = NewFileName,
                    filePath = fileLocation,
                    fileType = fileType,
                    description = "",
                    documentType = documentType,
                    moduleId = 2,
                    createdBy = userName
                };
                await attachmentCommentService.SaveDocumentAttachment(dataF);

            }
            if (imagePath_Vcard_Back != null)
            {
                string userName = User.Identity.Name;
                string documentType = "Vcard_Back";
                var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                var fileName = ContentDispositionHeaderValue.Parse(imagePath_Vcard_Back.ContentDisposition).FileName;
                string fileType = Path.GetExtension(fileName);
                fileName = fileName.Contains("\\")
                    ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                    : fileName.Trim('"');

                string NewFileName = Id + "_" + documentType + "_" + fileName;
                string fileLocation = "/Upload/Photo/" + NewFileName;
                var fullFilePath = Path.Combine(filesPath, NewFileName);

                using (var stream = new FileStream(fullFilePath, FileMode.Create))
                {
                    await imagePath_Vcard_Back.CopyToAsync(stream);
                }

                DocumentPhotoAttachment dataB = new DocumentPhotoAttachment
                {
                    Id = 0,
                    actionType = "Contact",
                    actionTypeId = Id,
                    subject = "",
                    fileName = NewFileName,
                    filePath = fileLocation,
                    fileType = fileType,
                    description = "",
                    documentType = documentType,
                    moduleId = 2,
                    createdBy = userName
                };
                await attachmentCommentService.SaveDocumentAttachment(dataB);
            }


            #region Save History
            string actDetailss = string.Empty;
            if (model.id == 0)
            {
                actDetailss = "Contact " + model.contactName + " is Created.";
            }
            else
            {
                actDetailss = "Contact " + model.contactName + " is Updated.";
            }

            LeadsHistory leadsData = new LeadsHistory
            {
                Id = 0,
                leadsId = Convert.ToInt32(model.leadId),
                actionName = "Contact",
                actionDetails = actDetailss
            };
            await leadsService.SaveLeadsHistory(leadsData);
            #endregion

            return RedirectToAction(nameof(ClientContacts), new
            {
                id = Convert.ToInt32(model.leadId),
                leadName = model.leadName
            });
        }
        #endregion



        #region Client Details
        public async Task<IActionResult> ClientDetail(int Id)
        {
            try
            {
                ViewBag.masterId = Id;
                var CommentInfo = await attachmentCommentService.GetCommentByActionTypeId(Id, "Lead", 2);
                if (CommentInfo == null)
                {
                    CommentInfo = new List<Comment>();
                }
                var photoInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(Id, "Lead", "photo", 2);
                if (photoInfo == null)
                {
                    photoInfo = new List<DocumentPhotoAttachment>();
                }
                var docInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(Id, "Lead", "Document", 2);
                if (docInfo == null)
                {
                    docInfo = new List<DocumentPhotoAttachment>();
                }

                var model = new LeadViewModel
                {
                    GetLeadDetailsById = await leadsService.GetLeadDetailsById(Id),
                    contacts = await contactsService.GetAllContactsByLeadsId(Convert.ToInt32(Id)),
                    Addresses = await addressesService.GetAddressesByLeadId(Id),
                    activityMasters = await activityMasterService.GetActivityMasterByLeadId(Id),
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
                    actionType = "Lead",
                    actionTypeId = model.actionTypeId,
                    title = model.title,
                    comment = model.comment,
                    moduleId = 2,
                    createdBy = HttpContext.User.Identity.Name,
                };
                int commentsId = await attachmentCommentService.SaveComment(entityComment);
                return RedirectToAction("ClientDetail", "Client", new { id = model.actionTypeId, Area = "CRMClient" });

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
            return RedirectToAction("ClientDetail", "Client", new { id = actionId, Area = "CRMClient" });
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
                        moduleId = 2,
                        createdBy = userName
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);
                }

                return RedirectToAction("ClientDetail", "Client", new { id = model.actionTypeId, Area = "CRMClient" });
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
                }

                return RedirectToAction("ClientDetail", "Client", new { id = model.actionTypeId, Area = "CRMClient" });
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
            return RedirectToAction("ClientDetail", "Client", new { id = actionId, Area = "CRMClient" });
        }
        #endregion


        #region Client History


        public async Task<IActionResult> ClientHistory(int? id, string leadName)
        {
            LeadsHistoryViewModel model = new LeadsHistoryViewModel()
            {
                leadsHistories = await leadsService.GetLeadsHistoryByLeadId(Convert.ToInt32(id))
            };
            ViewBag.leadName = leadName;
            ViewBag.leadId = id;
            return View(model);
        }


        #endregion

        #region Client Files
        public async Task<IActionResult> ClientFile(int id, string leadName)
        {
            try
            {
                ViewBag.masterId = id;

                var photoInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "Lead", "photo", 2);
                if (photoInfo == null)
                {
                    photoInfo = new List<DocumentPhotoAttachment>();
                }
                var docInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "Lead", "Document", 2);
                if (docInfo == null)
                {
                    docInfo = new List<DocumentPhotoAttachment>();
                }

                var model = new LeadViewModel
                {
                    photoes = photoInfo,
                    documents = docInfo,
                };
                ViewBag.leadName = leadName;
                ViewBag.leadId = id;
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> SaveDocClientFile([FromForm] DocumentAttachmentViewModel model, IFormFile formFile)
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
                        moduleId = 2,
                        createdBy = userName
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);
                }
                #region History Save 
                LeadsHistory leadsData = new LeadsHistory
                {
                    Id = 0,
                    leadsId = model.actionTypeId,
                    actionName = "File",
                    actionDetails = "Document is Uploaded."
                };
                await leadsService.SaveLeadsHistory(leadsData);
                #endregion

                return RedirectToAction("ClientFile", "Client", new { id = model.actionTypeId, Area = "CRMClient" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> SavePhotoClientFile([FromForm] DocumentAttachmentViewModel model, IFormFile formFile)
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
                }
                #region History Save 
                LeadsHistory leadsData = new LeadsHistory
                {
                    Id = 0,
                    leadsId = model.actionTypeId,
                    actionName = "File",
                    actionDetails = "Photo is Uploaded."
                };
                await leadsService.SaveLeadsHistory(leadsData);
                #endregion

                return RedirectToAction("ClientFile", "Client", new { id = model.actionTypeId, Area = "CRMClient" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeletePhotoClientFile(int actionId, int photoId)
        {
            #region History Save 
            LeadsHistory leadsData = new LeadsHistory
            {
                Id = 0,
                leadsId = actionId,
                actionName = "File",
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
            return RedirectToAction("ClientFile", "Client", new { id = actionId, Area = "CRMClient" });
        }
        #endregion



    }
}