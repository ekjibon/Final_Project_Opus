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
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.OtherSales.Services.Sales.Interfaces;
using OPUSERP.CRM.Services.Proposal.Interfaces;
using OPUSERP.Helpers;
using DinkToPdf.Contracts;
using OPUSERP.Data.Entity.Master;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.ERPServices.EmailService.interfaces;

namespace OPUSERP.Areas.CRMLead.Controllers
{
    [Area("CRMLead")]
    public class LeadManagementController : Controller
    {
        private ERPDbContext _db;
        private readonly IAgreementService agreementService;
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
        private readonly IUserInfoes userInfo;
        private readonly IERPCompanyService eRPCompanyService;
        private readonly ICustomerService customerService;
        private readonly IProposalService proposalService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly IEmailSenderService emailSenderService;
        private readonly IOSalesInvoiceMasterService oSalesInvoiceMasterService;

        private readonly string rootPath;
        private readonly MyPDF myPDF;

        public LeadManagementController(IConverter converter, ILeadsService leadsService, IOSalesInvoiceMasterService oSalesInvoiceMasterService, IPersonalInfoService personalInfoService, IEmailSenderService emailSenderService, IAgreementService agreementService, IProposalService proposalService, IERPCompanyService eRPCompanyService, ICustomerService customerService, IUserInfoes userInfo, ERPDbContext db, IHostingEnvironment hostingEnvironment, IContactsService contactsService, IResourceService resourceService, ICRMDesignationDepartmentService designationDepartmentService, ISectorService sectorService, ICompanyGroupService companyGroupService, IOwnershipService ownershipService, ISourceService sourceService, IClientService clientService, IAttachmentCommentService attachmentCommentService, IAddressesService addressesService, IActivityMasterService activityMasterService, IBankBranchService bankBranchService)
        {
            this.agreementService = agreementService;
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
            this.userInfo = userInfo;
            this.customerService = customerService;
            this.eRPCompanyService = eRPCompanyService;
            this.proposalService = proposalService;
            this.personalInfoService = personalInfoService;
            this.emailSenderService = emailSenderService;
            this.oSalesInvoiceMasterService = oSalesInvoiceMasterService;

            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        public async Task<IActionResult> LeadPreview(int id)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            LeadPreviewViewModel model = new LeadPreviewViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                leads = await leadsService.GetLeadsById(id),
                Addresses = await addressesService.GetAddressesByLeadId(id),
                contacts = await contactsService.GetAllContactsByLeadsId(Convert.ToInt32(id)),
                activityMasterCViewModels = await activityMasterService.GetActivityMasterCByLeadId(id),
                proposalMasters = await proposalService.GetProposalMasterByLeadId(Convert.ToInt32(id)),
                leadsBankInfos = await bankBranchService.GetLeadsBankInfoByLeadId(id),
                agreements = await agreementService.GetAgreementbyLeadId(id),
                leadsHistories = await leadsService.GetLeadsHistoryByLeadId(Convert.ToInt32(id))
            };
            return View(model);
        }

        //PrintPDF
        [AllowAnonymous]
        public IActionResult LeadViewPdf(int id)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string fileName;

            string url = scheme + "://" + host + "/CRMLead/LeadManagement/LeadPrintView/" + id;
            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        public async Task<IActionResult> LeadPrintView(int id)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            LeadPreviewViewModel model = new LeadPreviewViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                leads = await leadsService.GetLeadsById(id),
                Addresses = await addressesService.GetAddressesByLeadId(id),
                contacts = await contactsService.GetAllContactsByLeadsId(Convert.ToInt32(id)),
                activityMasterCViewModels = await activityMasterService.GetActivityMasterCByLeadId(id),
                proposalMasters = await proposalService.GetProposalMasterByLeadId(Convert.ToInt32(id)),
                leadsBankInfos = await bankBranchService.GetLeadsBankInfoByLeadId(id),
                agreements = await agreementService.GetAgreementbyLeadId(id),
                leadsHistories = await leadsService.GetLeadsHistoryByLeadId(Convert.ToInt32(id))
            };
            return View(model);
        }

        #region Lead Personal

        public async Task<IActionResult> LeadInfoPersonal(int? Id, string leadName, string message)
        {
            int Lid = Convert.ToInt32(Id);
            var sectorId = 0;

            var leadShortName = "";
            var leadNumber = "";
            int? companyGroupId = 0;
            int? fITypeId = 0;
            int? ownerShipTypeId = 0;
            int? sourceId = 0;
            var sourceDescription = "";
            int? totalemployee = 0;
            string companyName = "";
           

            if (Lid != 0)
            {
                var sectordata = await leadsService.GetLeadsById(Lid);
                leadName = sectordata.leadName;
                sectorId = sectordata.sectorId != null ? (int)sectordata.sectorId : 0;
                leadShortName = sectordata.leadShortName;
                leadNumber = sectordata.leadNumber;
                companyGroupId = sectordata.companyGroupId;
                fITypeId = sectordata.fITypeId;
                ownerShipTypeId = sectordata.ownerShipTypeId;
                sourceId = sectordata.sourceId;
                sourceDescription = sectordata.sourceDescription;
                leadName = sectordata.leadName;
                companyName = sectordata.companyName;
                totalemployee = sectordata.totalemployee;

            }
            else
            {
                var leadAutoNumbers = await leadsService.GetLeadAutoNumberBySp();
                leadNumber = leadAutoNumbers.leadNumber;
            }
            var model = new LeadViewModel
            {
                leads = await leadsService.GetAllLeadsByuser(HttpContext.User.Identity.Name),
                sectors = await sectorService.GetAllSector(),
                companyGroups = await companyGroupService.GetAllCompanyGroup(),
                ownerShipTypes = await ownershipService.GetAllOwnership(),
                sources = await sourceService.GetAllSource(),
                fITypes = await bankBranchService.GetAllFIType(),
                contacts = await contactsService.GetAllContactsByLeadsId(Convert.ToInt32(Id)),
                departments = await designationDepartmentService.GetDepartment(),
                designations = await designationDepartmentService.GetDesignations()
                //leadAutoNumbers = await leadsService.GetLeadAutoNumberBySp()
            };

            ViewBag.leadName = leadName;
            ViewBag.companyName = companyName;
            ViewBag.leadId = Id;
            ViewBag.sectorId = sectorId;
            ViewBag.leadShortName = leadShortName;
            ViewBag.leadNumber = leadNumber;
            ViewBag.companyGroupId = companyGroupId;
            ViewBag.fITypeId = fITypeId;
            ViewBag.ownerShipTypeId = ownerShipTypeId;
            ViewBag.sourceId = sourceId;
            ViewBag.sourceDescription = sourceDescription;
            ViewBag.message = message;
            ViewBag.totalemployee = totalemployee;

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LeadInfoPersonal([FromForm] ContactsViewModel model, IFormFile imagePath, IFormFile imagePath_Vcard_Font, IFormFile imagePath_Vcard_Back)
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

            var leadAutoNumbers = await leadsService.GetLeadAutoNumberBySp();
            var leadNumber = leadAutoNumbers.leadNumber;


            Leads lead = new Leads
            {
                Id = Convert.ToInt32(model.leadId),
                leadOwner = HttpContext.User.Identity.Name,
                leadName = model.contactName,
                leadShortName = model.contactName,
                leadNumber = leadNumber,
                isPersonal = 1,
                sourceDescription=model.sourceDescription,
                companyName = model.companyName,
                createdBy = HttpContext.User.Identity.Name,
                createdAt = DateTime.Now
            };

            var masterId = await leadsService.SaveLeads(lead);
            if (Convert.ToInt32(model.leadId) == 0)
            {
                RelSupplierCustomerResourse data2 = new RelSupplierCustomerResourse
                {
                    Id = 0,
                    LeadsId = masterId,
                    roleTypeId = 4

                };

                int relid = await customerService.SaveRelSupplierCustomerResourse(data2);

            }
            Resource resource = new Resource
            {
                Id = model.contactId,
                resourceName = model.contactName,
                phone = model.phone,
                mobile = model.mobile,
                email = model.email,
                age = model.age,
                gender = model.gender,
                imagePath = "",
                fax = model.fax,
                skypeId = model.skypeId,
                linkedInId = model.linkedInId,
                crmdesignationsId = model.designation,
                crmdepartmentsId = model.department
            };
            resourceId = await resourceService.SaveResourceReturnId(resource);
            //}              
           
            Contacts data = new Contacts
            {
                Id = model.id,
                contactOwner = HttpContext.User.Identity.Name,
                resourceId = resourceId,
                leadsId = masterId,
                isLead = 1,
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
                leadsId = masterId,
                actionName = "Contact",
                actionDetails = actDetailss
            };
            await leadsService.SaveLeadsHistory(leadsData);
            #endregion

            return RedirectToAction(nameof(LeadInfoPersonal), new
            {
                id = masterId,
                leadName = model.leadName
            });
        }

        #endregion

        #region Lead Create
        public async Task<IActionResult> LeadInfo(int? Id, string leadName, string message)
        {
            int Lid = Convert.ToInt32(Id);
            var sectorId = 0;

            var leadShortName = "";
            var leadNumber = "";
            int? companyGroupId = 0;
            int? fITypeId = 0;
            int? ownerShipTypeId = 0;
            int? sourceId = 0;
            var sourceDescription = "";
            int? totalemployee = 0;
            var leadsingal = new Leads();
            if (Lid != 0)
            {
                var sectordata = await leadsService.GetLeadsById(Lid);
                leadsingal = sectordata;
                leadName = sectordata.leadName;
                sectorId = sectordata.sectorId != null ? (int)sectordata.sectorId : 0;
                leadShortName = sectordata.leadShortName;
                leadNumber = sectordata.leadNumber;
                companyGroupId = sectordata.companyGroupId;
                fITypeId = sectordata.fITypeId;
                ownerShipTypeId = sectordata.ownerShipTypeId;
                sourceId = sectordata.sourceId;
                sourceDescription = sectordata.sourceDescription;
                leadName = sectordata.leadName;
                totalemployee = sectordata.totalemployee;
            }
            else
            {
                var leadAutoNumbers = await leadsService.GetLeadAutoNumberBySp();
                leadNumber = leadAutoNumbers.leadNumber;
            }
         
            var model = new LeadViewModel
            {
                leads = await leadsService.GetAllLeadsByuser(HttpContext.User.Identity.Name),
                sectors = await sectorService.GetAllSector(),
                companyGroups = await companyGroupService.GetAllCompanyGroup(),
                ownerShipTypes = await ownershipService.GetAllOwnership(),
                sources = await sourceService.GetAllSource(),
                fITypes = await bankBranchService.GetAllFIType(),
                leadsingal=leadsingal
                //leadAutoNumbers = await leadsService.GetLeadAutoNumberBySp()
            };
            if (model.companyGroups == null)
            {
                model.companyGroups = new List<CompanyGroup>();
            }
            ViewBag.leadName = leadName;
            ViewBag.leadId = Id;
            ViewBag.sectorId = sectorId;
            ViewBag.leadShortName = leadShortName;
            ViewBag.leadNumber = leadNumber;
            ViewBag.companyGroupId = companyGroupId;
            ViewBag.fITypeId = fITypeId;
            ViewBag.ownerShipTypeId = ownerShipTypeId;
            ViewBag.sourceId = sourceId;
            ViewBag.sourceDescription = sourceDescription;
            ViewBag.message = message;
            ViewBag.totalemployee = totalemployee;

            return View(model);
        }
        // POST: Lead/save/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LeadInfo([FromForm] LeadViewModel model)
        {
            var id = 0;
            if (!ModelState.IsValid)
            {
                model.leads = await leadsService.GetAllLeadsByuser(HttpContext.User.Identity.Name);
                return View(model);
            }
            var leadowner ="";
            if (model.leadId != 0)
            {
                leadowner = model.leadOwner;
            }
            else
            {
                leadowner = HttpContext.User.Identity.Name;
            }
            Leads data = new Leads
            {
                Id = model.leadId,
                leadOwner = leadowner,
                leadName = model.leadName,
                leadShortName = model.leadShortName,
                leadNumber = model.leadNumber,
                companyGroupId = model.companyGroupId,
                fITypeId = model.fITypeId,
                ownerShipTypeId = model.ownerShipTypeId,
                sourceId = model.sourceId,
                sourceDescription = model.sourceDescription,
                totalemployee = model.totalemployee,
                sectorId = model.sector,
                createdBy = HttpContext.User.Identity.Name,
                createdAt = DateTime.Now
            };

            id = await leadsService.SaveLeads(data);
            if (Convert.ToInt32(model.leadId) == 0)
            {
                RelSupplierCustomerResourse data2 = new RelSupplierCustomerResourse
                {
                    Id = 0,
                    LeadsId = id,
                    roleTypeId = 4

                };

                int relid = await customerService.SaveRelSupplierCustomerResourse(data2);

            }
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

            string message = "Save";
            return RedirectToAction(nameof(LeadInfo), new { id, model.leadName, message });
        }

        #endregion

        #region Lead List

        public IActionResult AllLeadList()
        {
            string LeadOwner = HttpContext.User.Identity.Name;
            LeadViewModel model = new LeadViewModel
            {
                // getLeadInfoListViewModels = await leadsService.GetLeadInfoAsQueryAble(LeadOwner)

            };
            //  var data = await leadsService.GetLeadInfoAsQueryAble(LeadOwner);
            return View(model);

        }
        public async Task<IActionResult> AllLeadListS()
        {
            string LeadOwner = HttpContext.User.Identity.Name;
            //LeadViewModel model = new LeadViewModel
            //{
            //    getLeadInfoListViewModels = await leadsService.GetLeadInfoAsQueryAble(LeadOwner)

            //};
            var data = await leadsService.GetLeadInfoAsQueryAble(LeadOwner);
            return Json(data.Where(x=>x.isPersonal!=1));

        }
        [HttpGet]
        public async Task<IActionResult> AllLeadListFilter(string TeamLeader, string Fa, string BD, string LeadId)
        {
            string Owner = HttpContext.User.Identity.Name;

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
            // var data = await agreementService.GetAgreementDetailsbyRatingDatefilter(FDate, TDate, Owner, TeamLeader, Fa, BD, LeadId);
           var data = await leadsService.GetLeadInfofilterAsQueryAble(Owner,TeamLeader,Fa,BD,LeadId);
            return Json(data);
        }

        public async Task<IActionResult> AllPersonLeadList()
        {
            string LeadOwner = HttpContext.User.Identity.Name;
            var leadList = await leadsService.GetLeadInfoAsQueryAble(LeadOwner);
         
            LeadViewModel model = new LeadViewModel
            {
                getLeadInfoListViewModels = leadList.Where(x => x.isPersonal == 1).ToList()

            };
            return View(model);

        }
        public async Task<IActionResult> AllPersonLeadListAl()
        {
            string LeadOwner = HttpContext.User.Identity.Name;
            var leadList = await leadsService.GetLeadInfoAsQueryAble("");
           
            LeadViewModel model = new LeadViewModel
            {
                getLeadInfoListViewModels = leadList.Where(x => x.isPersonal == 1).ToList()

            };
            return View(model);

        }
        public async Task<IActionResult> AllPersonLeadListA()
        {
            string LeadOwner = HttpContext.User.Identity.Name;

            var allcustomer=await oSalesInvoiceMasterService.GetAllSalesInvoiceMaster();
            var leadList = await leadsService.GetLeadInfoAsQueryAble("");
            var clientlist = await clientService.GetAllClients();
            List<int?> intclientlist = allcustomer.Select(x => x.relSupplierCustomerResourse.LeadsId).ToList();
            List<int?> disid = intclientlist.Distinct().ToList();
            leadList = leadList.Where(x =>intclientlist.Contains(x.Id));
            LeadViewModel model = new LeadViewModel
            {
                getLeadInfoListViewModels = leadList.Where(x => x.isPersonal == 1).ToList()

            };
            return View(model);

        }

        #endregion

        #region Contact

        public async Task<IActionResult> Contacts(int? id, string leadName)
        {
            var model = new ContactsViewModel
            {
                contacts = await contactsService.GetAllContactsByLeadsId(Convert.ToInt32(id)),
                departments = await designationDepartmentService.GetDepartment(),
                designations = await designationDepartmentService.GetDesignations()
            };
            ViewBag.leadName = leadName;
            ViewBag.leadId = id;
            string username = HttpContext.User.Identity.Name;
            var userinfo = await userInfo.GetUserInfoByUser(username);
            ViewBag.ContactOwner = userinfo.EmpName;
            return View(model);
        }

        public async Task<IActionResult> ContactsPersonal(int? id, string leadName)
        {
            var model = new ContactsViewModel
            {
                contacts = await contactsService.GetAllContactsByLeadsIdforPersonal(Convert.ToInt32(id)),
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
        public async Task<IActionResult> Contacts([FromForm] ContactsViewModel model, IFormFile imagePath, IFormFile imagePath_Vcard_Font, IFormFile imagePath_Vcard_Back)
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
            //if (model.id > 0)
            //{

            //}
            Resource resource = new Resource
            {
                Id = model.contactId,
                resourceName = model.contactName,
                phone = model.phone,
                mobile = model.mobile,
                otherPhone = model.otherPhone,
                email = model.email,
                age = model.age,
                gender = model.gender,
                alternativeEmail = model.alternativeEmail,
                imagePath = "",
                fax = model.fax,
                skypeId = model.skypeId,
                linkedInId = model.linkedInId,
                crmdesignationsId = model.designation,
                crmdepartmentsId = model.department
            };
            resourceId = await resourceService.SaveResourceReturnId(resource);
            //}              

            if (model.id == 0)
            {
                RelSupplierCustomerResourse data2 = new RelSupplierCustomerResourse
                {
                    Id =0,
                    resourceId = resourceId,
                    roleTypeId = 2

                };

                int relid = await customerService.SaveRelSupplierCustomerResourse(data2);

            }
            Contacts data = new Contacts
            {
                Id = model.id,
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
            var lead = await leadsService.GetLeadDetailsById(Convert.ToInt32(model.leadId));
            if(lead.isPersonal==1)
            {
                return RedirectToAction(nameof(ContactsPersonal), new
                {
                    id = Convert.ToInt32(model.leadId),
                    leadName = model.leadName
                });
            }
            else
            {
                return RedirectToAction(nameof(Contacts), new
                {
                    id = Convert.ToInt32(model.leadId),
                    leadName = model.leadName
                });
            }
          
        }



        [HttpGet]
        public async Task<IActionResult> GetContactVcard_FontByContactId(int Id)
        {
            return Json(await attachmentCommentService.GetDocumentAttachmentByActionIdContact(Id, "Contact", "Vcard_Font", 2));
        }

        [HttpGet]
        public async Task<IActionResult> GetContactphotoByContactId(int Id)
        {
            return Json(await attachmentCommentService.GetDocumentAttachmentByActionIdContact(Id, "Contact", "photo", 2));
        }
        [HttpGet]
        public async Task<IActionResult> GetContactVcard_BackByContactId(int Id)
        {
            return Json(await attachmentCommentService.GetDocumentAttachmentByActionIdContact(Id, "Contact", "Vcard_Back", 2));
        }

        #endregion

        #region API Section


        [AllowAnonymous]
        [Route("global/api/getAllLeadsByUserId")]
        [HttpGet]
        public async Task<IActionResult> getAllLeadsByUserId()
        {
            string LeadOwner = HttpContext.User.Identity.Name;
            return Json(await leadsService.GetLeadInfoAsQueryAble(LeadOwner));
        }

        [Route("global/api/GetLeadsByLeadOwner")]
        [HttpGet]
        public async Task<IActionResult> GetLeadsByLeadOwner()
        {
            string LeadOwner = HttpContext.User.Identity.Name;
            return Json(await leadsService.GetLeadsByLeadOwner(LeadOwner));
        }

        [Route("global/api/GetAllLeads")]
        [HttpGet]
        public async Task<IActionResult> GetAllLeads()
        {
            return Json(await leadsService.GetAllLeads());
        }

        [HttpGet]
        public async Task<IActionResult> GetLeadsById(int id)
        {
            return Json(await leadsService.GetLeadsById(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetLeadContactAddress(int leadId)
        {
            var data = await leadsService.GetLeadContactAddress(leadId);
            return Json(data.FirstOrDefault());
        }

        #endregion

        #region Leads Conversion
        public async Task<IActionResult> LeadsForConversion()
        {
            string LeadOwner = HttpContext.User.Identity.Name;
            LeadViewModel model = new LeadViewModel
            {
                getLeadsForConversionListViewModels = await leadsService.GetLeadsForConversion(LeadOwner)

            };
            return View(model);
        }
        public async Task<IActionResult> LeadsForReConversion()
        {
            string LeadOwner = HttpContext.User.Identity.Name;
            var bankdata = await bankBranchService.GetLeadsBankInfo();
            var data = await clientService.GetAllClients();
            var agreedata = await agreementService.GetAllAgreement();
            List<int?> leadids = agreedata.Where(x=>x.agreementStatusId==3 && x.agreementType.agreementTypeName!="Final").Select(x => x.leadsId).ToList();

            data = data.Where(x => leadids.Contains(x.leadsId) && x.isconverted != 1).ToList();
            LeadViewModel model = new LeadViewModel
            {
                clients = data,// await clientService.GetAllClients(),
                leadsBankInfos= bankdata.Where(x=>x.bankType=="Primary"),
                employeeInfos=await personalInfoService.GetEmployeeInfo()

            };
            return View(model);
        }
        public async Task<IActionResult> SaveLeadsReConversion(int Id, int leadstatusId, DateTime? receivedDate, string remarks)
        {
            var clientdata = await clientService.GetClientsById(Id);
            if (leadstatusId == 1)
            {
                //Clients data = new Clients
                //{
                //    Id = 0,
                //    leadsId = Convert.ToInt32(Id),
                //    isconverted = 1,
                //    isactive = 1,
                //    createdBy = HttpContext.User.Identity.Name,
                //    createdAt = DateTime.Now
                //};
                await clientService.UpdateReClient(Id);
                //await leadsService.UpdateLeads(Id, 3, remarks, HttpContext.User.Identity.Name);
                #region Save History            
              
                LeadsHistory leadsData = new LeadsHistory
                {
                    Id = 0,
                    leadsId = clientdata.leadsId,
                    actionName = "Leads Conversion",
                    actionDetails = "Leads Converted to Client"
                };
                await leadsService.SaveLeadsHistory(leadsData);
                #endregion

            }
            else
            {
                await leadsService.UpdateLeads(Id, 4, remarks, HttpContext.User.Identity.Name);
                #region Save History            

                LeadsHistory leadsData = new LeadsHistory
                {
                    Id = 0,
                    leadsId = Id,
                    actionName = "Leads not qualified",
                    actionDetails = "Leads not Converted to Client"
                };
                await leadsService.SaveLeadsHistory(leadsData);
                #endregion
            }



            try
            {
                string userName = HttpContext.User.Identity.Name;
                var userInfos = await userInfo.GetUserInfoByUser(userName);
                var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
                int empId = 0;
                string email = "";
                string name = "";
                if (EmpInfo != null)
                {
                    empId = EmpInfo.Id;
                    email = EmpInfo.emailAddress;
                    name = EmpInfo.nameEnglish;
                }

                var aggrementDetails = await leadsService.GetLeadDetailsById(clientdata.leadsId);
                string mesage = "";
                if (leadstatusId == 1)
                {
                    mesage = "is converted";
                }
                else
                {
                    mesage = "is not converted";
                }
                string html = "<div><strong>Lead verification</strong></div>"
                        + " <br/>"
                        + "<p>Dear Sir,</p>"
                        + " <br/>"
                        + " This is to inform you that " + aggrementDetails.leadName + " "+ mesage + " to client."
                        + "<br/>"
                        + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' >  " + name + " </p></div>"
                        + " <br/>";

                if (email != null)
                {
                    await emailSenderService.SendEmailWithFrom(aggrementDetails.leadOwner, email, "For Lead convertion :" + aggrementDetails.leadName, html);
                }
            }
            catch (Exception ex)
            {

                return RedirectToAction("LeadsForReConversion", "LeadManagement", new { id = 0, Area = "CRMLead" });
            }


            return RedirectToAction("LeadsForReConversion", "LeadManagement", new { id = 0, Area = "CRMLead" });
        }
        public async Task<IActionResult> SaveLeadsConversion(int Id, int leadstatusId, DateTime? receivedDate, string remarks)
        {
            if (leadstatusId == 1)
            {
                Clients data = new Clients
                {
                    Id = 0,
                    leadsId = Convert.ToInt32(Id),
                    isconverted=1,
                    isactive=1,
                    createdBy = HttpContext.User.Identity.Name,
                    createdAt = DateTime.Now
                };
                await clientService.SaveClients(data);
                await leadsService.UpdateLeads(Id, 3, remarks, HttpContext.User.Identity.Name);
                #region Save History            

                LeadsHistory leadsData = new LeadsHistory
                {
                    Id = 0,
                    leadsId = Id,
                    actionName = "Leads Conversion",
                    actionDetails = "Leads Converted to Client"
                };
                await leadsService.SaveLeadsHistory(leadsData);
                #endregion
                
            }
            else
            {
                await leadsService.UpdateLeads(Id, 4, remarks, HttpContext.User.Identity.Name);
                #region Save History            

                LeadsHistory leadsData = new LeadsHistory
                {
                    Id = 0,
                    leadsId = Id,
                    actionName = "Leads not qualified",
                    actionDetails = "Leads not Converted to Client"
                };
                await leadsService.SaveLeadsHistory(leadsData);
                #endregion
            }



            try
            {
                string userName = HttpContext.User.Identity.Name;
                var userInfos = await userInfo.GetUserInfoByUser(userName);
                var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
                int empId = 0;
                string email = "";
                string name = "";
                if (EmpInfo != null)
                {
                    empId = EmpInfo.Id;
                    email = EmpInfo.emailAddress;
                    name = EmpInfo.nameEnglish;
                }

                var aggrementDetails = await leadsService.GetLeadDetailsById(Id);
                string mesage = "";
                if (leadstatusId == 1)
                {
                    mesage = "is converted";
                }
                else
                {
                    mesage = "is not converted";
                }
                string html = "<div><strong>Lead verification</strong></div>"
                        + " <br/>"
                        + "<p>Dear Sir,</p>"
                        + " <br/>"
                        + " This is to inform you that " + aggrementDetails.leadName + " " + mesage + " to client."
                        + "<br/>"
                        + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' >  " + name + " </p></div>"
                        + " <br/>";

                if (email != null)
                {
                    await emailSenderService.SendEmailWithFrom(aggrementDetails.leadOwner, email, "For Lead convertion :" + aggrementDetails.leadName, html);
                }
            }
            catch (Exception ex)
            {

                return RedirectToAction("LeadsForConversion", "LeadManagement", new { id = 0, Area = "CRMLead" });
            }
            

            return RedirectToAction("LeadsForConversion", "LeadManagement", new { id = 0, Area = "CRMLead" });
        }

        #endregion

        #region Lead Details

        public async Task<IActionResult> LeadDetail(int Id)
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

                #region History Save               

                LeadsHistory leadsData = new LeadsHistory
                {
                    Id = 0,
                    leadsId = model.actionTypeId,
                    actionName = "Lead",
                    actionDetails = "Comments is Added."
                };
                await leadsService.SaveLeadsHistory(leadsData);

                #endregion

                return RedirectToAction("LeadDetail", "LeadManagement", new { id = model.actionTypeId, Area = "CRMLead" });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeleteComments(int Id, int actionId)
        {
            #region History Save 
            LeadsHistory leadsData = new LeadsHistory
            {
                Id = 0,
                leadsId = actionId,
                actionName = "Lead",
                actionDetails = "Comments is Deleted."
            };
            await leadsService.SaveLeadsHistory(leadsData);
            #endregion

            await attachmentCommentService.DeleteCommentById(Id);
            return RedirectToAction("LeadDetail", "LeadManagement", new { id = actionId, Area = "CRMLead" });
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
                        leadsId = model.actionTypeId,
                        actionName = "Lead",
                        actionDetails = "Document " + NewFileName + " is Uploaded."
                    };
                    await leadsService.SaveLeadsHistory(leadsData);
                    #endregion

                }



                return RedirectToAction("LeadDetail", "LeadManagement", new { id = model.actionTypeId, Area = "CRMLead" });
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
                        leadsId = model.actionTypeId,
                        actionName = "Lead",
                        actionDetails = "Photo " + NewFileName + " is Uploaded."
                    };
                    await leadsService.SaveLeadsHistory(leadsData);
                    #endregion
                }



                return RedirectToAction("LeadDetail", "LeadManagement", new { id = model.actionTypeId, Area = "CRMLead" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeletePhoto(int actionId, int photoId)
        {
            #region History Save 
            LeadsHistory leadsData = new LeadsHistory
            {
                Id = 0,
                leadsId = actionId,
                actionName = "Lead",
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
            return RedirectToAction("LeadDetail", "LeadManagement", new { id = actionId, Area = "CRMLead" });
        }

        #endregion
        public async Task<IActionResult> DeleteContact(int Id)
        {

            var leadbank = await contactsService.GetContactsById(Id);
            LeadsHistory leadsData = new LeadsHistory
            {
                Id = 0,
                leadsId = leadbank.leadsId,
                actionName = "Lead Contact",
                actionDetails = "Address is Deleted."
            };

            await contactsService.DeleteContactsById(Id);
            return RedirectToAction(nameof(Contacts), new
            {
                id = Convert.ToInt32(leadbank.leadsId),
                leadName = ""
            });
        }
        #region Leads History


        public async Task<IActionResult> LeadHistory(int? id, string leadName)
        {
            LeadsHistoryViewModel model = new LeadsHistoryViewModel()
            {
                leadsHistories = await leadsService.GetLeadsHistoryByLeadId(Convert.ToInt32(id))
            };
            ViewBag.leadName = leadName;
            ViewBag.leadId = id;
            return View(model);
        }

        public IActionResult AccountHistory()
        {
            LeadsHistoryViewModel model = new LeadsHistoryViewModel()
            {
                // leadsHistories = await leadsService.GetAllLeadsHistory()
            };
            //ViewBag.leadName = leadName;
            //ViewBag.leadId = id;
            return View(model);
        }
        public async Task<IActionResult> SurveillanceHistory()
        {
            LeadsHistoryViewModel model = new LeadsHistoryViewModel()
            {
                leadsHistories = await leadsService.GetAllLeadsHistory()
            };
            //ViewBag.leadName = leadName;
            //ViewBag.leadId = id;
            return View(model);
        }

        public async Task<IActionResult> LeadHistoryPersonal(int? id, string leadName)
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

        #region Lead Files
        public async Task<IActionResult> LeadFile(int id, string leadName)
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
                var lead = await leadsService.GetLeadDetailsById(id);
                if (lead != null)
                {
                    ViewBag.leadName = lead.leadName;
                    ViewBag.leadId = id;
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> LeadFilePersonal(int id, string leadName)
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
                var lead = await leadsService.GetLeadDetailsById(id);
                if (lead != null)
                {
                    ViewBag.leadName = lead.leadName;
                    ViewBag.leadId = id;
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public async Task<IActionResult> SaveDocLeadFile([FromForm] DocumentAttachmentViewModel model, IFormFile formFile)
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

                return RedirectToAction("LeadFile", "LeadManagement", new { id = model.actionTypeId, Area = "CRMLead" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public async Task<IActionResult> SavePhotoLeadFile([FromForm] DocumentAttachmentViewModel model, IFormFile formFile)
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

                return RedirectToAction("LeadFile", "LeadManagement", new { id = model.actionTypeId, Area = "CRMLead" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeletePhotoLeadFile(int actionId, int photoId)
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
            return RedirectToAction("LeadFile", "LeadManagement", new { id = actionId, Area = "CRMLead" });
        }
        #endregion

        #region Lead Bank
        public async Task<IActionResult> LeadBankList(int id, string leadName)
        {

            var model = new LeadBankViewModel
            {
                leadsBankInfos = await bankBranchService.GetLeadsBankInfoByLeadId(id)
            };
            var lead = await leadsService.GetLeadDetailsById(id);
            ViewBag.leadName = leadName; //lead.leadName;
            ViewBag.leadId = id;
            return View(model);
        }

        public async Task<IActionResult> LeadBankListPersonal(int id, string leadName)
        {

            var model = new LeadBankViewModel
            {
                leadsBankInfos = await bankBranchService.GetLeadsBankInfoByLeadId(id)
            };
            var lead = await leadsService.GetLeadDetailsById(id);
            ViewBag.leadName = leadName; //lead.leadName;
            ViewBag.leadId = id;
            return View(model);
        }


        public async Task<IActionResult> LeadBankInfo(int? id, int leadId, string leadName)
        {
            var model = new LeadBankViewModel
            {
                //contacts = await contactsService.GetAllContactsByLeadsId(Convert.ToInt32(id)),
                banks = await bankBranchService.GetAllBank(),
                bankBranches = await bankBranchService.GetAllBankBranch(),
                bankBranchDetails = await bankBranchService.GetAllBankBranchDetails(),
                departments = await designationDepartmentService.GetDepartment(),
                designations = await designationDepartmentService.GetDesignations(),
                fITypes = await bankBranchService.GetAllFIType(),
                leadsBankInfos=await bankBranchService.GetLeadsBankInfoByLeadId(leadId)
            };
            var lead = await leadsService.GetLeadDetailsById(leadId);
            if (lead != null)
            {
                ViewBag.leadName = lead.leadName;
                ViewBag.leadId = leadId;
                ViewBag.masterId = id;
            }
            return View(model);
        }

        public async Task<IActionResult> LeadBankInfoPersonal(int? id, int leadId, string leadName)
        {
            var model = new LeadBankViewModel
            {
                //contacts = await contactsService.GetAllContactsByLeadsId(Convert.ToInt32(id)),
                banks = await bankBranchService.GetAllBank(),
                bankBranches = await bankBranchService.GetAllBankBranch(),
                bankBranchDetails = await bankBranchService.GetAllBankBranchDetails()
            };
            var lead = await leadsService.GetLeadDetailsById(leadId);
            if (lead != null)
            {
                ViewBag.leadName = lead.leadName;
                ViewBag.leadId = leadId;
                ViewBag.masterId = id;
            }
            return View(model);
        }

        // POST: Thana/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LeadBankInfo([FromForm] LeadBankViewModel model)
        {
            LeadsBankInfo data = new LeadsBankInfo
            {
                Id = model.Id,
                leadsId = model.leadsId,
                bankBranchDetailsId = model.bankBranchDetailsId,
                bankType = model.bankType,
                contactName=model.contactName,
                crmdepartmentsId=model.department,
                crmdesignationsId=model.designation,
                mobile=model.mobile,
                email=model.email
            };

            await bankBranchService.SaveLeadBank(data);
            #region Save History            
            string actDetailss = string.Empty;
            if (model.Id == 0)
            {
                actDetailss = model.bankType + " Bank is Created.";
            }
            else
            {
                actDetailss = model.bankType + " Bank is Updated.";
            }
            LeadsHistory leadsData = new LeadsHistory
            {
                Id = 0,
                leadsId = model.leadsId,
                actionName = "Bank",
                actionDetails = actDetailss
            };
            await leadsService.SaveLeadsHistory(leadsData);
            #endregion

            if (model.isPersonal == 1)
            {
                return RedirectToAction(nameof(LeadBankListPersonal), new
                {
                    id = Convert.ToInt32(model.leadsId),
                    leadName = model.leadName
                });
            }
            else
            {
                return RedirectToAction(nameof(LeadBankList), new
                {
                    id = Convert.ToInt32(model.leadsId),
                    leadName = model.leadName
                });
            }


        }
       


        public async Task<JsonResult> GetBranchByBankId(int bankId)
        {
            var result = await bankBranchService.GetBranchByBankId(bankId);
            return Json(result);
        }
        public async Task<JsonResult> GetBankByFIId(int Id)
        {
            var result = await bankBranchService.GetAllBank();
            return Json(result);
        }
        public async Task<JsonResult> GeLeadsBankInfoById(int Id)
        {
            var result = await bankBranchService.GeLeadsBankInfoById(Id);
            return Json(result);
        }
       
        #endregion

        #region Client Bank
        public async Task<IActionResult> ClientBankList(int id, string leadName)
        {

            var model = new LeadBankViewModel
            {
                leadsBankInfos = await bankBranchService.GetLeadsBankInfoByLeadId(id)
            };
            var lead = await leadsService.GetLeadDetailsById(id);
            ViewBag.leadName = lead.leadName;
            ViewBag.leadId = id;
            return View(model);
        }
        [HttpGet]

        public async Task<IActionResult> DeleteLeadsBank(int Id)
        {
          
            var leadbank = await bankBranchService.GeLeadsBankInfoById(Id);
            LeadsHistory leadsData = new LeadsHistory
            {
                Id = 0,
                leadsId = leadbank.leadsId,
                actionName = "Lead Bank",
                actionDetails = leadbank.bankBranchDetails.bank + "-" + leadbank.bankBranchDetails.bankBranch + " is Deleted."
            };
            await bankBranchService.DeleteLeadsBankInfoById(Id);
            return RedirectToAction(nameof(LeadBankList), new
            {
                id = Convert.ToInt32(leadbank.leadsId),
                leadName = leadbank.leads.leadName
            });
        }
        public async Task<IActionResult> ClientBankInfo(int? id, int leadId, string leadName)
        {
            var model = new LeadBankViewModel
            {
                banks = await bankBranchService.GetAllBank(),
                bankBranches = await bankBranchService.GetAllBankBranch(),
                bankBranchDetails = await bankBranchService.GetAllBankBranchDetails()
            };
            var lead = await leadsService.GetLeadDetailsById(leadId);
            ViewBag.leadName = lead.leadName;
            ViewBag.leadId = leadId;
            ViewBag.masterId = id;
            return View(model);
        }
        // POST: Thana/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClientBankInfo([FromForm] LeadBankViewModel model)
        {
            LeadsBankInfo data = new LeadsBankInfo
            {
                Id = model.Id,
                leadsId = model.leadsId,
                bankBranchDetailsId = model.bankBranchDetailsId,
                bankType = model.bankType,
            };

            await bankBranchService.SaveLeadBank(data);
            #region Save History            
            string actDetailss = string.Empty;
            if (model.Id == 0)
            {
                actDetailss = model.bankType + " Bank is Created.";
            }
            else
            {
                actDetailss = model.bankType + " Bank is Updated.";
            }
            LeadsHistory leadsData = new LeadsHistory
            {
                Id = 0,
                leadsId = model.leadsId,
                actionName = "Bank",
                actionDetails = actDetailss
            };
            await leadsService.SaveLeadsHistory(leadsData);
            #endregion
            return RedirectToAction(nameof(ClientBankList), new
            {
                id = Convert.ToInt32(model.leadsId),
                leadName = model.leadName
            });

        }

        #endregion

        #region Crm Contacts

        public async Task<IActionResult> CrmContacts(int? id)
        {
            var model = new ContactsViewModel
            {
                departments = await designationDepartmentService.GetDepartment(),
                designations = await designationDepartmentService.GetDesignations()
            };
            ViewBag.leadId = id;
            return View(model);
        }

        public async Task<IActionResult> CrmContactsList(int? id)
        {
            var model = new ContactsViewModel
            {
                //departments = await designationDepartmentService.GetDepartment(),
                //designations = await designationDepartmentService.GetDesignations()
            };
            ViewBag.leadId = id;
            return View(model);
        }
        public async Task<IActionResult> CrmbankContactsList(int? id)
        {
            var model = new ContactsViewModel
            {
                //departments = await designationDepartmentService.GetDepartment(),
                //designations = await designationDepartmentService.GetDesignations()
            };
            ViewBag.leadId = id;
            return View(model);
        }

        // POST: Contact/save/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrmContacts([FromForm] ContactsViewModel model, IFormFile imagePath, IFormFile imagePath_Vcard_Font, IFormFile imagePath_Vcard_Back)
        {
            if (!ModelState.IsValid)
            {
                model.departments = await designationDepartmentService.GetDepartment();
                model.designations = await designationDepartmentService.GetDesignations();
                ViewBag.leadId = Convert.ToInt32(model.leadId);
                return View(model);
            }

            int resourceId = 0;

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

            //return RedirectToAction(nameof(CrmContacts));
            return RedirectToAction(nameof(CrmContacts), new
            {
                id = Convert.ToInt32(model.leadId)
            });
        }


        [HttpGet]
        public async Task<IActionResult> GetAllContactsByLeadsId(int? leadId)
        {
            return Json(await contactsService.GetAllContactsByLeadsId(Convert.ToInt32(leadId)));
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAllContacts()
        //{
        //    var data = await contactsService.GetContactsSpViewModel();
        //    return Json(data);
        //}
        [HttpGet]
        public async Task<IActionResult> GetAllContactsfilter(string TeamLeader, string Fa, string BD, string LeadId)
        {
            if (TeamLeader == "NoData")
            {
                TeamLeader = "";
            }
            if (Fa == "NoData")
            {
                Fa = "0";
            }
            if (BD == "NoData")
            {
                BD = "";
            }

            if (LeadId == "NoData")
            {
                LeadId = "";
            }
            var data = await contactsService.GetContactsSpViewModel(TeamLeader,Convert.ToInt32(Fa),BD,LeadId);
           
           
            return Json(data);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllbankContactsfilter(string TeamLeader, string Fa, string BD, string LeadId,string bank,string branch)
        {
            if (TeamLeader == "NoData")
            {
                TeamLeader = "";
            }
            if (Fa == "NoData")
            {
                Fa = "0";
            }
            if (BD == "NoData")
            {
                BD = "";
            }

            if (LeadId == "NoData")
            {
                LeadId = "";
            }
            if (bank == "NoData")
            {
                bank = "0";
            }
            if (branch == "NoData")
            {
                branch = "0";
            }
            var data = await contactsService.GetBankContactsSpViewModel(TeamLeader,Convert.ToInt32(Fa),BD,LeadId, Convert.ToInt32(bank), Convert.ToInt32(branch));
           
           
            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDesignation()
        {
            return Json(await designationDepartmentService.GetDesignations());
        }
        [HttpGet]
        public async Task<IActionResult> GetAllLeadsHistorySP()
        {
            return Json(await leadsService.GetAllLeadsHistorySP());
        }
        [HttpGet]
        public async Task<IActionResult> GetAllLeadsHistorySPfilter(string BD,string leadId)
        {
          
          
            if (BD == "NoData")
            {
                BD = "";
            }
            if (leadId == "NoData")
            {
                leadId = "";
            }
            var data = await leadsService.GetAllLeadsHistorySPfilter(BD,leadId);
            return Json(data);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllLeadsHistorySurSP()
        {
            return Json(await leadsService.GetAllLeadsHistorySurSP());
        }
        [HttpGet]
        public async Task<IActionResult> GetchangeOwnerLogShowViewModelsSP()
        {
            return Json(await leadsService.GetchangeOwnerLogShowViewModelsSP());
        }

        #endregion


    }
}