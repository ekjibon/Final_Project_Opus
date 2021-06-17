using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.CRO.Models;
using OPUSERP.CRO.Data.Entity.DistributeJob;
using OPUSERP.CRO.Services.DistributeJob;
using OPUSERP.CRO.Services.DistributeJob.Interfaces;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Employee.Interfaces;

namespace OPUSERP.Areas.CRO.Controllers
{
    [Area("CRO")]
    public class IRCRatingController : Controller
    {
        private readonly IDistributeJobService distributeJobService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUserInfoes userInfo;
        private readonly IAttachmentCommentService attachmentCommentService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly string rootPath;
        private readonly MyPDF myPDF;

        public IRCRatingController(IHostingEnvironment hostingEnvironment, IDistributeJobService distributeJobService, IUserInfoes userInfo, IPersonalInfoService personalInfoService, IAttachmentCommentService attachmentCommentService, IConverter converter)
        {
            this.distributeJobService = distributeJobService;
            this._hostingEnvironment = hostingEnvironment;
            this.userInfo = userInfo;
            this.attachmentCommentService = attachmentCommentService;
            this.personalInfoService = personalInfoService;
            //For PDF
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        public async Task<IActionResult> IRCPreview(int id)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            LeadInCroViewModel model = new LeadInCroViewModel
            {
                statusLogs = await distributeJobService.GetStatusLogByOperationMasterId(id),
                receiveDocuments = await distributeJobService.GetAllReceiveDocumentByOperMstid(id),
                croAttachmentViewModels = await attachmentCommentService.GetCroDocumentAttachment(id, "Document", 13),
                getLeadInfoInCroViewModels = await distributeJobService.GetLeadInfoInCroByOPMstId(id),
                iRCMeetingAttendances = await distributeJobService.GetAllIRCMeetingAttendanceByOperMstid(id),
                iRCMemberComments = await distributeJobService.GetAllIRCMemberCommentsByOperMstid(id),
                proposedRating = await distributeJobService.GetProposedRatingByOpMstId(id),
                iRCRating = await distributeJobService.GetIRCRatingByOperationMasterId(id)
            };
            return View(model);
        }

        public IActionResult IRCPreviewPdf(int Id)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;

            url = scheme + "://" + host + "/CRO/IRCRating/IRCPrintview?Id=" + Id;

            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        public async Task<IActionResult> IRCPrintview(int id)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            LeadInCroViewModel model = new LeadInCroViewModel
            {
                statusLogs = await distributeJobService.GetStatusLogByOperationMasterId(id),
                receiveDocuments = await distributeJobService.GetAllReceiveDocumentByOperMstid(id),
                croAttachmentViewModels = await attachmentCommentService.GetCroDocumentAttachment(id, "Document", 13),
                getLeadInfoInCroViewModels = await distributeJobService.GetLeadInfoInCroByOPMstId(id),
                iRCMeetingAttendances = await distributeJobService.GetAllIRCMeetingAttendanceByOperMstid(id),
                iRCMemberComments = await distributeJobService.GetAllIRCMemberCommentsByOperMstid(id),
                proposedRating = await distributeJobService.GetProposedRatingByOpMstId(id),
                iRCRating = await distributeJobService.GetIRCRatingByOperationMasterId(id)
            };
            return View(model);
        }

        public async Task<IActionResult> Index()
        {
            IRCMeetingAttendanceViewModel model = new IRCMeetingAttendanceViewModel
            {
                //operationMasters = await distributeJobService.GetAllOperationMaster(),
                getOMByassignToReviewerViewModels = await distributeJobService.GetAllOperationMasterBySp(),
                ratingValues = await distributeJobService.GetAllRatingValue()
            };
            return View(model);
        }

        public async Task<IActionResult> IRCRatinglist()
        {
            IRCMeetingAttendanceViewModel model = new IRCMeetingAttendanceViewModel
            {
                //operationMasters = await distributeJobService.GetAllOperationMaster(),
                getOMByassignToReviewerViewModels = await distributeJobService.GetAllOperationMasterBySp(),
                ratingValues = await distributeJobService.GetAllRatingValue()
            };
            return View(model);
        }
        public async Task<IActionResult> RatingPrevious()
        {
            IRCMeetingAttendanceViewModel model = new IRCMeetingAttendanceViewModel
            {
                //operationMasters = await distributeJobService.GetAllOperationMaster(),
                previousRatingDataViewModels = await distributeJobService.PreviousRatingDataViewModels(),
                ratingValues = await distributeJobService.GetAllRatingValue()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<JsonResult> RatingPrevious([FromForm] IRCMeetingAttendanceViewModel model)
        {
            try
            {
                OperationMaster OM = new OperationMaster();

                OM.Id = 0;
                OM.agreementId = Convert.ToInt32(model.agreementId);
                OM.approvedforCroId = Convert.ToInt32(model.approveforcroid);
                OM.jobStatusId = 47;
              
                OM.assignTeam = model.assignTeam;
                OM.assignTeamLeader = model.assignTeam;
                OM.assignTeamDate = model.assignDate;
                OM.assignDate = model.assignDate;
                OM.assignTo = model.assignTo;
                OM.comments = "Previous Rating";
                int OPMID = await distributeJobService.SaveOperationMaster(OM);

                IRCRating data = new IRCRating
                {
                    Id = 0,
                    operationMasterId = OPMID,
                    ircDate = model.ircDate,
                    ratingTypeName = model.ratingTypeName,
                    ratingValue = model.ratingValue,
                    shortRatingName = model.shortRatingName,
                    outlook = model.outlook,
                };
                int ratingId = await distributeJobService.SaveIRCRating(data);

                StatusLog status = new StatusLog
                {
                    Id = 0,
                    operationMasterId = OPMID,
                    jobStatusId = 47,
                    currentUser = HttpContext.User.Identity.Name
                };
                int statusId = await distributeJobService.SaveStatusLog(status);

                var masterId = await distributeJobService.UpdateOperationMasterArchieved(OPMID, 47);

                return Json(ratingId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public async Task<JsonResult> SaveIRCAttendance([FromForm] IRCMeetingAttendanceViewModel model)
        {
            try
            {
                int attendanceId = 0;

                var attendanceCheck = await distributeJobService.GetMeetingAttendanceByMstIdandEmpId(model.operationMasterId, model.employeeInfoId);
                if (attendanceCheck.Count() > 0)
                {                    
                    attendanceId = 0;
                }
                else
                {
                    IRCMeetingAttendance data = new IRCMeetingAttendance
                    {
                        Id = 0,
                        operationMasterId = Convert.ToInt32(model.operationMasterId),
                        employeeInfoId = Convert.ToInt32(model.employeeInfoId)
                    };

                    attendanceId = await distributeJobService.SaveIRCMeetingAttendance(data);
                }

                return Json(attendanceId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<JsonResult> SaveComments([FromForm] IRCMemberCommentsViewModel model)
        {           

            try
            {
                IRCMemberComments data = new IRCMemberComments
                {
                    Id = 0,
                    operationMasterId = Convert.ToInt32(model.operationMasterId),
                    employeeInfoId = Convert.ToInt32(model.employeeInfoIdComment),
                    comments = model.comments
                };

                int DocumentId = await distributeJobService.SaveIRCMemberComments(data);

                return Json(DocumentId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<JsonResult> SaveIrcRating([FromForm] IRCRatingViewModel model)
        {
            try
            {
                IRCRating data = new IRCRating
                {
                    Id = 0,
                    operationMasterId = model.operationMasterId,
                    ircDate = model.ircDate,
                    ratingTypeName = model.ratingTypeName,
                    ratingValue = model.ratingValue,
                    shortRatingName = model.shortRatingName,
                    outlook = model.outlook,
                };
                int ratingId = await distributeJobService.SaveIRCRating(data);

                StatusLog status = new StatusLog
                {
                    Id = 0,
                    operationMasterId = model.operationMasterId,
                    jobStatusId = 47,
                    currentUser = HttpContext.User.Identity.Name
                };
                int statusId = await distributeJobService.SaveStatusLog(status);

                var masterId = await distributeJobService.UpdateOperationMasterArchieved(model.operationMasterId, 47);

                return Json(ratingId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<JsonResult> DeclineIrcRating([FromForm] IRCRatingViewModel model)
        {
            try
            {
                StatusLog status = new StatusLog
                {
                    Id = 0,
                    operationMasterId = model.operationMasterId,
                    jobStatusId = 2060,
                    currentUser = HttpContext.User.Identity.Name
                };
                int statusId = await distributeJobService.SaveStatusLog(status);

                var masterId = await distributeJobService.UpdateOperationMasterArchieved(model.operationMasterId, 2060);

                return Json(statusId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIRCMeetingAttendanceByOperMstid(int Id)
        {
            return Json(await distributeJobService.GetAllIRCMeetingAttendanceByOperMstid(Id));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteIRCAttendance(int Id)
        {
            await distributeJobService.DeleteIRCMeetingAttendanceById(Id);
            return Json(true);
        }

        [HttpGet]
        public async Task<IActionResult> GetProposedRatingByMastId(int id)
        {
            return Json(await distributeJobService.GetProposedRatingByOpMstId(id));
        }
        [HttpGet]
        public async Task<IActionResult> GetIRCRatingByMastId(int id)
        {
            return Json(await distributeJobService.GetIRCRatingByOpMstId(id));
        }
        [HttpGet]
        public async Task<IActionResult> GetArchieveByMastId(int id)
        {
            return Json(await distributeJobService.GetArchiveByOperationMasterId(id));
        }
    }
}