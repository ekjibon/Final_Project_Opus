using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Meeting.Models;
using OPUSERP.Areas.Workflow.Models;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.Meeting.Data.Entity;
using OPUSERP.Meeting.Service.Interface;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Meeting.Controllers
{
    [Area("Meeting")]
    public class MeetingDocumentController : Controller
    {
        private readonly IUserInfoes userInfoes;
        private readonly IMeetingService docService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IERPCompanyService iERPCompanyService;

        private readonly string rootPath;
        private readonly MyPDF myPDF;

        public MeetingDocumentController(IHostingEnvironment hostingEnvironment, IERPCompanyService iERPCompanyService, IConverter converter, IUserInfoes userInfoes, IPersonalInfoService personalInfoService, IMeetingService docService)
        {
            this._hostingEnvironment = hostingEnvironment;
            this.userInfoes = userInfoes;
            this.docService = docService;
            this.personalInfoService = personalInfoService;
            this.iERPCompanyService = iERPCompanyService;

            //For PDF
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        // GET: VisitorEntry
        public IActionResult Index()
        {
            MeetingDocumentViewModel model = new MeetingDocumentViewModel
            {

            };
            return View(model);
        }

        public async Task<IActionResult> MeetingRoom(int id)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            MeetingDocumentViewModel model = new MeetingDocumentViewModel
            {
                employeeId = empId,
                meetingUsers = await docService.GetAllMeetingUserBoardSecritary(),
                meetingInfo = await docService.GetMeetingInfoById(id),
                meetingAttendancesPresents = await docService.GetAllMeetingAttendancePresentsByMeetingId(id),
                meetingAttendancesAbsents = await docService.GetAllMeetingAttendanceAbsentsByMeetingId(id),
                meetingDocs = await docService.GetAllMeetingDocsByMeetingId(id)
            };
            var user = model.meetingUsers.Where(x => empId.Equals(x.employeeId)).FirstOrDefault();
            ViewBag.user = user;
            return View(model);
        }

        public async Task<IActionResult> MeetingOpinion(int id)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }
            var meeting = await docService.GetMeetingInfoByDocId(id);
            MeetingDocumentViewModel model = new MeetingDocumentViewModel
            {
                employeeId = empId,
                meetingUsers = await docService.GetAllMeetingUserBoardMember(),
                docs = await docService.GetDocById(id),
                meetingInfo = await docService.GetMeetingInfoByDocId(id),
                meetingAttendancesPresents = await docService.GetAllMeetingAttendancePresentsByMeetingId(meeting.Id),
            };
            return View(model);
        }

        public async Task<IActionResult> MeetingUser()
        {
            MeetingDocumentViewModel model = new MeetingDocumentViewModel
            {
                meetingUsers =await docService.GetAllMeetingUser()
            };
            return View(model);
        }

        public IActionResult MeetingGoToArchive(int id)
        {
            docService.UpdateMeetingArchived(id);
            return RedirectToAction(nameof(MeetingList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MeetingUser([FromForm] MeetingUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            MeetingUser meetingActionInfo = new MeetingUser
            {
                employeeId = model.employeeId,
                role = model.role
            };
            int action = await docService.SaveMeetingUser(meetingActionInfo);

            return RedirectToAction(nameof(MeetingUser));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MeetingAttendance([FromForm] MeetingAttendanceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.checkbox != null)
            {
                foreach (var user in model.checkbox)
                {
                    docService.UpdateAttendance((int)model.meetingId, (int)user);
                }
            }

            return RedirectToAction("MeetingRoom", "MeetingDocument", new
            {
                id = model.meetingId
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MeetingOpinion([FromForm] MeetingOpinionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            docService.UpdateDocDecisionSummary((int)model.docId, model.summery, model.decision);

            if (model.user != null)
            {
                for(int i=0;i<model.user.Length;i++)
                {
                    MeetingActionInfo meetingActionInfo = new MeetingActionInfo
                    {
                        description =model.comment[i]
                    };
                    int action = await docService.SaveActionInfo(meetingActionInfo);

                    MeetingDetail meetingDetail = new MeetingDetail
                    {
                        meetingInfoId = model.meetingId,
                        docId = model.docId,
                        employeeId = model.user[i],
                        actionId = action
                    };
                   await docService.SaveMeetingDetail(meetingDetail);
                }
            }

            return RedirectToAction("MeetingRoom", "MeetingDocument", new
            {
                id = model.meetingId
            });
        }

        public async Task<IActionResult> MeetingUserDelete(int id)
        {
            try
            {
                await docService.DeleteMeetingUserById(id);
                return RedirectToAction(nameof(MeetingUserDelete));
            }
            catch
            {
                return RedirectToAction(nameof(MeetingUserDelete));
            }
        }

        public async Task<IActionResult> ChairmanList()
        {
            MeetingDocumentViewModel model = new MeetingDocumentViewModel
            {
                meetingInfos = await docService.GetAllMeetingInfoByStatus(1)
            };
            return View(model);
        }

        public async Task<IActionResult> MeetingList()
        {
            MeetingDocumentViewModel model = new MeetingDocumentViewModel
            {
                meetingInfos = await docService.GetAllMeetingInfoByStatus(3)
            };
            return View(model);
        }

        public async Task<IActionResult> ArchivedMeetingList()
        {
            MeetingDocumentViewModel model = new MeetingDocumentViewModel
            {
                meetingInfos = await docService.GetAllMeetingInfoArchiveds()
            };
            return View(model);
        }

        public async Task<IActionResult> ChairmanApprovedList()
        {
            MeetingDocumentViewModel model = new MeetingDocumentViewModel
            {
                meetingInfos = await docService.GetAllMeetingInfoByStatus(2)
            };
            return View(model);
        }

        public async Task<IActionResult> CallMeeting(int id)
        {
            MeetingDocumentViewModel model = new MeetingDocumentViewModel
            {
                meetingInfo = await docService.GetMeetingInfoById(id),
                meetingDocs = await docService.GetAllMeetingDocsByMeetingId(id)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CallMeeting([FromForm] MeetingInfoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            //return Json(model);
            docService.UpdateMeetingStatus((int)model.meetingId, 3);

            var userList = await docService.GetAllMeetingUserBoardMember();
            if (userList != null)
            {
                foreach(var user in userList)
                {
                    MeetingAttendance meetingAttendance = new MeetingAttendance
                    {
                        meetingInfoId = model.meetingId,
                        employeeId = user.employeeId,
                        isAttendance = 0,
                    };
                    await docService.SaveMeetingAttendance(meetingAttendance);
                }
            }

            return RedirectToAction(nameof(MeetingList));
        }

        public async Task<IActionResult> ChairmanApprove(int id)
        {
            MeetingDocumentViewModel model = new MeetingDocumentViewModel
            {
                meetingInfo = await docService.GetMeetingInfoById(id),
                meetingDocs = await docService.GetAllMeetingDocsByMeetingId(id)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChairmanApprove([FromForm] MeetingInfoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            //return Json(model);

            MeetingActionInfo meetingActionInfo = new MeetingActionInfo {
                description = model.ccontent,
            };
            int action = await docService.SaveActionInfo(meetingActionInfo);

            docService.UpdateMeetingAction((int)model.meetingId, action);
            docService.UpdateMeetingStatus((int)model.meetingId,2);
            await docService.DeleteMeetingDocsByMeetingId((int)model.meetingId);

            if (model.checkbox != null)
            {
                for (int i = 0; i < model.checkbox.Length; i++)
                {
                    MeetingDocs docRoute1 = new MeetingDocs
                    {
                        meetingInfoId = model.meetingId,
                        docId = model.checkbox[i],
                    };
                    int routId = await docService.SaveMeetingDocs(docRoute1);
                    docService.UpdateDocToMeetingReturn((int)model.checkbox[i]);
                }
            }

            return RedirectToAction(nameof(ChairmanApprovedList));
        }

        public async Task<IActionResult> CreateMeeting()
        {
            MeetingDocumentViewModel model = new MeetingDocumentViewModel
            {
                createdNote = await docService.GetAllDocForMeeting()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateMeeting([FromForm] MeetingInfoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            //return Json(model);

            MeetingInfo data = new MeetingInfo
            {
                Id = model.Id,
                type = model.type,
                employeeId = Convert.ToInt32(empId),
                title = model.name,
                Date = model.date,
                status = 1,
                isAchived = 0
            };

            int lstId = await docService.SaveMeetingInfo(data);

            if (model.checkbox != null)
            {
                for (int i = 0; i < model.checkbox.Length; i++)
                {
                    MeetingDocs docRoute1 = new MeetingDocs
                    {
                        meetingInfoId = lstId,
                        docId = model.checkbox[i],
                    };
                    int routId = await docService.SaveMeetingDocs(docRoute1);
                }
            }

            return RedirectToAction(nameof(ChairmanApprovedList));
        }

        public async Task<IActionResult> CreatedNote()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            MeetingDocumentViewModel model = new MeetingDocumentViewModel
            {
                createdNote = await docService.GetAllCreaedDoc(empId)
            };
            return View(model);
        }

        public async Task<IActionResult> ReturnedNote()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            MeetingDocumentViewModel model = new MeetingDocumentViewModel
            {
                createdNote = await docService.GetAllReturnDoc(empId)
            };
            return View(model);
        }

        public async Task<IActionResult> ProcessedNote()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            MeetingDocumentViewModel model = new MeetingDocumentViewModel
            {
                createdNote = await docService.GetAllProcessedDoc(empId)
            };
            return View(model);
        }

        public async Task<IActionResult> ManagedNote()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            MeetingDocumentViewModel model = new MeetingDocumentViewModel
            {
                createdNote = await docService.GetAllManagedDoc(empId)
            };
            return View(model);
        }

        public async Task<IActionResult> PendingNote()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            MeetingDocumentViewModel model = new MeetingDocumentViewModel
            {
                createdNote = await docService.GetAllPendingDoc(empId)
            };
            return View(model);
        }

        public async Task<IActionResult> ActiveNote()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            MeetingDocumentViewModel model = new MeetingDocumentViewModel
            {
                createdNote = await docService.GetAllActiveDoc(empId)
            };
            return View(model);
        }

        //public async Task<IActionResult> ReviewNote()
        //{
        //    string userName = HttpContext.User.Identity.Name;
        //    var userInfos = await userInfoes.GetUserInfoByUser(userName);
        //    var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
        //    int empId = 0;
        //    if (EmpInfo != null)
        //    {
        //        empId = EmpInfo.Id;
        //    }

        //    MeetingDocumentViewModel model = new MeetingDocumentViewModel
        //    {
        //        docWithReviewIdModels = await docService.GetAllPendingDocforRevew(empId)
                
        //    };
        //    return View(model);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([FromForm] DocumentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo.Id != 0)
            {
                empId = EmpInfo.Id;
            }

            //return Json(model);

            MeetingDocument data = new MeetingDocument
            {
                Id = model.docId,
                //employeeId = Convert.ToInt32(empId),
                employeeId = empId,
                number = model.number,
                Date = DateTime.Now,
                subject = model.subject,
                content = model.content,
                noteType = model.noteType,
                isClose = 0,
                isInitial = 1
            };

            int lstId = await docService.SaveDoc(data);

            MeetingDocRoute docRoute = new MeetingDocRoute
            {
                docId = lstId,
                employeeId = empId,
                isActive = 0,
                status = "Created",
                order = 1,
            };
            int lstrt = await docService.SaveDocRoute(docRoute);

            if (model.ids != null)
            {
                int Active = 1;
                int order = 2;
                for (int i = 0; i < model.ids.Length; i++)
                {
                    MeetingDocRoute docRoute1 = new MeetingDocRoute
                    {
                        docId = lstId,
                        employeeId = model.ids[i],
                        isActive = Active,
                        status = "Created",
                        order = order,
                    };
                    int routId = await docService.SaveDocRoute(docRoute1);
                    Active = 0;
                    order++;
                }
            }

            if (model.attachments != null)
            {
                for (int i = 0; i < model.attachments.Length; i++)
                {
                    string fileName;
                    string message = FileSave.SaveImage(out fileName, model.attachments[i]);

                    if (message == "success")
                    {
                        MeetingDocAttachment docAttachment = new MeetingDocAttachment
                        {
                            docId = lstId,
                            title = model.fileTitle[i],
                            fileUrl = fileName,
                        };
                        await docService.SaveDocAttachment(docAttachment);
                    }
                }
            }

            return RedirectToAction("DocPreView", "MeetingDocument", new
            {
                id = lstId
            });

            //return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DocPreView(int id)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            MeetingDocumentViewModel model = new MeetingDocumentViewModel
            {
                doc = await docService.GetDocByIdWithSignature(id),
                docAttachment = await docService.GetAllDocAttachmentByDocId(id),
                docRoute = await docService.GetAllDocRouteByDocIdWithSignature(id),
                //reviewRoutes = await docService.GetAllReviewRouteByRouteId(id),
                docCheck = await docService.GetDocRouteByEmpIdAndDocId(empId, id),
                employeeId = empId
            };
            return View(model);
        }

        //public async Task<IActionResult> DocReView(int id,int ReviewId)
        //{
        //    string userName = HttpContext.User.Identity.Name;
        //    var userInfos = await userInfoes.GetUserInfoByUser(userName);
        //    var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
        //    int empId = 0;
        //    if (EmpInfo != null)
        //    {
        //        empId = EmpInfo.Id;
        //    }

        //    DocumentViewModel model = new DocumentViewModel
        //    {
        //        doc = await docService.GetDocByIdWithSignature(id),
        //        docAttachment = await docService.GetAllDocAttachmentByDocId(id),
        //        docRoute = await docService.GetAllDocRouteByDocIdWithSignature(id),
        //        reviewRoutes = await docService.GetAllReviewRouteByRouteId(id),
        //        docCheck = await docService.GetDocRouteByEmpIdAndDocId(empId, id),
        //        employeeId = empId,
        //        ReviewId = ReviewId
        //    };
        //    return View(model);
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DocForward([FromForm] DocForwardViewmodel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            //return Json(model);

            MeetingActionInfo actionInfo = new MeetingActionInfo
            {
                description = model.comment,
                createdAt = DateTime.Now,
            };
            int actionId = await docService.SaveActionInfo(actionInfo);
            docService.UpdateActionIdInRoute(model.docRouteID, actionId);

            MeetingDocRoute route = await docService.GetDocRouteById(model.docRouteID);
            docService.UpdateRouteStatus(model.docRouteID, 0);

            if (model.submit == "Forward")
            {
                int? seq = route.order;
                seq++;
                MeetingDocRoute docRouteNew = await docService.GetDocRouteByDocIdAndOrder(model.docId, (int)seq);
                if (docRouteNew != null)
                {
                    docService.UpdateRouteStatus(docRouteNew.Id, 1);
                }
                else
                {
                    docService.UpdateDocToCloded(model.docId);
                }
            }
            else
            {
                docService.UpdateDocToClodedReturn(model.docId);
            }

            return RedirectToAction("DocPreView", "MeetingDocument", new
            {
                id = model.docId
            });

            //return RedirectToAction(nameof(Index));
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ReviewComment([FromForm] DocForwardViewmodel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    string userName = HttpContext.User.Identity.Name;
        //    var userInfos = await userInfoes.GetUserInfoByUser(userName);
        //    var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
        //    int empId = 0;
        //    if (EmpInfo != null)
        //    {
        //        empId = EmpInfo.Id;
        //    }

        //    //return Json(model);

        //    ActionInfo actionInfo = new ActionInfo
        //    {
        //        description = model.comment,
        //        createdAt = DateTime.Now,
        //    };
        //    int actionId = await docService.SaveActionInfo(actionInfo);
        //    docService.UpdateActionIdInReviewRoute((int)model.ReviewId, actionId);
            
        //    return RedirectToAction(nameof(ReviewNote));
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DocAttachment([FromForm] DocAttachmentViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    string userName = HttpContext.User.Identity.Name;
        //    var userInfos = await userInfoes.GetUserInfoByUser(userName);
        //    var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
        //    int empId = 0;
        //    if (EmpInfo != null)
        //    {
        //        empId = EmpInfo.Id;
        //    }

        //    //return Json(model);

        //    if (model.attachments != null)
        //    {
        //        for (int i = 0; i < model.attachments.Length; i++)
        //        {
        //            string fileName;
        //            string message = FileSave.SaveImage(out fileName, model.attachments[i]);

        //            if (message == "success")
        //            {
        //                DocAttachment docAttachment = new DocAttachment
        //                {
        //                    docId = model.docId,
        //                    title = model.fileTitle[i],
        //                    fileUrl = fileName,
        //                };
        //                await docService.SaveDocAttachment(docAttachment);
        //            }
        //        }
        //    }


        //    return RedirectToAction("DocPreView", "Document", new
        //    {
        //        id = model.docId
        //    });

        //    //return RedirectToAction(nameof(Index));
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DocApproverAdd([FromForm] AddDocRouteViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    string userName = HttpContext.User.Identity.Name;
        //    var userInfos = await userInfoes.GetUserInfoByUser(userName);
        //    var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
        //    int empId = 0;
        //    if (EmpInfo != null)
        //    {
        //        empId = EmpInfo.Id;
        //    }

        //    //return Json(model);

        //    if (model.noteType == "Internal")
        //    {
        //        if (model.ids != null)
        //        {
        //            for (int i = 0; i < model.ids.Length; i++)
        //            {
        //                ReviewRoute reviewRoute = new ReviewRoute
        //                {
        //                    docRouteId = model.docRouteID,
        //                    employeeId = model.ids[i],
        //                    status = "Created"
        //                };
        //                await docService.SaveReviewRoute(reviewRoute);
        //            }
        //        }

        //    }
        //    else
        //    {
        //        int count = model.ids.Length;

        //        DocRoute docRoute = await docService.GetDocRouteById(model.docRouteID);

        //        IEnumerable<DocRoute> docRoutes = await docService.GetAllDocRouteByDocIdDecendaing(model.docId);

        //        foreach (var data in docRoutes)
        //        {
        //            int newOrder = 0;
        //            if (data.isActive == 1)
        //            {
        //                break;
        //            }
        //            newOrder = (int)data.order + count;
        //            docService.UpdateRouteOrder(data.Id, newOrder);
        //        }

        //        if (model.ids != null)
        //        {
        //            int order = (int)docRoute.order + 1;
        //            for (int i = 0; i < model.ids.Length; i++)
        //            {
        //                DocRoute docRoute1 = new DocRoute
        //                {
        //                    docId = model.docId,
        //                    employeeId = model.ids[i],
        //                    isActive = 0,
        //                    status = "Created",
        //                    order = order,
        //                };
        //                int routId = await docService.SaveDocRoute(docRoute1);
        //                order++;
        //            }
        //        }
        //    }
        //    return RedirectToAction("DocPreView", "Document", new
        //    {
        //        id = model.docId
        //    });

        //    //return RedirectToAction(nameof(Index));
        //}




        [AllowAnonymous]
        public async Task<IActionResult> DocView(int id)
        {
            MeetingDocumentViewModel model = new MeetingDocumentViewModel
            {
                doc = await docService.GetDocByIdWithSignature(id),
                docAttachment = await docService.GetAllDocAttachmentByDocId(id),
                docRoute = await docService.GetAllDocRouteByDocIdWithSignature(id),
                companies = await iERPCompanyService.GetAllCompany()
            };
            return View(model);
        }

        //PrintPDF
        [AllowAnonymous]
        public IActionResult DocViewPdf(int id)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string fileName;

            string url = scheme + "://" + host + "/Meeting/MeetingDocument/DocView/" + id;
            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public async Task<IActionResult> ArchivedMeeting(int id)
        {
            MeetingDocumentViewModel model = new MeetingDocumentViewModel
            {
                meetingAttendancesPresents = await docService.GetAllMeetingAttendancePresentsByMeetingId(id),
                meetingAttendancesAbsents = await docService.GetAllMeetingAttendanceAbsentsByMeetingId(id),
                meetingInfo = await docService.GetMeetingInfoById(id),
                docWiseMeetingDetails = await docService.DocWiseMeetingDetails(id),
                companies = await iERPCompanyService.GetAllCompany()
            };
            return View(model);
        }

        //PrintPDF
        [AllowAnonymous]
        public IActionResult ArchivedMeetingPdf(int id)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string fileName;

            string url = scheme + "://" + host + "/Meeting/MeetingDocument/ArchivedMeeting/" + id;
            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }



    }
}