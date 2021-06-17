using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.CRO.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Services.Lead.Interfaces;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.CRO.Data.Entity.DistributeJob;
using OPUSERP.CRO.Services.DistributeJob.Interfaces;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPService.MasterData.Interfaces;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
//using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Employee.Interfaces;

namespace OPUSERP.Areas.CRO.Controllers
{
    [Area("CRO")]
    public class JobProgressController : Controller
    {
        private readonly IDistributeJobService distributeJobService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUserInfoes userInfo;
        private readonly IAttachmentCommentService attachmentCommentService;
        private readonly string rootPath;
        private readonly MyPDF myPDF;
        private readonly IERPCompanyService eRPCompanyService;
        private readonly OPUSERP.CRM.Services.Lead.Interfaces.IAddressesService addressService;
        private readonly IContactsService contactsService;
        private readonly IBankBranchService bankBranchService;
        private readonly ITeamService teamService;
        private readonly IAgreementService agreementService;
        private readonly ILeadsService leadsService;
        //private readonly IAddressesService addressesService;
        private readonly IPersonalInfoService personalInfoService;

        public JobProgressController(IHostingEnvironment hostingEnvironment, IPersonalInfoService personalInfoService, IAgreementService agreementService, ILeadsService leadsService, ITeamService teamService, IBankBranchService bankBranchService, IContactsService contactsService, OPUSERP.CRM.Services.Lead.Interfaces.IAddressesService addressService, IERPCompanyService eRPCompanyService, IDistributeJobService distributeJobService, IUserInfoes userInfo, IAttachmentCommentService attachmentCommentService, IConverter converter)
        {           
            this.distributeJobService = distributeJobService;
            this._hostingEnvironment = hostingEnvironment;
            this.userInfo = userInfo;
            this.attachmentCommentService = attachmentCommentService;
            this.eRPCompanyService = eRPCompanyService;
            this.addressService = addressService;
            this.contactsService = contactsService;
            this.bankBranchService = bankBranchService;
            this.teamService = teamService;
            this.personalInfoService = personalInfoService;
            this.agreementService = agreementService;
            this.leadsService = leadsService;
            //For PDF
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }
        public ActionResult HoldList()
        {
            return View();
        }
        public ActionResult BlockList()
        {
            return View();
        }
        public ActionResult UnBlockList()
        {
            return View();
        }
        public ActionResult RatingList()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult CheckListPdf(int Id)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;

            url = scheme + "://" + host + "/CRO/JobProgress/CheckList?Id=" + Id;

            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
        [AllowAnonymous]
        public IActionResult CheckListUPdf(int Id,int croId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;

            url = scheme + "://" + host + "/CRO/JobProgress/CheckListU?Id=" + Id+"&&croId="+ croId;

            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        public async Task<IActionResult> LeadInCroPreview(int id)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            LeadInCroViewModel model = new LeadInCroViewModel
            {
                statusLogs = await distributeJobService.GetStatusLogByOperationMasterId(id),
                receiveDocuments = await distributeJobService.GetAllReceiveDocumentByOperMstid(id),
                croAttachmentViewModels = await attachmentCommentService.GetCroDocumentAttachment(id, "Document", 13),
                getLeadInfoInCroViewModels = await distributeJobService.GetLeadInfoInCroByOPMstId(id)
            };
            return View(model);
        }

        public IActionResult LeadInCroPreviewPdf(int Id)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;

            url = scheme + "://" + host + "/CRO/JobProgress/LeadInCroPrintview?Id=" + Id;

            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        public async Task<IActionResult> LeadInCroPrintview(int id)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            LeadInCroViewModel model = new LeadInCroViewModel
            {
                statusLogs = await distributeJobService.GetStatusLogByOperationMasterId(id),
                receiveDocuments = await distributeJobService.GetAllReceiveDocumentByOperMstid(id),
                croAttachmentViewModels = await attachmentCommentService.GetCroDocumentAttachment(id, "Document", 13),
                getLeadInfoInCroViewModels = await distributeJobService.GetLeadInfoInCroByOPMstId(id)
            };
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult IRCMMData(int Id)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;

            url = scheme + "://" + host + "/CRO/JobProgress/IRCMMPdf?Id=" + Id;

            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        public async Task<IActionResult> CheckList(int Id)
        {

            var companies = await eRPCompanyService.GetAllCompany();
            var opmaster = await distributeJobService.GetOperationMasterById(Id);
            var address = await addressService.GetAddressesByLeadId((int)opmaster.agreement.leadsId);
            if (address == null)
            {
                address = new List<Address>();
            }
            var contacts = await contactsService.GetAllContactsbyLeadId((int)opmaster.agreement.leadsId);
            var teamTL = await personalInfoService.GetEmployeeInfoByCode(opmaster.assignTeamLeader);
            var teamFA = await personalInfoService.GetEmployeeInfoByCode(opmaster.assignTo);
            ViewBag.Leader = teamTL?.nameEnglish;
            ViewBag.FA = teamFA?.nameEnglish;
            CheckListViewModel model = new CheckListViewModel()
            {
                operationMaster = opmaster,
                company=companies.LastOrDefault(),
                aspNetUsersViewModel= await userInfo.GetUserInfoByUser(opmaster.agreement.leads.leadOwner),
                Address=address.FirstOrDefault(),
                contacts=contacts.FirstOrDefault(),
                leadsBankInfo=await bankBranchService.GetLeadsBankInfoByLeadId((int)opmaster.agreement.leadsId),
                contactlist= contacts,                
               

            };

           
            return View(model);
        }

        public async Task<IActionResult> CheckListU(int Id,int croId)
        {
            var agreement = await agreementService.GetAgreementById(Id);
            var companies = await eRPCompanyService.GetAllCompany();
            var opmaster = await distributeJobService.GetOperationMasterById(0);
            var address = await addressService.GetAddressesByLeadId((int)agreement.leadsId);
            if (opmaster == null)
            {
                opmaster = new OperationMaster();
            }
            if (address == null)
            {
                address = new List<Address>();
            }
            var contacts = await contactsService.GetAllContactsbyLeadId((int)agreement.leadsId);
            //var teamTL = await personalInfoService.GetEmployeeInfoByCode(str);
            //var teamFA = await personalInfoService.GetEmployeeInfoByCode('');
            ViewBag.Leader = "";
            ViewBag.FA = "";
            CheckListViewModel model = new CheckListViewModel()
            {
                operationMaster = opmaster,
                company = companies.LastOrDefault(),
                aspNetUsersViewModel = await userInfo.GetUserInfoByUser(agreement.leads.leadOwner),
                Address = address.FirstOrDefault(),
                contacts = contacts.FirstOrDefault(),
                leadsBankInfo = await bankBranchService.GetLeadsBankInfoByLeadId((int)agreement.leadsId),
                contactlist = contacts,
                agreement = agreement,
                approvedforCro=await agreementService.GetApprovedforCroById(croId)


            };


            return View(model);
        }

        public async Task<IActionResult> IRCMMPdf(int Id)
        {
            var companies = await eRPCompanyService.GetAllCompany();
            var opmaster = await distributeJobService.GetOperationMasterById(Id);
            var address = await addressService.GetAddressesByLeadId((int)opmaster.agreement.leadsId);
            if (address == null)
            {
                address = new List<Address>();
            }
            var contacts = await contactsService.GetAllContactsbyLeadId((int)opmaster.agreement.leadsId);
            var teamTL = await personalInfoService.GetEmployeeInfoByCode(opmaster.assignTeamLeader);
            var teamFA = await personalInfoService.GetEmployeeInfoByCode(opmaster.assignTo);
            ViewBag.Leader = teamTL?.nameEnglish;
            ViewBag.FA = teamFA?.nameEnglish;
            ViewBag.Designation = teamFA?.designation;
            CheckListViewModel model = new CheckListViewModel()
            {
                operationMaster = opmaster,
                company = companies.LastOrDefault(),
                aspNetUsersViewModel = await userInfo.GetUserInfoByUser(opmaster.assignTo),
                Address = address.FirstOrDefault(),
                contacts = contacts.FirstOrDefault(),
                leadsBankInfo = await bankBranchService.GetLeadsBankInfoByLeadId((int)opmaster.agreement.leadsId),
                contactlist = contacts,
                proposedRating=await distributeJobService.GetProposedRatingByOpMstId(Id),
                iRCMeetingAttendances = await distributeJobService.GetAllIRCMeetingAttendanceByOperMstid(Id),
                iRCMemberComments = await distributeJobService.GetAllIRCMemberCommentsByOperMstid(Id),
                iRCRating = await distributeJobService.GetIRCRatingByOpMstId(Id),
                IRCSignatories=await distributeJobService.GetAllIRCSignatory(),
                archives = await distributeJobService.GetArchiveByOperationMasterId(Id)

            };

            // ViewBag.opMstId = mstid;
            return View(model);
        }

        public async Task<IActionResult> Index(int? opMstId)
        {
            int? mstid = 0;
            if(opMstId==null || opMstId==0)
            {
                mstid = 0;
            }
            else
            {
                mstid = opMstId;
            }
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            //var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos.EmpCode);
            string empCode = "";
            if (empCode != null)
            {
                empCode = userInfos.EmpCode;
            }
           
            AttachmentStatusViewModel model = new AttachmentStatusViewModel()
            {
                //operationMasters = await distributeJobService.GetOperationMasterByAssignTo(empCode),
                getOMByassignToReviewerViewModels = await distributeJobService.GetOperationMasterByAssignToBySp(empCode),
                attachmentTypes = await distributeJobService.GetAllAttachmentType(),
                documentCharts = await distributeJobService.GetAllDocumentChart(),
                jobStatuses = await distributeJobService.GetAllJobStatus(),
                ratingValues = await distributeJobService.GetAllRatingValue(),
                 employeeInfos = await personalInfoService.GetEmployeeInfo()
            };

            ViewBag.opMstId = mstid;
            return View(model);
        }
        public async Task<IActionResult> ClosedList(int? opMstId)
        {
            int? mstid = 0;
            if(opMstId==null || opMstId==0)
            {
                mstid = 0;
            }
            else
            {
                mstid = opMstId;
            }
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            //var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos.EmpCode);
            string empCode = "";
            if (empCode != null)
            {
                empCode = userInfos.EmpCode;
            }
           
            AttachmentStatusViewModel model = new AttachmentStatusViewModel()
            {
                //operationMasters = await distributeJobService.GetOperationMasterByAssignTo(empCode),
                getOMByassignToClosedViewModels = await distributeJobService.GetOMByassignToClosedViewModels(empCode),
                attachmentTypes = await distributeJobService.GetAllAttachmentType(),
                documentCharts = await distributeJobService.GetAllDocumentChart(),
                jobStatuses = await distributeJobService.GetAllJobStatus(),
                ratingValues = await distributeJobService.GetAllRatingValue()
            };

            ViewBag.opMstId = mstid;
            return View(model);
        }


        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 409715200)]
        [RequestSizeLimit(409715200)]
        public async Task<IActionResult> Index([FromForm] AttachmentStatusViewModel model,IFormFile imagePath)
        {
            string NewFileName = string.Empty;
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            //var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos.EmpCode);
            string empCode = "";
            if (empCode != null)
            {
                empCode = userInfos.EmpCode;
            }
            if (!ModelState.IsValid)
            {
                model.operationMasters = await distributeJobService.GetOperationMasterByAssignTo(empCode);
                model.attachmentTypes = await distributeJobService.GetAllAttachmentType();
                model.documentCharts = await distributeJobService.GetAllDocumentChart();
                model.jobStatuses = await distributeJobService.GetAllJobStatus();

                return View(model);
            }

            AttachmentStatus data = new AttachmentStatus
            {
                Id = model.attachmentStatusId,
                operationMasterId = model.operationMasterId,
                attachmentTypeId = model.attachmentTypeId,
                fileTypeName = model.fileTypeName,
                subjectName=model.subjectName
            };

            int attachId = await distributeJobService.SaveAttachmentStatus(data);

            if (imagePath != null)
            {                
                string documentType = "Document";
                var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                var fileName = ContentDispositionHeaderValue.Parse(imagePath.ContentDisposition).FileName;
                string fileType = Path.GetExtension(fileName);
                fileName = fileName.Contains("\\")
                    ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                    : fileName.Trim('"');

                NewFileName = attachId + "_" + documentType + "_" + fileName;
                string fileLocation = "/Upload/Photo/" + NewFileName;
                var fullFilePath = Path.Combine(filesPath, NewFileName);

                using (var stream = new FileStream(fullFilePath, FileMode.Create))
                {
                    await imagePath.CopyToAsync(stream);
                }

                DocumentPhotoAttachment dataPH = new DocumentPhotoAttachment
                {
                    Id = 0,
                    actionType = "CRO My Job",
                    actionTypeId = model.operationMasterId,
                    subject = model.subjectName,
                    fileName = NewFileName,
                    filePath = fileLocation,
                    fileType = fileType,
                    description = attachId.ToString(),
                    documentType = documentType,
                    moduleId = 13,
                    //createdBy = userName
                };
                await attachmentCommentService.SaveDocumentAttachment(dataPH);
            }

            StatusLog statusdata = new StatusLog
            {
                Id = 0,
                operationMasterId = Convert.ToInt32(model.operationMasterId),
                jobStatusId = 2061,
                currentUser = HttpContext.User.Identity.Name,
                remarks = model.subjectName + "-" + NewFileName
            };

            int DocumentId = await distributeJobService.SaveStatusLog(statusdata);
            
            return RedirectToAction(nameof(Index), new
            {
                opMstId = model.operationMasterId
            });
            //return Json(1);
        }

        [HttpGet]
        public async Task<IActionResult> GetDocumentsByOperationMasterId(int Id)
        {
            return Json(await attachmentCommentService.GetCroDocumentAttachment(Id, "Document", 13));
        }

        [HttpPost]
        public async Task<IActionResult> SaveReceiveDocument([FromForm] AttachmentStatusViewModel model)
        {
            try
            {               
                ReceiveDocument data = new ReceiveDocument
                {
                    Id = Convert.ToInt32(model.receiveDocumentId),
                    operationMasterId = model.operationMasterIdDoc,
                    documentChartId = model.documentChartId,
                    description=model.description,
                    receiveDate=model.receiveDate,
                    archieveId = 0
                };

                int DocumentId = await distributeJobService.SaveReceiveDocument(data);
                StatusLog statusdata = new StatusLog
                {
                    Id = 0,
                    operationMasterId = Convert.ToInt32(model.operationMasterIdDoc),
                    jobStatusId = 7,
                    currentUser = HttpContext.User.Identity.Name,
                    remarks = "Received Documents"
                };

                 await distributeJobService.SaveStatusLog(statusdata);
               // return Json(DocumentId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction(nameof(Index), new
            {
                opMstId = model.operationMasterId
            });

        }

        [HttpGet]
        public async Task<IActionResult> GetReceiveDocumentsByOperationMasterId(int Id)
        {
            //return Json(await distributeJobService.GetAllReceiveDocumentByOperMstid(Id));
            return Json(await attachmentCommentService.GetCroDocumentsViewModels(Id));
        }
        [HttpGet]
        public async Task<IActionResult> UpdateOperationMasterbyId(int Id)
        {
            await distributeJobService.UpdateOperationMaster(Id, 2051, HttpContext.User.Identity.Name);
            StatusLog log = new StatusLog();

            log.Id = 0;
            log.operationMasterId = Id;
            log.jobStatusId = 2051;
            log.remarks ="Job Un hold";
            log.currentUser = HttpContext.User.Identity.Name;
            log.createdAt = DateTime.Now;
            log.createdBy = HttpContext.User.Identity.Name;
            await distributeJobService.SaveStatusLog(log);
            // return RedirectToAction("HoldList", "JobProgress", new {Area = "CRO" });
            return Json(Id);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateOperationMasterreassignbyId(int Id)
        {
            await distributeJobService.UpdateOperationMaster(Id, 43, HttpContext.User.Identity.Name);
            StatusLog log = new StatusLog();

            log.Id = 0;
            log.operationMasterId = Id;
            log.jobStatusId = 43;
            log.remarks = "Job Un hold";
            log.currentUser = HttpContext.User.Identity.Name;
            log.createdAt = DateTime.Now;
            log.createdBy = HttpContext.User.Identity.Name;
            await distributeJobService.SaveStatusLog(log);
            // return RedirectToAction("HoldList", "JobProgress", new {Area = "CRO" });
            return Json(Id);
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

            #region
            StatusLog statusdata = new StatusLog
            {
                Id = 0,
                operationMasterId = Convert.ToInt32(actionId),
                jobStatusId = 2062,
                currentUser = HttpContext.User.Identity.Name,
                remarks = data.fileName
            };
            int DocumentId = await distributeJobService.SaveStatusLog(statusdata);
            #endregion

            return RedirectToAction("Index", "JobProgress", new { opMstId = actionId, Area = "CRO" });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteReceiveDocument(int operMstId, int Id)
        {
            //var data = await distributeJobService.GetReceiveDocumentById(Id);       
            await distributeJobService.DeleteReceiveDocumentById(Id);

          
            StatusLog statusdata = new StatusLog
            {
                Id = 0,
                operationMasterId = Convert.ToInt32(operMstId),
                jobStatusId = 2063,
                currentUser = HttpContext.User.Identity.Name,
                remarks ="Received document deleted"
            };
            int DocumentId = await distributeJobService.SaveStatusLog(statusdata);
             return RedirectToAction(nameof(Index), new
            {
                opMstId = Convert.ToInt32(operMstId)
            });
        }

        [HttpPost]
        public async Task<JsonResult> SaveStatusLog([FromForm] AttachmentStatusViewModel model)
        {
            try
            {
                StatusLog data = new StatusLog
                {
                    Id = 0,
                    operationMasterId = Convert.ToInt32(model.operationMasterIdJob),
                    jobStatusId = model.jobStatusId,
                    currentUser = HttpContext.User.Identity.Name,
                    remarks = model.remarks
                };

                int DocumentId = await distributeJobService.SaveStatusLog(data);

                if (model.jobStatusId == 20)
                {
                    await distributeJobService.DeleteProposedRatingByOperMasterId(Convert.ToInt32(model.operationMasterIdJob));

                    ProposedRating prating = new ProposedRating
                    {
                        Id = 0,
                        operationMasterId = Convert.ToInt32(model.operationMasterIdJob),
                        proposedIRCRatingTypeName = model.proposedIRCRatingTypeName,
                        proposedIRCRatingValue = model.proposedIRCRatingValue,
                        proposedIRCShortRatingName = model.proposedIRCShortRatingName,
                        proposedIRCOutlook = model.proposedIRCOutlook
                    };

                    int ProposedRatingId = await distributeJobService.SaveProposedRating(prating);

                }

                var masterId = await distributeJobService.UpdateOperationMaster(model.operationMasterIdJob, model.jobStatusId,model.reportNo,model.reportDate);

                return Json(masterId);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetStatusLogByOperationMasterId(int Id)
        {
            //return Json(await distributeJobService.GetStatusLogByOperationMasterId(Id));
            return Json(await attachmentCommentService.GetCroStatusViewModels(Id));
        }


        [HttpGet]
        public async Task<IActionResult> GetOperationmasterhdata()
        {
            string fromdate = string.Empty;
            string todate = string.Empty;
            return Json(await distributeJobService.OperationMasterSPHoldViewModels(fromdate, todate));
        }
        [Route("global/api/GetOperationmasterhdata/{fromDate}/{toDate}")]
        [HttpGet]
        public async Task<IActionResult> GetOperationmasterhdata(string fromdate, string todate)
        {
            if (fromdate != null)
            {
                fromdate = Convert.ToDateTime(fromdate).ToString("yyyyMMdd");
                todate = Convert.ToDateTime(todate).ToString("yyyyMMdd");
            }
            else
            {
                fromdate = string.Empty;
                todate = string.Empty;
            }
            return Json(await distributeJobService.OperationMasterSPHoldViewModels(fromdate, todate));
        }

        [HttpGet]
        public async Task<IActionResult> GetOperationmasterblockdata()
        {
            string fromdate = string.Empty;
            string todate = string.Empty;
            return Json(await distributeJobService.OperationMasterSPBlockViewModels(fromdate, todate, 41));
        }
        [Route("global/api/GetOperationmasterbockdata/{fromDate}/{toDate}")]
        [HttpGet]
        public async Task<IActionResult> GetOperationmasterbockdata(string fromdate, string todate)
        {
            if (fromdate != null)
            {
                fromdate = Convert.ToDateTime(fromdate).ToString("yyyyMMdd");
                todate = Convert.ToDateTime(todate).ToString("yyyyMMdd");
            }
            else
            {
                fromdate = string.Empty;
                todate = string.Empty;
            }
            return Json(await distributeJobService.OperationMasterSPBlockViewModels(fromdate, todate,41));
        }

        [HttpGet]
        public async Task<IActionResult> GetOperationmasterratedata()
        {
            string fromdate = string.Empty;
            string todate = string.Empty;
            return Json(await distributeJobService.OperationMasterSPRateViewModels(fromdate, todate));
        }
        [Route("global/api/GetOperationmasterratedata/{fromDate}/{toDate}")]
        [HttpGet]
        public async Task<IActionResult> GetOperationmasterratedata(string fromdate, string todate)
        {
            if (fromdate != null)
            {
                fromdate = Convert.ToDateTime(fromdate).ToString("yyyyMMdd");
                todate = Convert.ToDateTime(todate).ToString("yyyyMMdd");
            }
            else
            {
                fromdate = string.Empty;
                todate = string.Empty;
            }
            return Json(await distributeJobService.OperationMasterSPRateViewModels(fromdate, todate));
        }

        [HttpGet]
        public async Task<IActionResult> GetOperationmasterunblockdata()
        {
            string fromdate = string.Empty;
            string todate = string.Empty;
            return Json(await distributeJobService.OperationMasterSPUnBlockViewModels(fromdate, todate, 41));
        }
        [Route("global/api/GetOperationmasterunbockdata/{fromDate}/{toDate}")]
        [HttpGet]
        public async Task<IActionResult> GetOperationmasterunbockdata(string fromdate, string todate)
        {
            if (fromdate != null)
            {
                fromdate = Convert.ToDateTime(fromdate).ToString("yyyyMMdd");
                todate = Convert.ToDateTime(todate).ToString("yyyyMMdd");
            }
            else
            {
                fromdate = string.Empty;
                todate = string.Empty;
            }
            return Json(await distributeJobService.OperationMasterSPUnBlockViewModels(fromdate, todate, 41));
        }

        [HttpGet]
        public async Task<IActionResult> GetOperationmasterhradata()
        {
            string fromdate = string.Empty;
            string todate = string.Empty;
            return Json(await distributeJobService.OperationMasterSPUAViewModels(fromdate, todate));
        }
        [Route("global/api/GetOperationmasterhradata/{fromDate}/{toDate}")]
        [HttpGet]
        public async Task<IActionResult> GetOperationmasterhradata(string fromdate, string todate)
        {
            if (fromdate != null)
            {
                fromdate = Convert.ToDateTime(fromdate).ToString("yyyyMMdd");
                todate = Convert.ToDateTime(todate).ToString("yyyyMMdd");
            }
            else
            {
                fromdate = string.Empty;
                todate = string.Empty;
            }
            return Json(await distributeJobService.OperationMasterSPUAViewModels(fromdate, todate));
        }
        [HttpGet]
        public async Task<IActionResult> UpdateOperationMasterblockbyId(int Id)
        {
            await distributeJobService.UpdateOperationMaster(Id, 41, HttpContext.User.Identity.Name);
            StatusLog log = new StatusLog();

            log.Id = 0;
            log.operationMasterId = Id;
            log.jobStatusId = 42;
            log.remarks = "Job Block";
            log.currentUser = HttpContext.User.Identity.Name;
            log.createdAt = DateTime.Now;
            log.createdBy = HttpContext.User.Identity.Name;
            await distributeJobService.SaveStatusLog(log);
            return RedirectToAction("BlockList", "JobProgress", new { Area = "CRO" });
        }
        [HttpGet]
        public async Task<IActionResult> UpdateOperationMasterunblockbyId(int Id)
        {
            await distributeJobService.UpdateOperationMaster(Id, 42, HttpContext.User.Identity.Name);
            StatusLog log = new StatusLog();

            log.Id = 0;
            log.operationMasterId = Id;
            log.jobStatusId = 42;
            log.remarks = "Job Un blocked";
            log.currentUser = HttpContext.User.Identity.Name;
            log.createdAt = DateTime.Now;
            log.createdBy = HttpContext.User.Identity.Name;
            await distributeJobService.SaveStatusLog(log);
            return RedirectToAction("UnBlockList", "JobProgress", new { Area = "CRO" });
        }

        [HttpGet]
        public async Task<IActionResult> GetRatingValueByRatingTypeCategoryId(int ratType,int categoryId)
        {
            return Json(await distributeJobService.GetRatingValueByRatingTypeCategoryId(ratType, categoryId));
        }

        [HttpGet]
        public async Task<IActionResult> GetOperationMasterByMstId(int id)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            string empCode = "";
            if (empCode != null)
            {
                empCode = userInfos.EmpCode;
            }

            var data = await distributeJobService.GetOperationMasterByAssignToBySp(empCode);
            return Json(data.Where(a => a.Id == id).FirstOrDefault());
        }
    }
}