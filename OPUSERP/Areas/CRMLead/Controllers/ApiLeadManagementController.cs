using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.CRM.Services.Lead.Interfaces;
using OPUSERP.Data.Entity;
//using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.CRM.Services.Client.Interfaces;
using OPUSERP.Areas.CRMLead.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.Client;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.Areas.MasterData.Models;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using IDesignationDepartmentService = OPUSERP.HRPMS.Services.MasterData.Interfaces.IDesignationDepartmentService;
using OPUSERP.CRM.Data.Entity.MasterData;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using OPUSERP.ERPService.MasterData.Interfaces;

namespace OPUSERP.Areas.CRMLead.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ApiLeadManagementController : ControllerBase
    {
        private ERPDbContext _db;
        private readonly ILeadsService leadsService;
        private readonly IContactsService contactsService;
        private readonly IResourceService resourceService;
        private readonly ICRMDesignationDepartmentService designationDepartmentService;
        private readonly ISectorService sectorService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ICompanyGroupService companyGroupService;
        private readonly IOwnershipService ownershipService;
        private readonly ISourceService sourceService;
        private readonly IClientService clientService;
        private readonly IAttachmentCommentService attachmentCommentService;
        private readonly IAddressesService addressesService;
        private readonly IActivityMasterService activityMasterService;
        private readonly IBankBranchService bankBranchService;
        private readonly IActivityCategoryService activityCategoryService;
        private readonly IActivitySessionService activitySessionService;
        private readonly IActivityTypeService activityTypeService;
        private readonly IActivityStatusService activityStatusService;
        private readonly ITeamService teamService;


        public ApiLeadManagementController(ILeadsService leadsService, ERPDbContext db, IHostingEnvironment hostingEnvironment, IContactsService contactsService, IResourceService resourceService, ICRMDesignationDepartmentService designationDepartmentService, ISectorService sectorService, ICompanyGroupService companyGroupService, IOwnershipService ownershipService, ISourceService sourceService, IClientService clientService, IAttachmentCommentService attachmentCommentService, IAddressesService addressesService, IActivityMasterService activityMasterService, IBankBranchService bankBranchService, IActivityCategoryService activityCategoryService, IActivitySessionService activitySessionService, IActivityTypeService activityTypeService, IActivityStatusService activityStatusService, ITeamService teamService)
        {
            this.bankBranchService = bankBranchService;
            this.leadsService = leadsService;
            this.contactsService = contactsService;
            this.resourceService = resourceService;
            this.designationDepartmentService = designationDepartmentService;
            this.sectorService = sectorService;
            this._hostingEnvironment = hostingEnvironment;
            _db = db;
            this.companyGroupService = companyGroupService;
            this.ownershipService = ownershipService;
            this.sourceService = sourceService;
            this.clientService = clientService;
            this.attachmentCommentService = attachmentCommentService;
            this.addressesService = addressesService;
            this.activityMasterService = activityMasterService;
            this.activityCategoryService = activityCategoryService;
            this.activitySessionService = activitySessionService;
            this.activityTypeService = activityTypeService;
            this.activityStatusService = activityStatusService;
            this.teamService = teamService;
        }

        #region Lead Create
        [HttpGet]
        public async Task<IActionResult> LeadInfo()
        {
            var leadAutoNumbers = await leadsService.GetLeadAutoNumberBySp();
            var model = new LeadViewModel
            {
                leads = await leadsService.GetAllLeadsByuser(HttpContext.User.Identity.Name),
                sectors = await sectorService.GetAllSector(),
                companyGroups = await companyGroupService.GetAllCompanyGroup(),
                ownerShipTypes = await ownershipService.GetAllOwnership(),
                sources = await sourceService.GetAllSource(),
                fITypes=await bankBranchService.GetAllFIType(),
                leadAutoNumbers= leadAutoNumbers
               
            };
            
            return new OkObjectResult(model);
        }
       
        // POST: Lead/save/Edit
        [HttpPost]
      
        public async Task<IActionResult> LeadInfo([FromBody] LeadViewModel model)
        {
            var id = 0;
            if (!ModelState.IsValid)
            {
                model.leads = await leadsService.GetAllLeadsByuser(HttpContext.User.Identity.Name);
                return new OkObjectResult("Something Went Wrong!!");
            }

            Leads data = new Leads
            {
                Id = model.leadId,
                leadOwner = HttpContext.User.Identity.Name,
                leadName = model.leadName,
                leadShortName = model.leadShortName,
                leadNumber = model.leadNumber,
                companyGroupId = model.companyGroupId,
                fITypeId=model.fITypeId,
                ownerShipTypeId = model.ownerShipTypeId,
                sourceId = model.sourceId,
                sourceDescription = model.sourceDescription,
                totalemployee = model.totalemployee,
                sectorId = (int)model.sector,
                createdBy = HttpContext.User.Identity.Name,
                createdAt = DateTime.Now
            };

            id = await leadsService.SaveLeads(data);

            #region Save History
            string actDetailss = string.Empty;
            if (model.leadId == 0)
            {
                actDetailss = "Lead " + model.leadName + " is Created.";
            }
            else
            {
                actDetailss = "Lead " + model.leadName + " is Updated.";
            }

            LeadsHistory leadsData = new LeadsHistory
            {
                Id = 0,
                leadsId = id,
                actionName = "Lead",
                actionDetails = actDetailss
            };
            await leadsService.SaveLeadsHistory(leadsData);
            #endregion
            
            return new OkObjectResult(id);
        }

        #endregion
        #region Contacts
        [HttpPost]
    
        public async Task<IActionResult> Contacts([FromBody] ContactsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.contacts = await contactsService.GetAllContactsByLeadsId(Convert.ToInt32(model.leadId));
                model.departments = await designationDepartmentService.GetDepartment();
                model.designations = await designationDepartmentService.GetDesignations();
                return new OkObjectResult("Something Went Wrong!!");
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
                skypeId = model.skypeId,
                linkedInId = model.linkedInId,
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
            //if (imagePath != null)
            //{
            //    string userName = User.Identity.Name;
            //    string documentType = "photo";
            //    var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
            //    var fileName = ContentDispositionHeaderValue.Parse(imagePath.ContentDisposition).FileName;
            //    string fileType = Path.GetExtension(fileName);
            //    fileName = fileName.Contains("\\")
            //        ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
            //        : fileName.Trim('"');

            //    string NewFileName = Id + "_" + documentType + "_" + fileName;
            //    string fileLocation = "/Upload/Photo/" + NewFileName;
            //    var fullFilePath = Path.Combine(filesPath, NewFileName);

            //    using (var stream = new FileStream(fullFilePath, FileMode.Create))
            //    {
            //        await imagePath.CopyToAsync(stream);
            //    }

            //    DocumentPhotoAttachment dataPH = new DocumentPhotoAttachment
            //    {
            //        Id = 0,
            //        actionType = "Contact",
            //        actionTypeId = Id,
            //        subject = "",
            //        fileName = NewFileName,
            //        filePath = fileLocation,
            //        fileType = fileType,
            //        description = "",
            //        documentType = documentType,
            //        moduleId = 2,
            //        createdBy = userName
            //    };
            //    await attachmentCommentService.SaveDocumentAttachment(dataPH);
            //}

            //if (imagePath_Vcard_Font != null)
            //{

            //    string userName = User.Identity.Name;
            //    string documentType = "Vcard_Font";
            //    var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
            //    var fileName = ContentDispositionHeaderValue.Parse(imagePath_Vcard_Font.ContentDisposition).FileName;
            //    string fileType = Path.GetExtension(fileName);
            //    fileName = fileName.Contains("\\")
            //        ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
            //        : fileName.Trim('"');

            //    string NewFileName = Id + "_" + documentType + "_" + fileName;
            //    string fileLocation = "/Upload/Photo/" + NewFileName;
            //    var fullFilePath = Path.Combine(filesPath, NewFileName);

            //    using (var stream = new FileStream(fullFilePath, FileMode.Create))
            //    {
            //        await imagePath_Vcard_Font.CopyToAsync(stream);
            //    }

            //    DocumentPhotoAttachment dataF = new DocumentPhotoAttachment
            //    {
            //        Id = 0,
            //        actionType = "Contact",
            //        actionTypeId = Id,
            //        subject = "",
            //        fileName = NewFileName,
            //        filePath = fileLocation,
            //        fileType = fileType,
            //        description = "",
            //        documentType = documentType,
            //        moduleId = 2,
            //        createdBy = userName
            //    };
            //    await attachmentCommentService.SaveDocumentAttachment(dataF);

            //}
            //if (imagePath_Vcard_Back != null)
            //{
            //    string userName = User.Identity.Name;
            //    string documentType = "Vcard_Back";
            //    var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
            //    var fileName = ContentDispositionHeaderValue.Parse(imagePath_Vcard_Back.ContentDisposition).FileName;
            //    string fileType = Path.GetExtension(fileName);
            //    fileName = fileName.Contains("\\")
            //        ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
            //        : fileName.Trim('"');

            //    string NewFileName = Id + "_" + documentType + "_" + fileName;
            //    string fileLocation = "/Upload/Photo/" + NewFileName;
            //    var fullFilePath = Path.Combine(filesPath, NewFileName);

            //    using (var stream = new FileStream(fullFilePath, FileMode.Create))
            //    {
            //        await imagePath_Vcard_Back.CopyToAsync(stream);
            //    }

            //    DocumentPhotoAttachment dataB = new DocumentPhotoAttachment
            //    {
            //        Id = 0,
            //        actionType = "Contact",
            //        actionTypeId = Id,
            //        subject = "",
            //        fileName = NewFileName,
            //        filePath = fileLocation,
            //        fileType = fileType,
            //        description = "",
            //        documentType = documentType,
            //        moduleId = 2,
            //        createdBy = userName
            //    };
            //    await attachmentCommentService.SaveDocumentAttachment(dataB);
            //}


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

            return new OkObjectResult("Saved Successfully !!");
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> allDepartments()
        {
            return new OkObjectResult(await designationDepartmentService.GetDepartment());
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> allDesigations()
        {
            return new OkObjectResult(await designationDepartmentService.GetDesignations());
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllContactsByLeadsId(int id)
        {
            return new OkObjectResult(await contactsService.GetAllContactsByLeadsId(id));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivityMasterByLeadId(int id)
        {
            return new OkObjectResult(await activityMasterService.GetActivityMasterByLeadId(id));
        }
        
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeamByParrentId(int id)
        {
            return new OkObjectResult(await teamService.GetTeamByParrentId(id));
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllActivityCategory()
        {
            return new OkObjectResult(await activityCategoryService.GetAllActivityCategory());
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllActivitySession()
        {
            return new OkObjectResult(await activitySessionService.GetAllActivitySession());
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllActivityType()
        {
            return new OkObjectResult(await activityTypeService.GetAllActivityType());
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllActivityStatus()
        {
            return new OkObjectResult(await activityStatusService.GetAllActivityStatus());
        }


        #endregion


    }
}